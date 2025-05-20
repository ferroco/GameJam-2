using System;
using Unity.VisualScripting;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private UISoundList[] _soundList;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayUISound(int index = 0, float volume = .2f)
    {
        AudioClip clip = _soundList[index].Sound;
        _audioSource.PlayOneShot(clip, volume);
    }
}

[Serializable]
public struct UISoundList
{
    public AudioClip Sound { get => _sound; }
    public string name;
    [SerializeReference] private AudioClip _sound;
}