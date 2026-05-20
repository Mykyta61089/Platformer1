using System.Collections;
using TMPro;
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

    public int maxJump = 2;
    private int currentjump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _intscale = transform.localScale.x;
    }

    private void FixedUpdate()
    { 
    {
      if(direction.x > 0)
            {
                transform.localScale = new Vector3(_intscale, transform.localScale.y, transform.localScale.z);
            }
      else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-_intscale, transform.localScale.y, transform.localScale.z);
            }
        MovePlayer();
        LimitMaxSpeed();
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

    private void MovePlayer()
    {
        if (direction.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(direction.x * acceleration, 0))
                _animator.Setbool("IsMoving", true);
        }
        else if (_rigidbody2D.linearVelocityX != 0)
        {
            if (_rigidbody2D.linearVelocityX < stoppingPoint && _rigibody2D.linearVelocityX > -stoppingPoint)
            {
                _rigibody2D.linearVelocity = new Vector2(0.0f, _rigibody2D.linearVelocityY);
            }
            else
            {
                _rigibody2D.AddForce(new Vector2(-_rigibody2D.linearVelocityX * stoppingForce, 0));
            }
        }

        if (direction.x == 0)
        {
            _animator.SetBool("isMoving", false)
        }
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
            currentjump++;
            if (currentjump >= maxJump)
            {
                canJump = false;
            }
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