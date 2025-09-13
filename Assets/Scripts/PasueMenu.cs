using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PasueMenu : MonoBehaviour
{
    bool isOpen = false;
    public GameObject pause;
    public bool effect;
    public Toggle toggle;
    public Slider slider;
    public Renderer2DData rendererData;
    public string prefSound = "sound";

    private void Start()
    {
        toggle.onValueChanged.AddListener(Effect);
        slider.onValueChanged.AddListener(Sound);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isOpen)
        {
            Time.timeScale = 0;
            isOpen = true;
            pause.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            Time.timeScale = 1;
            isOpen = false;
            pause.SetActive(false);
        }
    }
    public void Effect(bool e)
    {
        foreach (var item in rendererData.rendererFeatures)
        {
            if (item == null) continue;
            if (item.name == "Full Screen" || item.GetType().Name.ToLower().Contains("glitch"))
            {
                item.SetActive(e);
                Debug.Log($"Feature '{item.name} {e}");
            }
        }
    }
    public void Sound(float v)
    {
        PlayerPrefs.SetFloat(prefSound, v);
    }
}
