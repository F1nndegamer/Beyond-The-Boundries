using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HintsCont : MonoBehaviour
{
    public TextMeshProUGUI hintsText;
    private List<String> hints = new List<string>
    {
        "Really, Are you asking for Hints? That's sad.",
        "HMMM That wall part looks different...",
        "20 headhitters in a row? Impressive!",
        "That spike trap looks weird, maybe you can jump on it?",
        "A moving platform? There are 2 sides top and bottom, try both.",
        "That wall looks, jumpable?",
        "Are you telling me the pause menu lets you jump?",
        "Remember, R restarts the level, well normally at least.",
        "I could build up alot of momentum here if I fall from a height.",
        "The developers are lazy, they didn't stop the gravity when paused.",
    };
    void Start()
    {
        hintsText = GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < hints.Count; i++)
        {
            if (i == SceneManager.GetActiveScene().buildIndex - 1)
            {
                hintsText.text = hints[i];
                break;
            }
        }
    }
}
