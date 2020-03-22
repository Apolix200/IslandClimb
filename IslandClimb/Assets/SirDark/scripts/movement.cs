using UnityEngine;
using System.Collections;


public class movement : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    //public float gravity = 20.0f;

    private float moveInput = 0f;

    
    private bool onWall= false;

    public GameObject jumpParticle;
    public string groundtag = "Ground";
    public string walltag = "Wall";
    
    //private GameObject lastWall = null;

    public string double_jump_unlock_tag = "double jump unlock";
    bool double_jump_unlock = false;
    bool double_jump = false;

    private bool isGrounded = true;
    public Transform leftSide;
    public float checkRadius;
    public LayerMask whatIsWall;
    public Transform rightSide;

    bool onLeftWall = false;
    bool onRightWall = false;

    

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {

        onLeftWall = Physics2D.OverlapCircle(leftSide.position, checkRadius, whatIsWall);
        onRightWall = Physics2D.OverlapCircle(rightSide.position, checkRadius, whatIsWall);

        onWall = onLeftWall || onRightWall;
        if(onWall && double_jump_unlock){
            double_jump = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded || onWall || double_jump)){
            
            if(isGrounded){
                body.velocity = new Vector2(0,1)* jumpSpeed;    
            }
            else if(onWall){
                if(onLeftWall){
                     body.velocity = new Vector2(1,1)* jumpSpeed;
                }
                if(onRightWall){
                     body.velocity = new Vector2(-1,1)* jumpSpeed;
                }
                   
                
            }
            else if(double_jump){
                Instantiate(jumpParticle, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                body.velocity = new Vector2(0,1)* jumpSpeed;
                double_jump = false;
            }
        }

        
        animator.SetFloat("jump", Mathf.Abs(body.velocity.y));
        animator.SetFloat("speed", Mathf.Abs(body.velocity.x));
        
    }
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        /*if(onLeftWall){
            if(moveInput < 0){
                moveInput = 0;
            }
        }
        if(onRightWall){
            if(moveInput > 0){
                moveInput = 0;
            }
        }*/
        //Debug.Log("RightWall " + onRightWall + " "+ moveInput);
        //Debug.Log("LeftWall " + onLeftWall+" "+ moveInput);
        Debug.Log(moveInput);
        body.velocity = new Vector2(moveInput*speed, body.velocity.y);

        Vector3 characterScale = transform.localScale;



        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            characterScale.x = -2;
            Transform c = rightSide;
            rightSide = leftSide;
            leftSide = c;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            characterScale.x = 2;
            Transform c = rightSide;
            rightSide = leftSide;
            leftSide = c;
        }
        transform.localScale = characterScale;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == groundtag){
            isGrounded = true;
            //lastWall = null;
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

    public bool getOnWall ()
    {
        return onWall;
    }
    public bool getIsGrounded()
    {
        return isGrounded;
    }
}