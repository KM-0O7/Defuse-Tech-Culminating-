using UnityEngine;

public class WireDrag : MonoBehaviour
{
    [SerializeField] private LineRenderer groundWireDrag;
    [SerializeField] private Transform startPos;
    bool draggingWire = false;
    SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("Clicker").GetComponent<SceneTransition>();
    }

    void Update()
    {
        groundWireDrag.positionCount = 2;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!draggingWire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Wires"));
                if (hit && hit.collider.CompareTag("GroundWire"))
                {
                    draggingWire = true;
                    PlayerMovement.canMove = false;
                    groundWireDrag.SetPosition(0, startPos.position);
                    groundWireDrag.SetPosition(1, mousePos);
                }
            }
        } else {

            groundWireDrag.SetPosition(1, mousePos);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Wires"));
                if (hit && hit.collider.CompareTag("GroundWireSlot"))
                {
                    draggingWire = false;
                    sceneTransition.TP("Warp3");
                }
            }
        }
    }
}
