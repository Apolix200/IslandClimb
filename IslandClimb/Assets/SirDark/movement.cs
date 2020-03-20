using UnityEngine;
using System.Collections;


public class movement : MonoBehaviour
{
    Rigidbody2D body;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    //public float gravity = 20.0f;

    private float moveInput = 0f;

    private bool isGrounded = true;
    private bool onWall= false;

    public string groundtag = "Ground";
    public string walltag = "Wall";
    
    private GameObject lastWall = null;

    

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded || onWall)){
            
            if(isGrounded){
                body.velocity = new Vector2(0,1)* jumpSpeed;
                

            }
            else if(onWall){
                body.velocity = new Vector2(0,1)* jumpSpeed;
                onWall = false;
            }

        }
        Debug.Log(onWall);
        
    }
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        body.velocity = new Vector2(moveInput*speed, body.velocity.y);

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == groundtag){
            isGrounded = true;
            lastWall = null;
        }
        if(other.gameObject.tag == walltag && other.gameObject != lastWall){
            onWall = true;
            lastWall = other.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == groundtag){
            isGrounded = false;
        }
        if(other.gameObject.tag == walltag){
            onWall = false;
        }
    }

    public bool getOnWall ()
    {
        return onWall;
    }
    public bool getIsGrounded()
    {
        return isGrounded;
    }
}