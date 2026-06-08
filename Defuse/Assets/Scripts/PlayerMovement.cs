using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRig;
    float speedX;
    float playerSpeed = 2f;
    public static bool canMove = true;
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (canMove)
        {
            speedX = Input.GetAxisRaw("Horizontal");
            playerRig.linearVelocityX = speedX * playerSpeed;
        } else playerRig.linearVelocityX = 0; 
    }   
}
