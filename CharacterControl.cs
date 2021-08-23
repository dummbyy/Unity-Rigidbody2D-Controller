using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    // Components
    Rigidbody2D _rb;
    Animator animator;

    // Movement Variables
    [SerializeField] float characterSpeed = 10f;
    private float moveInput;
    private bool faceRight = true;
    
    // Jumping Variables
    [SerializeField]  Transform checkTheGround;
    [SerializeField]  float checkRadius;
    [SerializeField]  LayerMask whatIsGround;
    [SerializeField]  int playerExtraJumpsValue;
    [SerializeField]  float jumpForce = 1f;
    private bool isPlayerGrounded;
    private int playerExtraJumps;
    private bool canMove = true;
    [SerializeField] Transform resPosition;
    
    void Start()
    {
        playerExtraJumps = playerExtraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (transform.position.y < -6)
        {
            canMove = false;
            transform.position = resPosition.position;
            StartCoroutine(WaitForCam());
        }
        Move();
    }

    void Update(){
        isPlayerGrounded = Physics2D.OverlapCircle(checkTheGround.position, checkRadius, whatIsGround);
        
        if(isPlayerGrounded){
            playerExtraJumps = playerExtraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && playerExtraJumps > 0){
            _rb.velocity = Vector2.up * jumpForce;
            playerExtraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && playerExtraJumps > 0){
            _rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        //if (moveInput > 0 || moveInput < 0) animator.SetBool("isRunning", true);
        //else animator.SetBool("isRunning", false);
        if (canMove)
        {
            _rb.velocity = new Vector2(moveInput * characterSpeed, _rb.velocity.y);
        }
        
        if(faceRight == false && moveInput > 0) Flip();
        else if(faceRight == true && moveInput < 0) Flip();
    }
    void Flip(){ // Flip 
        faceRight = !faceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator WaitForCam()
    {
        yield return new WaitForSeconds(2f);
        canMove = true;
    }
}
