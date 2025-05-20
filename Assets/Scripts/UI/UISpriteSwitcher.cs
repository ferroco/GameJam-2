using Microsoft.Unity.VisualStudio.Editor;
using Image = UnityEngine.UI.Image;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Image _gameObjectImage;
    public Sprite spriteByDefault;
    public Sprite spriteOnMouseOver;
    public Sprite spriteOnMouseDown;
    void Start()
    {
        _gameObjectImage = gameObject.GetComponent<Image>();
    }

    void OnMouseDown()
    {
        _gameObjectImage.sprite = spriteOnMouseDown;
    }
    void OnMouseUpAsButton()
    {
        _gameObjectImage.sprite = spriteOnMouseOver;
    }
    void OnMouseEnter()
    {
        _gameObjectImage.sprite = spriteOnMouseOver;
    }
    void OnMouseExit()
    {
        _gameObjectImage.sprite = spriteByDefault;
    }

}
