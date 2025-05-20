using System;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private UISoundList[] _soundList;
    private static UISoundManager instance;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayUISound(int index, float volume = 1)
    {
        AudioClip clip = instance._soundList[index].Sound;
        instance._audioSource.PlayOneShot(clip, volume);
    }
}

[Serializable]
public struct UISoundList
{
    public AudioClip Sound { get => _sound; }
    public string name;
    [SerializeReference] private AudioClip _sound;
}