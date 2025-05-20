using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _UIContainerMenu;
    [SerializeField] private GameObject _UIContainerJuego;
    private UISoundManager _UISoundManager;
    private MusicSoundManager _musicSoundManager;
    private GameSoundManager _gameSoundManager;

    private bool _isRuntimeOnMenu { get; set; }
    public bool isRuntimeOnMenu // Cambiar al booleano activa o desactiva los menús
    {
        get { return _isRuntimeOnMenu; }
        set
        {
            UISelectorSwitch(value);
            _isRuntimeOnMenu = value;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        _UISoundManager = gameObject.GetComponent<UISoundManager>();
        _musicSoundManager = gameObject.GetComponent<MusicSoundManager>();
        _gameSoundManager = gameObject.GetComponent<GameSoundManager>();

        isRuntimeOnMenu = true;
        _musicSoundManager.SelectMusicToPlay(0);
        _musicSoundManager.SetVolumeMusicPlayer(.2f);
        _musicSoundManager.PlayMusicPlayer();
    }

    // Funciones cambian el nombre del texto del título y tiempo limite respectivo
    public void UpdateLevelName(string newValue)
    {
        _UIContainerJuego.GetComponent<UIContainerJuego>().LevelTextChange(newValue);
    }

    public void UpdateLevelCountdown(int newValue)
    {
        _UIContainerJuego.GetComponent<UIContainerJuego>().LevelCountdownTextChange(newValue);
    }

    // Función para que el booleano desactive o active ciertos contenedores del UI cuando cambie.
    private void UISelectorSwitch(bool localIsRuntimeOnMenu)
    {
        if (!localIsRuntimeOnMenu)
        {
            _UIContainerMenu.SetActive(false);
            _UIContainerJuego.SetActive(true);
        }
        else
        {
            _UIContainerMenu.SetActive(true);
            _UIContainerJuego.SetActive(false);
        }
    }

    // Reproduce sonido sfx

    public void PlaySoundOnButtonClick()
    {
        _UISoundManager.PlayUISound(0, 0.2f);
    }
    
}
