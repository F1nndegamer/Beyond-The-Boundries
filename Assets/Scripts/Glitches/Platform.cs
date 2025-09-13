using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CrusherBlock2D : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float pushForce = 5000f;

    private Vector2 targetPos;
    private bool movingToB = true;
    private Vector2 moveDir;

    void Start()
    {
        targetPos = pointB.position;
    }

    void FixedUpdate()
    {
        moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            movingToB = !movingToB;
            targetPos = movingToB ? pointB.position : pointA.position;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.rigidbody;
            if (playerRb != null)
            {
                playerRb.linearVelocity = moveDir * moveSpeed;
                playerRb.AddForce(moveDir * pushForce * Time.fixedDeltaTime, ForceMode2D.Force);
            }
        }
    }
}
