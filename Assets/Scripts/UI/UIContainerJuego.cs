using System;
using TMPro;
using UnityEngine;

public class UIContainerJuego : MonoBehaviour
{
    [Header("Contenedores")]
    [SerializeField] private GameObject _ContainerAyuda;
    [SerializeField] private GameObject _ContainerJuegoActivo;
    [SerializeField] private GameObject _ContainerSalirMenu;

    [Header("Texto de nivel")]
    [SerializeField] private TextMeshProUGUI _levelTitleText, _levelCountdownText;

    public enum GameMenuSwitch
    {
        Game,
        GameHelp,
        Pause
    }

    void Start()
    {
        EnableGameUIDefaultSetup();
    }

    void OnDisable()
    {
        EnableGameUIDefaultSetup();
    }
    private void EnableGameUIDefaultSetup()
    {
        _ContainerJuegoActivo.SetActive(true);
        _ContainerAyuda.SetActive(false);
        _ContainerSalirMenu.SetActive(false);
        LevelTextChange("Nivel 0 - Test");
        LevelCountdownTextChange(60);
    }

    // Permite que los botones puedan cambiar el menú al accionarse un listener
    public void GameUISwitchMenuCall(int valueInt)
    {
        switch (valueInt)
        {
            case 1:
                GameUISwitcher(GameMenuSwitch.Game);
                break;
            case 2:
                GameUISwitcher(GameMenuSwitch.GameHelp);
                break;
            case 3:
                GameUISwitcher(GameMenuSwitch.Pause);
                break;
        }
    }
    private void GameUISwitcher(GameMenuSwitch valueToSwitch)
    {
        bool localTrueBool = true;
        switch (valueToSwitch)
        {
            case GameMenuSwitch.Game:
                _ContainerJuegoActivo.SetActive(localTrueBool);
                _ContainerSalirMenu.SetActive(!localTrueBool);
                break;
            case GameMenuSwitch.GameHelp:
                if (_ContainerAyuda.activeSelf)
                {
                    _ContainerAyuda.SetActive(localTrueBool);
                }
                else
                {
                    _ContainerAyuda.SetActive(!localTrueBool);
                }
                break;
            case GameMenuSwitch.Pause:
                _ContainerJuegoActivo.SetActive(!localTrueBool);
                if (_ContainerAyuda.activeSelf)
                {
                    _ContainerAyuda.SetActive(!localTrueBool);
                }
                _ContainerSalirMenu.SetActive(localTrueBool);
                break;
        }
    }

    public void LevelTextChange(string stringValue)
    {
        _levelTitleText.text = stringValue;
    }

    public void LevelCountdownTextChange(int newCountValue)
    {
        _levelCountdownText.text = newCountValue.ToString();
    }
}
