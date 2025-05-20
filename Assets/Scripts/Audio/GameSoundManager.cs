using System;
using UnityEngine;

public enum GameSoundType
{
    STEPS,
    RUN,
    PUSH
}
public class GameSoundManager : MonoBehaviour
{
    [SerializeField] private GameSoundList[] _soundList;
    private static GameSoundManager instance;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayRandomGameSound(GameSoundType soundType, float volume = 1)
    {
        AudioClip[] clips = instance._soundList[(int)soundType].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance._audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(GameSoundType));
        Array.Resize(ref _soundList, names.Length);
        for (int i = 0; i < _soundList.Length; i++)
        {
            _soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct GameSoundList
{
    public AudioClip[] Sounds { get => sounds; }
    public string name;
    [SerializeReference] private AudioClip[] sounds;
}