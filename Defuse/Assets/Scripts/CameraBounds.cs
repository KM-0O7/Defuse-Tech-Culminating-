using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private BoxCollider2D bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetBounds();
        }
    }

    public void ResetBounds()
    {
        bc = GetComponent<BoxCollider2D>();
        if (bc != null)
        {
            Bounds b = bc.bounds;

            Vector2 min = b.min;
            Vector2 max = b.max;
            Debug.Log("Reset Bound");
            Camera.main.GetComponent<FollowPlayer>().SetBounds(min, max);
        }
        else Debug.Log("How?");
    }
}