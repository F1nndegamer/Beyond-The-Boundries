using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;
using URPGlitch;

public class SceneGlitchTransition : MonoBehaviour
{
    public Volume volume;
    AnalogGlitchVolume glitchVolume;
    DigitalGlitchVolume digital;
    public float transitionTime = 1f;

    private void Start()
    {
        if (volume.profile.TryGet(out AnalogGlitchVolume analogGlitchVolume))
        {
            glitchVolume = analogGlitchVolume;
        }
        if (volume.profile.TryGet(out DigitalGlitchVolume digitalGlitchVolume))
        {
            digital = digitalGlitchVolume;
        }
    }

    public IEnumerator GlitchTransition(int sceneName)
    {
        float t = 0f;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            glitchVolume.colorDrift.value = t / transitionTime;
            glitchVolume.scanLineJitter.value = t / transitionTime;
            glitchVolume.horizontalShake.value = t / transitionTime;
            glitchVolume.colorDrift.value = t / transitionTime;
            digital.intensity.value = t / transitionTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        t = transitionTime;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            yield return null;
        }
    }
}
