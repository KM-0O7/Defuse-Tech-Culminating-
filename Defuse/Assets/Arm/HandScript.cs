using UnityEngine;

public class HandScript : MonoBehaviour
{
    TargetJoint2D Tj2d;
    public static string currentObject;
    bool carryingObject = false;
    GameObject clonedItem = null;
    
    void Start()
    {
        Tj2d = GetComponent<TargetJoint2D>();
    }
 
    void Update()
    {
        Input.mousePosition.Set(Input.mousePosition.x, Input.mousePosition.y, 0);
        Tj2d.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!carryingObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Items"));
                if (hit.collider != null)
                {
                    carryingObject = true;
                    var heldObject = hit.collider.gameObject;
                    
                }
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }
}