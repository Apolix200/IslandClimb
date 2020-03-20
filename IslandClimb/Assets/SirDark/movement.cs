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

    public string double_jump_unlock_tag = "double jump unlock";
    bool double_jump_unlock = false;
    bool double_jump = false;

    

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded || onWall || double_jump)){
            
            if(isGrounded){
                body.velocity = new Vector2(0,1)* jumpSpeed;
                

            }
            else if(onWall){
                body.velocity = new Vector2(0,1)* jumpSpeed;
                onWall = false;
            }
            else if(double_jump){
                body.velocity = new Vector2(0,1)* jumpSpeed;
                double_jump = false;
            }
            

        }
        Debug.Log(isGrounded);

        
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
            if(double_jump_unlock){
                double_jump = true;
            }
        }
        if(other.gameObject.tag == walltag && other.gameObject != lastWall){
            onWall = true;
            lastWall = other.gameObject;
            if(double_jump_unlock){
                double_jump = true;
            }
        }
        if(other.gameObject.tag == double_jump_unlock_tag){
            double_jump_unlock = true;
            
            double_jump = true;
            Destroy(other.gameObject);
            
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
}