using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool isOnLadder;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isOnLadder = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (isOnLadder && verticalInput > 0)
        {
            rb.gravityScale = 0;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalInput, isOnLadder ? verticalInput : 0);
        //rb.MovePosition(((Vector2)transform.position + new Vector2(horizontalMove, 0)));
        transform.position += movement * Time.deltaTime * 7.5f;
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
        } 
    }
}
