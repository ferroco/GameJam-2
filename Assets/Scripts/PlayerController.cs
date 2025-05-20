using UnityEngine;
using System.Collections;

public class TilePlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float tileSize = 1f;
    public LayerMask obstacleLayer;
    public LayerMask pushableLayer;

    public AudioSource footstepAudioSource;
    public AudioSource pushAudioSource;
    private bool isMoving = false;
    private Vector2 input;
    private Animator animator;
    private Coroutine currentMovement; // 👉 Para detener corutinas de movimiento

    private void Awake()
    {
        // Puedes inicializar variables aquí si necesitas
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isMoving) return;

        input = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            input = Vector2.right;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            input = Vector2.left;
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            input = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            input = Vector2.down;

        if (input == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        Vector3 destination = transform.position + new Vector3(input.x, input.y, 0f) * tileSize;

        // Check pushable object
        Collider2D pushable = Physics2D.OverlapCircle(destination, 0.1f, pushableLayer);
        if (pushable != null)
        {
            Vector3 pushablePos = pushable.transform.position;
            float toleranceX = 0.05f;
            float toleranceY = 0.1f;
            bool canPush = false;

            if ((input == Vector2.up || input == Vector2.down) && Mathf.Abs(pushablePos.x - transform.position.x) < toleranceX)
                canPush = true;

            if ((input == Vector2.left || input == Vector2.right) && Mathf.Abs(pushablePos.y - transform.position.y) < toleranceY)
                canPush = true;

            if (canPush)
            {
                PushableObject po = pushable.GetComponent<PushableObject>();
                if (po != null && po.CanMove(input))
                {
                    PlayPushSound();
                    currentMovement = StartCoroutine(PushAndMove(po, input, destination));
                    return;
                }
            }
        }

        if (!Physics2D.OverlapCircle(destination, 0.1f, obstacleLayer | pushableLayer))
        {
            float speed = Input.GetKey(KeyCode.X) ? runSpeed : walkSpeed;
            PlayFootstepSound();
            currentMovement = StartCoroutine(MoveTo(destination, speed));
        }
    }

    IEnumerator PushAndMove(PushableObject obj, Vector2 dir, Vector3 playerDestination)
    {
        isMoving = true;
        StartCoroutine(obj.Move(dir));

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        while ((playerDestination - transform.position).sqrMagnitude > 0.0001f)
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

    // 👉 MÉTODO PARA TELETRANSPORTAR
    public void Teleport(Vector3 newPosition)
    {
        if (currentMovement != null)
        {
            StopCoroutine(currentMovement);
            currentMovement = null;
        }

        isMoving = false;
        transform.position = newPosition;

        if (animator != null)
        {
            animator.SetBool("isMoving", false);
        }
    }

    void PlayFootstepSound()
    {
        if (footstepAudioSource != null && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
    }

    void PlayPushSound()
    {
        if (pushAudioSource != null)
        {
            pushAudioSource.Play();
        }
    }
}
