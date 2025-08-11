using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider effectSlider;

        void Start()
    {
        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 0f);
        float savedEffect = PlayerPrefs.GetFloat("EffectVolume", 0f);

        bgmSlider.value = savedBGM;
        effectSlider.value = savedEffect;

        SoundManager.Instance.SetVolume(SoundType.BGM, savedBGM);
        SoundManager.Instance.SetVolume(SoundType.EFFECT, savedEffect);

        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        effectSlider.onValueChanged.AddListener(OnEffectVolumeChanged);
    }

    private void OnBGMVolumeChanged(float value)
    {
        SoundManager.Instance.SetVolume(SoundType.BGM, value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    private void OnEffectVolumeChanged(float value)
    {
        SoundManager.Instance.SetVolume(SoundType.EFFECT, value);
        PlayerPrefs.SetFloat("EffectVolume", value);
    }
}
