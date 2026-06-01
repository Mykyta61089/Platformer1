using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_motor : MonoBehaviour
{
    Vector2 direction;
    new Rigidbody2D rigidbody2D;
    public float speed = 10;
    public float jumpForce = 5;
    public float maxSpeed = 5;
    public float stoppingForce = 5;
    public float dashforce = 10;
    public float stoppingforce = 10;
    public float skoki = 2;
    private bool canJump = true;
    private bool canDash = true;
    private bool canDoubleJump;
    private Animator animator;
    private float initScale;
    private float dashbar;

    public int maxJump = 2;
    private int currentjump;
    public delegate void OnDashInitializedHandler(float dashbar);
    public event OnDashInitializedHandler OnDashInitialized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        dashbar = 1;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initScale = transform.localScale.x;
        OnDashInitialized?.Invoke(dashbar);
    }


    private void FixedUpdate()
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(initScale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-initScale, transform.localScale.y, transform.localScale.z);
        }
        PlayerHandelingXMovement();
        MaxSpeedLimiting();
        animator.SetFloat("SpeedY", rigidbody2D.linearVelocityY);
    }

    private void PlayerHandelingXMovement()
    {
        if (direction.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(direction.x * speed, 0));
            animator.SetBool("IsMoving", true);
        }
        else if (rigidbody2D.linearVelocityX != 0)
        {
            rigidbody2D.AddForce(new Vector2(-rigidbody2D.linearVelocityX * stoppingforce, 0));
        }

        if (direction.x == 0)
        {
            animator.SetBool("IsMoving", false);
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

    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnJump()
    {


        if (canJump)
        {
            //Debug.Log("Jump");
            rigidbody2D.AddForce(Vector2.up * 10 * jumpForce, ForceMode2D.Impulse);
            skoki--;

        }



        if (skoki == 0)
        {
            canJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        skoki = 2;


        if (skoki != 0)
        {
            canJump = true;
        }
    }

    private void OnDash()
    {
        // Debug.Log("Dashing");

        if (canDash)
        {
            if (direction.x != 0)
            {
                rigidbody2D.AddForce(new Vector2(direction.x * dashforce, 0), ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(direction.x * dashforce, 0), ForceMode2D.Impulse);
            }

            StartCoroutine(ResetDash(1));
            dashbar--;
            OnDashInitialized?.Invoke(dashbar);
            canDash = false;
        }

        IEnumerator ResetDash(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            dashbar++;
            canDash = true;
            OnDashInitialized?.Invoke(dashbar);
        }


    }
}
