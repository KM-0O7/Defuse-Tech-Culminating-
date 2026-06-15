using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private SceneTransition sceneTransition;
    [SerializeField] private string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("Clicker").GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneTransition.canTP)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Button"));
                if (hit && hit.collider.gameObject == gameObject)
                {
                    sceneTransition.TP(sceneName);
                }
            }
        }
    }
}