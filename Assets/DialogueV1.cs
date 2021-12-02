using TMPro;
using UnityEngine;

public class DialogueV1 : MonoBehaviour
{
    // TODO - Provide toggle to turn bounce animation on or off. 
    // Provide way to read text in and stop at certain characters until user gives input. 
    // Provide way to animate text in or just show all
    // Give option to have dynamic field size or static. 
    // Give option to show a speaker name or title 
    TextMeshPro textbox;
    SpriteRenderer sprite;

    public string text;
    string _text;

    private void Awake()
    {
        _text = text;
        textbox = GetComponentInChildren<TextMeshPro>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        InitializeText();
        InitializeSprite();
    }

    private void Update()
    {
        if (!text.Equals(_text))
        {
            _text = text;
            InitializeText();
            InitializeSprite();
        }

    }

    void InitializeSprite()
    {
        if (sprite == null) return;

        float spriteWidth = (_text.Length * 0.38f) + 2.0f;
        Vector2 newSize = new Vector2(spriteWidth, sprite.size.y);
        sprite.size = newSize;
    }
    void InitializeText()
    {
        if (textbox == null) return;
        textbox.text = _text;
    }
}
