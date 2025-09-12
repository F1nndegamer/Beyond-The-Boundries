using TMPro;
using UnityEngine;

public class SimpleGlitch : MonoBehaviour
{
    SpriteRenderer sr;
    Vector3 originalPos;
    Color originalColor;

    public float glitchChance = 0.05f; 
    public float glitchOffset = 0.1f;  
    public float glitchTime = 0.05f;
    public bool startGlitch = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalPos = transform.localPosition;
        originalColor = sr.color;
    }

    void Update()
    {
        if (startGlitch & Random.value < glitchChance)
            StartCoroutine(DoGlitch());
        else if (!startGlitch)
            GetComponent<Collider2D>().enabled = true;
    }

    System.Collections.IEnumerator DoGlitch()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.localPosition = originalPos + new Vector3(Random.Range(-glitchOffset, glitchOffset), 0, 0);

        sr.color = new Color(Random.value, Random.value, Random.value);

        yield return new WaitForSeconds(glitchTime);

        transform.localPosition = originalPos;
        sr.color = originalColor;
    }
}
