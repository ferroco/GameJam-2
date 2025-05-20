using System;
using UnityEngine;

public class MusicSoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private UIMusicList[] _musicList;
    private AudioSource _audioSource;
    private AudioClip _musicOnPlay;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public  void SelectMusicToPlay(int musicIndex = 0)
    {
        AudioClip clip = _musicList[musicIndex].Music;
        _musicOnPlay = clip;
    }
    public void PlayMusicPlayer()
    {
        _audioSource.clip = _musicOnPlay;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    public void SetVolumeMusicPlayer(float volume) { _audioSource.volume = volume; }
    public void PauseMusicPlayer() { _audioSource.Pause(); }
    public void UnpauseMusicPlayer() { _audioSource.UnPause(); }
    public void StopMusicPlayer() { _audioSource.Stop(); }

}

[Serializable]
public struct UIMusicList
{
    public AudioClip Music { get => _music; }
    public string name;
    [SerializeField] private AudioClip _music;
}