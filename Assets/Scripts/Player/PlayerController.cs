using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    //Movement variables
    public float speed;
    public float jumpForce;

    //Groundcheck
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;


    Coroutine jumpForceChange;

    public int maxLives = 5;
    private int _lives = 3;

    public int lives
    {
       get { return _lives; }
       set
        { _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
                Debug.Log("Lives have been set to: " + _lives.ToString());
            }
        }
    }

    private int _score = 0;

    public int score
    {
        get { return _score; }
        set
        { _score = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrectly, defaulting to " + speed.ToString());
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
            Debug.Log("Jump Force was set incorrectly, defaulting to " + jumpForce.ToString());
        }
        
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("Ground Check Radius was set incorrectly, defaulting to " + groundCheckRadius.ToString());
        }
        
        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check not set, finding it manually");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
       
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }
        
        //Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        //rb.velocity = moveDirection;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("hInput", Mathf.Abs(hInput));

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Fire")
            {
                anim.SetTrigger("Fire");
            }
            else if (curPlayingClip[0].clip.name == "Fire")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
               Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
                
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isGrounded)
        {
            anim.SetTrigger("JumpAttack");
        }

        if (hInput < 0)
        {
            sr.flipX = true;
        }
        if (hInput > 0)
        {
            sr.flipX = false;
        }

        if (isGrounded)
            rb.gravityScale = 1;
    }

    public void increaseGravity()
    {
        rb.gravityScale = 5;
    }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }
    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }
}
