using System;
using UnityEngine;

public enum UISoundType
{
    MUSIC,
    UI_SFX,
    AUX
}
public class UISoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] _soundList;
    private static UISoundManager instance;
    private AudioSource _audioSource;
    private AudioClip _musicOnPlay;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayRandomUISound(UISoundType soundType, float volume = 1)
    {
        AudioClip[] clips = instance._soundList[(int)soundType].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance._audioSource.PlayOneShot(randomClip, volume);
    }

    public static void SelectMusicToPlay(int musicIndex = 0)
    {
        instance._musicOnPlay = instance._soundList[(int)UISoundType.MUSIC].Sounds[musicIndex];
    }
    public static void PlayMusicPlayer()
    {
        instance._audioSource.clip = instance._musicOnPlay;
        instance._audioSource.loop = true;
        instance._audioSource.Play();
    }
    public static void SetVolumeMusicPlayer(float volume) { instance._audioSource.volume = volume; }
    public static void PauseMusicPlayer() { instance._audioSource.Pause(); }
    public static void UnpauseMusicPlayer() { instance._audioSource.UnPause(); }
    public static void StopMusicPlayer() { instance._audioSource.Stop(); }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(UISoundType));
        Array.Resize(ref _soundList, names.Length);
        for (int i = 0; i < _soundList.Length; i++)
        {
            _soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeReference] private AudioClip[] sounds;
}