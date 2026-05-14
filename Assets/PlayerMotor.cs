using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_motor : MonoBehaviour
{
    Vector2 direction;
    Rigidbody2D rigidbody2D;
    public float speed = 10;
    public float jumpForce = 10;
    public float maxSpeed = 5;
    public float stoppingForce = 5;
    public float dashforce = 10;
    private bool canJump = true;
    private bool canDash = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(direction.x * speed, 0));
        }
        else if (rigidbody2D.linearVelocity.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(-rigidbody2D.linearVelocityX * stoppingForce, 0));
        }


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
            canJump = false;
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
    }
}