using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIContainerMenu : MonoBehaviour
{
    [Header("Contenedores")]
    [SerializeField] private GameObject _ContainerPrincipal;
    [SerializeField] private GameObject _ContainerCreditos;
    [SerializeField] private GameObject _ContainerSalidaSelector;
    public enum MainMenuSwitch
    {
        Main,
        Credits,
        ToExit
    }
    void Start()
    {
        EnableMenuDefaultSetup();
    }

    void OnDisable()
    {
        EnableMenuDefaultSetup();
    }
    private void EnableMenuDefaultSetup()
    {
        _ContainerPrincipal.SetActive(true);
        _ContainerCreditos.SetActive(false);
        _ContainerSalidaSelector.SetActive(false);
    }

     public void MenuUISwitchMenuCall(int valueInt)
    {
        switch (valueInt)
        {
            case 1:
                MainMenuSwitcher(MainMenuSwitch.Main);
                break;
            case 2:
                MainMenuSwitcher(MainMenuSwitch.Credits);
                break;
            case 3:
                MainMenuSwitcher(MainMenuSwitch.ToExit);
                break;
        }
    }
    public void MainMenuSwitcher(MainMenuSwitch valueToSwitch)
    {
        bool localTrueBool = true;
        switch (valueToSwitch)
        {
            case MainMenuSwitch.Main:
                _ContainerPrincipal.SetActive(localTrueBool);
                _ContainerCreditos.SetActive(!localTrueBool);
                _ContainerSalidaSelector.SetActive(!localTrueBool);
                break;
            case MainMenuSwitch.Credits:
                _ContainerPrincipal.SetActive(!localTrueBool);
                _ContainerCreditos.SetActive(localTrueBool);
                _ContainerSalidaSelector.SetActive(!localTrueBool);
                break;
            case MainMenuSwitch.ToExit:
                _ContainerPrincipal.SetActive(!localTrueBool);
                _ContainerCreditos.SetActive(!localTrueBool);
                _ContainerSalidaSelector.SetActive(localTrueBool);
                break;
        }

    }
}
