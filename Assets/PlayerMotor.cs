using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_motor : MonoBehaviour
{
    Vector2 direction;
    new Rigidbody2D rigidbody2D;
    public float speed = 10;
    public float jumpForce = 10;
    public float maxSpeed = 5;
    public float stoppingForce = 5;
    public float dashforce = 10;
    private bool canJump = true;
    private bool canDash = true;
    private bool canDoubleJump;
    private Animator animator;
    private float initScale;


    public int maxJump = 2;
    private int currentjump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initScale = transform.localScale.x;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        animator.SetFloat("SpeedY", rigidbody2D.linearVelocityY);
        //(check if moving right)
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(initScale, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-initScale, transform.localScale.y, transform.localScale.z);
        }
        HandlePlayerMovement();
        MaxSpeedLimiting();
    }


    private void HandlePlayerMovement()
    {
        if (direction.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(direction.x * speed, 0));
            animator.SetBool("is moving", true);
        }
        else if (rigidbody2D.linearVelocity.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(-rigidbody2D.linearVelocityX * stoppingForce, 0));
            animator.SetBool("is moving", false);

        }
    }

    private void MaxSpeedLimiting()
    {
        if (!canDash)
        {
            return;
        }
        if (rigidbody2D.linearVelocityX >= maxSpeed)
        {
            rigidbody2D.linearVelocityX = maxSpeed;
        }
        else if (rigidbody2D.linearVelocityX <= -maxSpeed)
        {
            rigidbody2D.linearVelocityX = -maxSpeed;
        }
        //transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
    }






    void OnMove(InputValue value)
    {
        //Debug.Log("Move");
        // Debug.Log(value.Get<Vector2>());
        direction = value.Get<Vector2>();
    }


    private void OnJump()
    {
        if (canJump)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("is jumping", true);
            canJump = false;
        }
        else if (canDoubleJump)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce * 0.5f, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }


    private void OnDash()
    {
        if (canDash)
        {
            if (direction.x != 0)
            {
                rigidbody2D.AddForce(new Vector2(direction.x * dashforce, 0), ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(dashforce, 0), ForceMode2D.Impulse);
            }
            canDash = false;
            StartCoroutine(ResetDash(1));
        }
    }

    IEnumerator ResetDash(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        canDoubleJump = true;
        animator.SetBool("is jumping", false);
    }
}
