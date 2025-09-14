using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OSTPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip musicClip;
    public string volumePrefKey = "sound";
    public float defaultVolume = 0.7f;
    private AudioSource audioSource;
    public static OSTPlayer instance;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            this.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource.clip = musicClip;
        audioSource.loop = true;
        float savedVolume = PlayerPrefs.HasKey(volumePrefKey) ? PlayerPrefs.GetFloat(volumePrefKey) : defaultVolume;
        audioSource.volume = savedVolume;

        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(volumePrefKey, volume);
        PlayerPrefs.Save();
    }
}
