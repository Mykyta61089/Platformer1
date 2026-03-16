using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
    }
    private void OnAnimatorMove()
    {
    
    }

    void OnMove(InputValue Value)
    {
        //Debug.log("Move");
        //Debug.Log(Value.Get<Vector2>());
        direction = Value.Get<Vector2>();         
    }
}
