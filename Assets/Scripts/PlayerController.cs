using UnityEngine;
using System.Collections;

public class TilePlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float tileSize = 1f;
    public LayerMask obstacleLayer;
    public LayerMask pushableLayer;

    private bool isMoving = false;
    private Vector2 input;
    private Animator animator;

    private void Awake()
    {
       // animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (input.x != 0) input.y = 0;
            if(input.x == 0 && input.y == 0){ animator.SetBool("isMoving", false); }else
            {animator.SetBool("isMoving", true);}

            Debug.Log(input.x + " " + input.y);
            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                Vector3 destination = transform.position + new Vector3(input.x, input.y, 0f)*tileSize ;

                // Check for pushable object
                Collider2D pushable = Physics2D.OverlapCircle(destination, 0.1f, pushableLayer);
                if (pushable != null)
                {
                    PushableObject po = pushable.GetComponent<PushableObject>();
                    if (po != null && po.CanMove(input))
                    {
                        StartCoroutine(PushAndMove(po, input, destination));
                        return;
                    }
                }

                // Move player if there's no obstacle
                if (!Physics2D.OverlapCircle(destination, 0.1f, obstacleLayer | pushableLayer))
                {
                    float speed = Input.GetKey(KeyCode.X) ? runSpeed : walkSpeed;
                    StartCoroutine(MoveTo(destination, speed));
                }
            }
        }
       // animator.SetBool("isMoving", isMoving);
    }

    IEnumerator PushAndMove(PushableObject obj, Vector2 dir, Vector3 playerDestination)
    {
        isMoving = true;

        // Start push coroutine
        StartCoroutine(obj.Move(dir));

        // Move player after a short delay to match object push timing
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        while ((playerDestination - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerDestination, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = playerDestination;
        isMoving = false;
    }

    IEnumerator MoveTo(Vector3 destination, float speed)
    {
        isMoving = true;

        while ((destination - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }
}
