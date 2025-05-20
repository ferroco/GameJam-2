using UnityEngine;
using System.Collections;

public class PushableObject : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float tileSize = 1f;
    public LayerMask obstacleLayer;

    private bool isMoving = false;

    /// <summary>
    /// Verifica si el objeto puede moverse en la dirección deseada sin colisionar con obstáculos.
    /// </summary>
    public bool CanMove(Vector2 direction)
    {
        if (isMoving) return false;

        Vector3 destination = transform.position + (Vector3)(direction * tileSize);
        // Revisa si hay colisión con obstáculo en el destino
        return !Physics2D.OverlapCircle(destination, 0.05f, obstacleLayer);
    }

    /// <summary>
    /// Mueve el objeto suavemente hacia la dirección especificada, si es posible.
    /// </summary>
    public IEnumerator Move(Vector2 direction)
    {
        if (isMoving) yield break;

        Vector3 destination = transform.position + (Vector3)(direction * tileSize);

        // Verifica otra vez antes de mover (por si CanMove no se llamó antes)
        if (Physics2D.OverlapCircle(destination, 0.05f, obstacleLayer))
            yield break;

        isMoving = true;

        while ((destination - transform.position).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }
}
