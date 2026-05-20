
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter teleport;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position1 = teleport.transform.position;
        Vector3 position = position1;
        collision.transform.position = position;
    }
}
