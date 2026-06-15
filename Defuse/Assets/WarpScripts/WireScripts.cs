using UnityEngine;

public class WireScripts : MonoBehaviour
{
    [SerializeField] private bool correctWire;
    SceneTransition sceneTransition;
    [SerializeField] private string sceneToTp;
    [SerializeField] private bool canSkip = false;
    bool tp = false;

    private void Start()
    {
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
                }
            }
        }
    }
}