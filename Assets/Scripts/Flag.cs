using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class Flag : MonoBehaviour
{
    private Rigidbody2D flagrb;
    bool isfalling;
    public float falltime;
    public GameObject GlitchFor;

    void Awake()
    {
        flagrb = GetComponent<Rigidbody2D>();
        Reset(); // Start frozen
    }

    public void Win()
    {
        isfalling = true;
        Debug.Log("Flag falling");
        GlitchFor.GetComponent<CompositeCollider2D>().isTrigger = true;
        GlitchFor.GetComponent<TilemapCollider2D>().isTrigger = true;
        GlitchFor.GetComponent<SimpleGlitch>().startGlitch = true;
        flagrb.bodyType = RigidbodyType2D.Dynamic;
        flagrb.gravityScale = 1;
        flagrb.constraints = RigidbodyConstraints2D.None;
    }
    void Update()
    {
        if (isfalling)
        {
            falltime += Time.deltaTime;
        }
    }
    public void Reset()
    {
        falltime = 0f;
        flagrb.bodyType = RigidbodyType2D.Static;
        flagrb.gravityScale = 0;
        flagrb.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Flag reset");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ground"))
        {
            Debug.Log("Flag hit the ground");
            if (falltime > 0.5f)
            {
                OSTPlayer.instance.SwitchTracks(false);
                Reset();
                gameObject.GetComponent<FlagWin>().canwin = true;
               this.enabled = false; 
           }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("Flag hit the ground");
            if (falltime > 0.5f)
            {
                Reset();
                gameObject.GetComponent<FlagWin>().canwin = true;
               this.enabled = false; 
           }
        }
    }
}
