using UnityEngine;
using UnityEngine.UIElements;

public class HandScript : MonoBehaviour
{
    private TargetJoint2D Tj2d;
    public static bool holdingCorrectObject = false;
    public static bool carryingObject = false;
    private GameObject clonedItem;
    private GameObject heldObject;
    public bool holdingSolderBoard = false;
    public bool glasses = false;

    private void Start()
    {
        Tj2d = GetComponent<TargetJoint2D>();
    }

    private void Update()
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
                    heldObject = hit.collider.gameObject;
                    if (hit.collider.tag == "WireCutter" || hit.collider.tag == "Drone" || hit.collider.tag == "Solder")
                    {
                        Debug.Log("Picked Up Cutter!");
                        holdingCorrectObject = true;
                    }
                    else holdingCorrectObject = false;
                    if (hit.collider.tag == "SolderBoard")
                    {
                        holdingSolderBoard = true;
                    }
                    else holdingSolderBoard = false;

                    if (hit.collider.tag == "Glasses")
                    {
                        Destroy(hit.collider.gameObject);
                        glasses = true;
                        carryingObject = false;
                    }
                    else
                    {
                        clonedItem = Instantiate(heldObject);
                        heldObject.SetActive(false);
                        SpriteRenderer[] sprites = clonedItem.GetComponentsInChildren<SpriteRenderer>();
                        for (int i = 0; i < sprites.Length; i++)
                        {
                            sprites[i].sortingOrder = 4;
                        }
                    }
                }
            }
        }
        else
        {
            if (clonedItem != null)
            {
                clonedItem.transform.position = transform.position;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (clonedItem.gameObject)
                {
                    Destroy(clonedItem.gameObject);
                    heldObject.SetActive(true);
                    clonedItem = null;
                    carryingObject = false;
                    holdingCorrectObject = false;
                    holdingSolderBoard = false;
                }
            }
        }
    }

    private void OnEnable()
    {
        carryingObject = false;
        holdingCorrectObject = false;
    }
}