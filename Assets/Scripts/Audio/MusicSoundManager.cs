using System;
using UnityEngine;

public class MusicSoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private UIMusicList[] _musicList;
    private static MusicSoundManager instance;
    private AudioSource _audioSource;
    private AudioClip _musicOnPlay;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void SelectMusicToPlay(int musicIndex = 0)
    {
        AudioClip clip = instance._musicList[musicIndex].Music;
        instance._musicOnPlay = clip;
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

}

[Serializable]
public struct UIMusicList
{
    public AudioClip Music { get => _music; }
    public string name;
    [SerializeField] private AudioClip _music;
}