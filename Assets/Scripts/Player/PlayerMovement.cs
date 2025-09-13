using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float jumpForce = 12f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    public bool isJumping { get; private set; }
    [Header("Win Condition Stage 1")]
    public TextMeshProUGUI InteractionText;
    public bool isInRange = false;
    public bool KeyPressed = true;
    public GameObject Flag;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
        else if (isGrounded)
        {
            isJumping = false;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            KeyPressed = true;
            InteractionText.gameObject.SetActive(false);
            Flag flag = Flag.GetComponent<Flag>();
            flag.Fall();
        }
    }
    public void TakeDamage(Vector2 knockback)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero; // reset current movement
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D Trigger)
    {
        if (Trigger.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal reached!");
            InteractionText.gameObject.SetActive(true);
            isInRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D Trigger)
    {
        if (Trigger.gameObject.CompareTag("Goal"))
        {
            InteractionText.gameObject.SetActive(false);
            isInRange = false;
        }
    }
}
