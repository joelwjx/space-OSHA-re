using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool isOnLadder;
    private Rigidbody2D rb;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isOnLadder = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey("e"))
        {
            horizontalInput = 0;
            animator.SetBool("IsInteracting", true);
            return;
        }
        else
        {
            animator.SetBool("IsInteracting", false);
        }

        animator.SetBool("IsMoving", Mathf.Abs(horizontalInput) >= 0.01);

        SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();
        if (horizontalInput > 0)
        {
            Debug.Log("Moving right");
            mySpriteRenderer.flipX = false;
        } else if (horizontalInput < 0)
        {
            Debug.Log("Moving left");
            mySpriteRenderer.flipX = true;
        }


        if (isOnLadder && verticalInput > 0)
        {
            rb.gravityScale = 0;
            animator.SetBool("IsClimbing", true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalInput, isOnLadder ? verticalInput : 0);
        //rb.MovePosition(((Vector2)transform.position + new Vector2(horizontalMove, 0)));
        transform.position += movement * Time.deltaTime * 10f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder") isOnLadder = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder") {
            isOnLadder = false;
            rb.gravityScale = 1;
            animator.SetBool("IsClimbing", false);
        } 
    }
}
