using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SecretWall : MonoBehaviour
{
    public enum RevealMode { OnJumpThrough, OnInteract, OnHit }
    public RevealMode revealMode = RevealMode.OnJumpThrough;

    /*[Tooltip("Hangi yönden geçiþe izin verilecek? (ör: Vector2.left = saðdan gelince)")]
    public Vector2 allowedDirection = Vector2.left;*/
    [Tooltip("Will the collider remain closed (permanently) or will it be reopened later?")]
    public bool permanent = true;

    BoxCollider2D box;

    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (revealMode == RevealMode.OnHit) Reveal();
            /*else if (revealMode == RevealMode.OnJumpThrough)
            {
                Vector2 contactNormal = col.contacts.Length > 0 ? col.contacts[0].normal : Vector2.zero;
                float dot = Vector2.Dot(contactNormal, allowedDirection.normalized);
                if (dot > 0.5f)
                {
                    var player = col.gameObject.GetComponent<PlayerMovement>(); // örnek
                    bool isJumping = player != null && player.isJumping;
                    if (isJumping) Reveal();
                }
            }*/ // didn't work
        }
    }

    public void Reveal()
    {
        if (box == null) return;
        box.isTrigger = true;
        //effect, animation, sprite replacement
        var sr = GetComponent<SpriteRenderer>();
        if (sr) sr.color = new Color(1f, 1f, 1f, 0.5f);

        if (!permanent)
        {
            Invoke(nameof(Close), 1f);
        }
    }

    void Close()
    {
        if (box) box.isTrigger = false;
        var sr = GetComponent<SpriteRenderer>();
        if (sr) sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
