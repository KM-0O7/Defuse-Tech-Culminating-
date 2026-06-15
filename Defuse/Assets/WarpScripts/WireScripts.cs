using UnityEngine;

public class WireScripts : MonoBehaviour
{
    [SerializeField] private bool correctWire;
    private SceneTransition sceneTransition;
    [SerializeField] private string sceneToTp;
    [SerializeField] private bool canSkip = false;
    [SerializeField] private bool solderWire = false;
    [SerializeField] private GameObject solderBoardToShow;
    private bool placedSolderBoard = false;
    private bool tp = false;
    private HandScript handScript;

    private void Start()
    {
        handScript = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandScript>();
        sceneTransition = GameObject.FindGameObjectWithTag("Clicker").GetComponent<SceneTransition>();
    }

    private void Update()
    {
        if (SceneTransition.canTP)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Wires"));
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (HandScript.carryingObject && tp == false && SceneTransition.canTP)
                    {
                        if (!solderWire)
                        {
                            tp = true;
                            if (HandScript.holdingCorrectObject || canSkip)
                            {
                                Debug.Log("HoldingCorrectItem");
                                if (correctWire)
                                {
                                    sceneTransition.TP(sceneToTp);
                                }
                                else
                                {
                                    Debug.Log("What?");
                                    sceneTransition.TP("BlowUp");
                                }
                            }
                            else
                            {
                                Debug.Log("BlowUp");
                                sceneTransition.TP("BlowUp");
                            }
                        }
                        else
                        {
                            if (HandScript.holdingCorrectObject && placedSolderBoard && handScript.glasses)
                            {
                                tp = true;
                                sceneTransition.TP("BigButton");
                            }
                            else if (!placedSolderBoard && handScript.holdingSolderBoard)
                            {
                                handScript.holdingSolderBoard = false;
                                placedSolderBoard = true;
                                HandScript.carryingObject = false;
                                solderBoardToShow.SetActive(true);
                                Destroy(GameObject.Find("SolderBoardHold"));
                                Destroy(GameObject.Find("SolderBoardHold(Clone)"));
                            }
                            else if (!handScript.glasses || (!handScript.holdingSolderBoard && !placedSolderBoard))
                            {
                                Debug.Log("TP");
                                sceneTransition.TP("BlowUp");
                            }
                        }
                    }
                }
            }
        }
    }
}