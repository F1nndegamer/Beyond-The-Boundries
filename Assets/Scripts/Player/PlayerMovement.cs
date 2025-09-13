using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
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
    [Header("Jump Bug")]
    private Queue<float> jumpTimes = new Queue<float>();
    public float spamWindow = 2f;
    public int spamThreshold = 5;
    public bool touchingWall = false;
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
        if ((isGrounded || (SceneManager.GetActiveScene().name == "Level6" && Time.timeScale == 0f)) && Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "Level6" && Time.timeScale == 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isJumping = true;
                return;
            }
            else
            {
                Debug.Log(Time.timeScale);
            }

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;

            jumpTimes.Enqueue(Time.time);

            while (jumpTimes.Count > 0 && Time.time - jumpTimes.Peek() > spamWindow)
            {
                jumpTimes.Dequeue();
            }

            if (jumpTimes.Count >= spamThreshold)
            {
                Debug.Log("Player is spamming jumps!");
                StartCoroutine(ShootColliders());
                jumpTimes.Clear();
            }
        }

        else if (isGrounded)
        {
            isJumping = false;
        }
        if (SceneManager.GetActiveScene().name == "Level5")
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) || touchingWall;
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            KeyPressed = true;
            InteractionText.gameObject.SetActive(false);
            Flag flag = Flag.GetComponent<Flag>();
            if (flag != null) flag.Win();
            FlagWin flagWin = Flag.GetComponent<FlagWin>();
            flagWin.Win();
        }
    }
    public void TakeDamage(Vector2 knockback)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero; // reset current movement
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
    private IEnumerator ShootColliders(float duration = 0.5f)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Ground"), true);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 20f);

        yield return new WaitForSeconds(duration);

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Ground"), false);
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
    void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                if (Mathf.Abs(normal.x) > 0.5f)
                {
                    touchingWall = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            touchingWall = false;
        }
    }
}
