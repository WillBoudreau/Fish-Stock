using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] float speed;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundMask;
    public KeyCode jumpingKey;
    public KeyCode leftButton;
    public KeyCode rightButton;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();        
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftButton))
        {
            body.velocity = new Vector2(-1 * speed, body.velocity.y);
        }
        else if (Input.GetKey(rightButton))
        {
            body.velocity = new Vector2(1 * speed, body.velocity.y);
        }
        else
            body.velocity = new Vector2(0, body.velocity.y);

        if (Input.GetKey(jumpingKey))
        {
            if (isGrounded())
                Jump();
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed * 2);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundMask);
        return raycastHit.collider != null;
    }
}
