using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button settingButton;
    public Button quitButton;
    public Button settingExitButton;
    public GameObject settingCanvas;
    public GameObject mainmenuCanvas;
    public bool effect;
    public Toggle toggle;
    public Slider slider;
    public Renderer2DData rendererData;
    public SceneGlitchTransition sceneGlitchTransition;
    public string prefSound = "sound";
    void Start()
    {
        playButton.onClick.AddListener(Play);
        settingButton.onClick.AddListener(Setting);
        quitButton.onClick.AddListener(Quit);
        settingExitButton.onClick.AddListener(SettingExit);
        toggle.onValueChanged.AddListener(Effect);
        slider.onValueChanged.AddListener(Sound);
        settingCanvas.SetActive(false);
        mainmenuCanvas.SetActive(true);
        slider.value = PlayerPrefs.HasKey(prefSound) ? PlayerPrefs.GetFloat(prefSound) : 0.7f;
    }
    public void Play()
    {
        StartCoroutine(sceneGlitchTransition.GlitchTransition());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            settingCanvas.SetActive(false);
            mainmenuCanvas.SetActive(true);
        }
    }
    public void Setting()
    {
        settingCanvas.SetActive(true);
        mainmenuCanvas.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
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
        OSTPlayer.instance.SetVolume(v);
        PlayerPrefs.SetFloat(prefSound, v);
        PlayerPrefs.Save();
    }
    public void SettingExit()
    {
        settingCanvas.SetActive(false);
        mainmenuCanvas.SetActive(true);
    }
}
