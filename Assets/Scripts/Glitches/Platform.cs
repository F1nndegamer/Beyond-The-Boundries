using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class CrusherBlock2D : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;

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
            // Start coroutine once when player overlaps too deeply
            foreach (var contact in collision.contacts)
            {
                if (contact.separation < -0.1f) 
                {
                    Collider2D crusherCol = GetComponent<Collider2D>();
                    Collider2D playerCol = collision.collider;
                    Rigidbody2D playerRb = collision.rigidbody;

                    StartCoroutine(TemporarilyIgnoreCollision(playerCol, crusherCol, playerRb));
                    break;
                }
            }
        }
    }

    private IEnumerator TemporarilyIgnoreCollision(Collider2D playerCol, Collider2D crusherCol, Rigidbody2D playerRb)
    {
        Physics2D.IgnoreCollision(playerCol, crusherCol, true);

        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -15f);

        yield return new WaitForSeconds(0.2f);

        Physics2D.IgnoreCollision(playerCol, crusherCol, false);
    }
}
