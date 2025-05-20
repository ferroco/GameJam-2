using UnityEngine;
using System.Collections;

public class PushableObject : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float tileSize = 1f;
    public LayerMask obstacleLayer;

    private bool isMoving = false;

    public bool CanMove(Vector2 direction)
    {
        Vector3 destination = transform.position + (Vector3)(direction * 0.5f)/3;

        // Check if the destination has any obstacle or another pushable
        return !Physics2D.OverlapCircle(destination, 0.1f, obstacleLayer);
    }

    public IEnumerator Move(Vector2 direction)
    {
        isMoving = true;
        Vector3 destination = transform.position + (Vector3)(direction * 0.5f)/3;

        /*  while ((destination - transform.position).sqrMagnitude > Mathf.Epsilon)
          {
              transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
              yield return null;
          }*/
        while ((destination - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }
}