using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed; 
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask groundMask2; 
    public KeyCode jumpingKey;
    public KeyCode leftButton;
    public KeyCode rightButton;
    public int faceDir; // this tell if the player is facing right or left with 1 and -1    
    private Vector3 initialScale;
    public bool isPushing = false;
    public bool isJumping = false;
    public bool invincibility = false; 


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();        
        boxCollider = GetComponent<BoxCollider2D>();
        initialScale = transform.localScale;        
        faceDir = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() || isGrounded2())
            isJumping = false; 

        if (Input.GetKey(leftButton))
        {
            body.velocity = new Vector2(-1 * speed, body.velocity.y);   
            if(!isPushing)
            faceDir = -1; //Looking left
        }
        else if (Input.GetKey(rightButton))
        {
            body.velocity = new Vector2(1 * speed, body.velocity.y);
            if(!isPushing)
            faceDir = 1; //Looking Right
        }
        else
            body.velocity = new Vector2(0, body.velocity.y);



        if (isWall())
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        if (Input.GetKey(jumpingKey))
        {
            if (!isJumping)
                Jump();
        }

        if(Mathf.Abs(faceDir) == 1 && !isPushing) 
        {            
            transform.localScale = new Vector3(initialScale.x * faceDir, transform.localScale.y, transform.localScale.z);  
        }        


    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed * 2);
        isJumping = true;
    }

    public void DamageJump() 
    {
        body.velocity = new Vector2(body.velocity.x + (jumpSpeed * faceDir * -1f), jumpSpeed * 1.5f); 
        Debug.Log("Jumping from the attack"); 
        isJumping = true;
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundMask);
        return raycastHit.collider != null;
    }

    private bool isGrounded2() 
    {
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundMask2);
        return raycastHit2.collider != null;
    }

    private bool isWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(faceDir, 0), 0.1f, groundMask);
        return raycastHit.collider != null;
    }

    public void IncreaseSpeed(float increase) 
    {
        speed += increase; 
    }

    public void IncreaseJump(float increase) 
    {
        jumpSpeed += increase;
    }

}
