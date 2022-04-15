using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ////////////////////////* DECLARATIONS *//////////////////////////////////////////////////////////////

    private Rigidbody2D rb; // assigned at Start

    // horizontal movement-related
    private float xInput; //used to capture xinput changes
    private float xSpeed = 10;
    private float scaleX;

    // jump-related
    private float jumpForce = 10;
    // used to check if on the ground
    public bool isGrounded;
    public Transform GroundChecker; // circle collider located under the player object, used to check if on the ground
    public LayerMask GroundLayer; // objects e.g. ground / platforms that marked as "Ground" in the Inspector


    //////////////////////////////////////////* FUNCTIONS *////////////////////////////////////////////////////////
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // assigns player rigidbody to variable rb
        scaleX = transform.localScale.x;
    }
    void Update()
    {
        xInput = Input.GetAxis("Horizontal"); // get horizontal input and stores in variable
        Jump(); // handles jump logic
    }

    private void FixedUpdate()
    {
        Move(); // handles horizontal logic
    }

    public void Move() // handles horizontal movement logic
    {
        Flip(); // checks to see if direction has changed
        rb.velocity = new Vector2(xInput * xSpeed, rb.velocity.y); //update x-velocity, while keeping y-velocity constant
    }

    public void Flip() // called to check if x-direction has changed
    {
        if (xInput > 0) { transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z); }
        if (xInput < 0) { transform.localScale = new Vector3((-1) * scaleX, transform.localScale.y, transform.localScale.z); }
    }

    public void Jump() // manages jumping
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckIfGrounded(); //updates isGrounded

            if (isGrounded) // called to see if on ground
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    public void CheckIfGrounded() // checks if grounded, returns true if it is
    {
        isGrounded = Physics2D.OverlapCircle(GroundChecker.position, GroundChecker.GetComponent<CircleCollider2D>().radius, GroundLayer);

        // the above take Groundchecker collider (a circle collider below the player), its radius, and see if its collided / colliding
        // with anything in the GroundLayer (platforms tagged with ground); if it has it returns true to isGrounded
    }
}