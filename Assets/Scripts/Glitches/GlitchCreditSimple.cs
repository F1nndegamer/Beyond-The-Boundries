// GlitchCreditSimple.cs
using System.Collections;
using TMPro;
using UnityEngine;

public class GlitchCreditSimple : MonoBehaviour
{
    public TextMeshProUGUI tmp;             
    [TextArea] public string finalText;     
    public float glitchDuration = 1.5f;     
    public float charGlitchInterval = 0.04f; 
    public float revealSpeed = 0.02f;      

    public string glitchChars = "@#%&*?<>/\\|[]{}~—=+-0123456789"; 

    private void Start()
    {
        if (tmp == null) tmp = GetComponent<TextMeshProUGUI>();
        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        string[] lines = finalText.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string targetLine = lines[i];
            tmp.text = new string(' ', targetLine.Length);

            float t = 0f;
            while (t < glitchDuration)
            {
                tmp.text = RandomizeStringLength(targetLine.Length);
                t += charGlitchInterval;
                yield return new WaitForSeconds(charGlitchInterval);
            }


            yield return StartCoroutine(RevealText(targetLine));


            yield return new WaitForSeconds(0.6f);
        }

    }

    string RandomizeStringLength(int length)
    {
        char[] arr = new char[length];
        for (int i = 0; i < length; i++)
            arr[i] = glitchChars[Random.Range(0, glitchChars.Length)];
        return new string(arr);
    }

    IEnumerator RevealText(string target)
    {
        char[] display = new char[target.Length];
        for (int i = 0; i < display.Length; i++) display[i] = ' ';

        for (int i = 0; i < target.Length; i++)
        {

            int glitchCycles = Random.Range(2, 6);
            for (int g = 0; g < glitchCycles; g++)
            {
                display[i] = glitchChars[Random.Range(0, glitchChars.Length)];
                tmp.text = new string(display);
                yield return new WaitForSeconds(charGlitchInterval);
            }

            display[i] = target[i];
            tmp.text = new string(display);
            yield return new WaitForSeconds(revealSpeed);
        }
    }
}
