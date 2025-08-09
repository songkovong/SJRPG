using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BGM,
    EFFECT
}

//https://bonnate.tistory.com/183#%E2%9C%85%20%EA%B5%AC%ED%98%84%201-1
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Audio Mixer
    [SerializeField] private AudioMixer mixer;

    // BGM, effect volume
    private float BGMVolume, EffectVolume;

    // Dictionary for audio clips
    private Dictionary<string, AudioClip> clipsDictionary;

    // preloaded clips
    [SerializeField] private AudioClip[] preloadClips;

    // temporary sound lists
    private List<TemporarySoundPlayer> tempSounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        clipsDictionary = new Dictionary<string, AudioClip>();

        foreach (var clip in preloadClips)
        {
            clipsDictionary.Add(clip.name, clip);
        }

        tempSounds = new List<TemporarySoundPlayer>();
    }

    // Get Audio Clip for name
    private AudioClip GetClip(string clipName)
    {
        AudioClip clip = clipsDictionary[clipName];

        if (clip == null)
        {
            Debug.LogError(clipName + "is not exist.");
        }

        return clip;
    }

    // 사운드를 재생할 때, 루프 형태로 재생된경우에는 나중에 제거하기위해 리스트에 저장한다.
    private void AddToList(TemporarySoundPlayer soundPlayer)
    {
        tempSounds.Add(soundPlayer);
    }

    // Stop Loop Sound
    public void StopLoopSound(string clipName)
    {
        foreach (TemporarySoundPlayer audioPlayer in tempSounds)
        {
            if (audioPlayer.ClipName == clipName)
            {
                tempSounds.Remove(audioPlayer);
                Destroy(audioPlayer.gameObject);
                return;
            }
        }

        Debug.LogWarning(clipName + " is not founded.");
    }

    // Play audio clip to 2d
    public void Play2DSound(string clipName, float delay = 0f, bool isLoop = false, SoundType type = SoundType.EFFECT)
    {
        GameObject obj = new GameObject("TemporarySoundPlayer 2D");
        TemporarySoundPlayer soundPlayer = obj.AddComponent<TemporarySoundPlayer>();

        if (isLoop)
        {
            AddToList(soundPlayer);
        }

        soundPlayer.InitSound2D(GetClip(clipName));
        soundPlayer.Play(mixer.FindMatchingGroups(type.ToString())[0], delay, isLoop);
    }

    // Play audio clip to 3d
    public void Play3DSound(string clipName, Transform audioTarget, float delay = 0f, bool isLoop = false, SoundType type = SoundType.EFFECT, bool attachToTarget = true, float minDistance = 0.0f, float maxDistance = 50.0f)
    {
        GameObject obj = new GameObject("TemporarySoundPlayer 3D");
        obj.transform.localPosition = audioTarget.transform.position;

        if (attachToTarget)
        {
            obj.transform.parent = audioTarget;
        }

        TemporarySoundPlayer soundPlayer = obj.AddComponent<TemporarySoundPlayer>();

        if (isLoop)
        {
            AddToList(soundPlayer);
        }

        soundPlayer.InitSound3D(GetClip(clipName), minDistance, maxDistance);
        soundPlayer.Play(mixer.FindMatchingGroups(type.ToString())[0], delay, isLoop);
    }

    // If load scene, manager init all volumes
    public void InitVolumes(float bgm, float effect)
    {
        SetVolume(SoundType.BGM, bgm);
        SetVolume(SoundType.EFFECT, effect);
    }

    // Set Volume for setting
    public void SetVolume(SoundType type, float value)
    {
        mixer.SetFloat(type.ToString(), value);
    }

    // Random?
}
