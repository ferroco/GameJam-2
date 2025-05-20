using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportPosition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BoxCollider2D collider = collision.GetComponent<BoxCollider2D>();
            TilePlayerController tilePlayerController = collision.GetComponent<TilePlayerController>();
            tilePlayerController.Teleport(teleportPosition.position);
        }
    }
}
