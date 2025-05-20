using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _UIContainerMenu;
    [SerializeField] private GameObject _UIContainerJuego;

    private UISoundManager _UISoundManager;
    private MusicSoundManager _musicSoundManager;
    private GameSoundManager _gameSoundManager;

    private static UIManager instance;

    private bool _isRuntimeOnMenu { get; set; }
    public bool isRuntimeOnMenu
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
        // 🚫 Si ya existe una instancia, destruir esta nueva
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ✅ Si no existe, esta será la única
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _UISoundManager = gameObject.GetComponent<UISoundManager>();
        _musicSoundManager = gameObject.GetComponent<MusicSoundManager>();
        _gameSoundManager = gameObject.GetComponent<GameSoundManager>();

        isRuntimeOnMenu = true;
        _musicSoundManager.SelectMusicToPlay(1);
        _musicSoundManager.SetVolumeMusicPlayer(.2f);
        _musicSoundManager.PlayMusicPlayer();
    }

    public void UpdateLevelName(string newValue)
    {
        _UIContainerJuego.GetComponent<UIContainerJuego>().LevelTextChange(newValue);
    }

    public void UpdateLevelCountdown(int newValue)
    {
        _UIContainerJuego.GetComponent<UIContainerJuego>().LevelCountdownTextChange(newValue);
    }

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

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void SiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NuevoJuego()
    {
        _musicSoundManager.SelectMusicToPlay(0);
        SceneManager.LoadScene(1);
    }

    public void MenuPrincipal()
    {
        _musicSoundManager.SelectMusicToPlay(1);
        _UIContainerJuego.SetActive(false);
        _UIContainerMenu.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlaySoundOnButtonClick()
    {
        _UISoundManager.PlayUISound(0, 0.2f);
    }

    public void SetMovementValueWalk(bool isWalking)
    {
        _gameSoundManager.isMoving = isWalking;
    }

    public void SetMovementValuePush(bool isPushing)
    {
        _gameSoundManager.isPushingObject = isPushing;
    }
}
