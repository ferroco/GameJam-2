using UnityEngine;

public class PilaContacto : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pila"))
        {
            CircleCollider2D collider = collision.GetComponent<CircleCollider2D>();
            collider.enabled = false;
            collision.transform.position = transform.position;
            Debug.Log("toca");
            PasarNivel pasarNivel = FindAnyObjectByType<PasarNivel>();
            if (pasarNivel != null)
            {
                pasarNivel.energiaPuerta++;
            }
        }
    }
}
