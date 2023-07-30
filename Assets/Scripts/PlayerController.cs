using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerBody;

    public float speed;
    public float jump;

    private bool shiftPressed = false;
    private bool ctrlPressed = false;
    private bool isGrounded = false;

    private void Awake()
    {
        Debug.Log("Player controller awake");
    }
    private void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

    // Update is called once per frame
    private void Update()
    {
         float horizontal = Input.GetAxisRaw("Horizontal");
         float vertical = Input.GetAxis("Jump");
         shiftPressed = Input.GetKey(KeyCode.LeftShift);
         ctrlPressed = (Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightControl)) ;

        MoveCharacter(horizontal,vertical);
        PlayMovementAnimation(horizontal,vertical);

    }

    private void MoveCharacter(float horizontal, float vertical)
        {
            // move character horizontally
            //walk and run
            Vector3 position = transform.position;
            if(shiftPressed)                          
            {
                position.x += horizontal * speed * Time.deltaTime * 2;  //Run
            }
            else
            {
                position.x += horizontal * speed * Time.deltaTime;     //Walk
            }
            transform.position = position; 

            if(vertical > 0 && isGrounded)
            {
                isGrounded = false;
                // move character vertically
                if(shiftPressed){
                    playerBody.AddForce(new Vector2(0f, 1.3f * jump), ForceMode2D.Impulse);
                }
                else{
                    playerBody.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                }

            }
        }

    private void PlayMovementAnimation(float horizontal, float vertical)
    {
        if (shiftPressed)                          // For Run animation, I make speed variable > 1.99, Pressing L.Shift
        {
            horizontal = 2 * horizontal;
        }
         animator.SetBool("IsWalking", (horizontal != 0 ? true : false));
         animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //Flipping the player 
        Vector3 scale = transform.localScale;
        if(horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        } else if(horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        animator.SetBool("IsCrouching", ctrlPressed);

        //Crouch Animation
        if (ctrlPressed)
        {
            playerCollider.offset = new Vector2(-0.12f, 0.6f);
            playerCollider.size = new Vector2(0.9f, 1.3f);
        }
        else
        {
            playerCollider.offset = new Vector2(0f, 0.95f);
            playerCollider.size = new Vector2(0.6f, 2f);
        }

        //Jump Animation
        if( vertical > 0 )
        {
           animator.SetBool("IsJumping", true);
        } else 
        {
            animator.SetBool("IsJumping", false);
        }

    }

}