using UnityEngine;

public class Flag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fall()
    {
        Debug.Log("Flag falling");
        Rigidbody2D flagrb = GetComponent<Rigidbody2D>();
        flagrb.bodyType = RigidbodyType2D.Dynamic;
        flagrb.gravityScale = 1;
            flagrb.constraints = RigidbodyConstraints2D.None;
        }
    public void Reset()
    {
        Rigidbody2D flagrb = GetComponent<Rigidbody2D>();
        flagrb.bodyType = RigidbodyType2D.Static;
        flagrb.gravityScale = 0;
        flagrb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Reset();
        }
    }
}
