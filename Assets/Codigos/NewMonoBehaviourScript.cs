using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    private  float moveX;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded;

    public Transform visual;
    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        anim = visual.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if  (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        anim.SetBool("IsRunning", Mathf.Abs(moveX) > 0f && isGrounded);
        if (moveX > 0.01f)
        {
            visual.localScale = new Vector3(12, 12, 2);
        }
        else if (moveX < -0.01f)
        {
            visual.localScale = new Vector3(-12, 12, 2);
        }

     Move();
    }

    void Move()
    {
        rb2d.linearVelocity = new  Vector2(moveX * moveSpeed, rb2d.linearVelocity.y);
    }

    void Jump()
    {
    rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
    }
}
        
        

