
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter teleport;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = teleport.transform.position;
    }
}
