using UnityEngine;

public class PasarNivel : MonoBehaviour
{
    public SpriteRenderer puerta;
    public int energiaPuerta;
    private BoxCollider2D boxCollider;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = puerta.GetComponent<Animator>();
        boxCollider = puerta.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        energiaPuerta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (energiaPuerta >= 2)
        {
            boxCollider.enabled = true;
            anim.SetBool("doorOpen", true);
        }
    }
}
