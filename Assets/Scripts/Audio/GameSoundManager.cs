using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [SerializeField] private GameSoundList[] _soundList;
    private IEnumerator _PlayDelayedOnMove;
    private AudioSource _audioSource;
    public float delaySpeed = 0.3f, volume = 0.3f;
    private bool _isMoving, _isPushingObject;
    public bool isMoving // el setter activa o desactiva la co-rutina según su valor (se llama desde un método en UIManager)
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
            if (_isMoving) { StartCoroutine(nameof(_PlayDelayedOnMove)); }
            else { StopCoroutine(nameof(_PlayDelayedOnMove)); }
        }
    }
    public bool isPushingObject
    {
        get { return _isPushingObject; }
        set => _isPushingObject = value;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _PlayDelayedOnMove = PlayDelayed(delaySpeed);
    }

    IEnumerator PlayDelayed(float delay) {
        AudioClip clip;
        if (_isPushingObject) { clip = _soundList[1].Sound; }
        else { clip = _soundList[0].Sound; }

        _audioSource.pitch = UnityEngine.Random.Range(.95f, 1.05f);
        _audioSource.PlayOneShot(clip, volume);
        _audioSource.pitch = 1;
        yield return new WaitForSeconds(delay); 
    }
}

[Serializable]
public struct GameSoundList
{
    public AudioClip Sound { get => sound; }
    public string name;
    [SerializeReference] private AudioClip sound;
}