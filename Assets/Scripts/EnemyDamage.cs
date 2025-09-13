using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float knockbackForce = 10f;  // Push
    public float knockbackForceMulti = 1.1f;  // Push
    public float knockbackDuration = 0.2f; // The player loses control during this period.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

                playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
                knockbackForce *= knockbackForceMulti;

                PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
                if (movement != null)
                {
                    movement.enabled = false;
                    collision.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(ReEnableMovement(movement));
                }
            }
        }
    }

    private System.Collections.IEnumerator ReEnableMovement(PlayerMovement movement)
    {
        yield return new WaitForSeconds(knockbackDuration);
        movement.enabled = true;
    }
}
