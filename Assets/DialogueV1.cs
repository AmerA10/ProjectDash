using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DialogueV1 : MonoBehaviour
{
    // TODO 
    // Add indicator to show when there's more text to read.
    // Add way to show and hide the dialogue box
    // Give option to show a speaker name or title 
    TextMeshPro textbox;
    SpriteRenderer sprite;

    public bool animateText;
    public bool bounce;
    public bool fixedSize;
    public string dialogue;
    public float delay = 0.01f;
    public int maxCharsWide = 10; // If fixedSize is false, we can set the maximum width using maxCharsWide. If the text has more characters than this number, the box will increase in height.
    public float textPaddingTop = 0f;
    public float textPaddingSide = 0f;
    public float textCharacterWidth = 0.25f;
    public float textRowHeight = 0.3f;
    float timer = 0f;
    List<string> _textList;
    string _text;
    bool waitForInput;


    private void Awake()
    {
        if (!bounce) GetComponentInChildren<Animator>().enabled = false;

        ParseDialogue();
        textbox = GetComponentInChildren<TextMeshPro>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        InitializeText();
        UpdateSprite();
    }

    private void Update()
    {
        if (waitForInput && Input.GetKeyDown(KeyCode.Space))
        {
            waitForInput = false;
            timer = 0f;
        }
        else if (!waitForInput)
        {
            if (animateText)
            {
                timer += Time.deltaTime;
                if (timer < delay) return;
            }
            UpdateText();
            
        }
        UpdateSprite();
    }

    void ParseDialogue()
    {
        if (dialogue.TrimEnd().Length == 0) return;
        _textList = dialogue.Split('|').ToList();
    }

    void InitializeText()
    {
        if (textbox == null) return;
        if (_textList.Count == 0) return;
        if (_textList.Count > 1 && !animateText) waitForInput = true;
        string nextText = _textList[0];
        _textList.RemoveAt(0);
        _text = nextText;
        if (!animateText) textbox.text = _text;
        else textbox.text = _text.Substring(0, 1);
    }
    void UpdateText()
    {
        // When user presses space
        // Get the next text and set _text to it
        // Then update the display. 
        // If animating, animate it.

        if (_textList.Count > 0)
        {
            if (!animateText || (animateText && textbox.text.Equals(_text)))
            {
                string nextText = _textList[0];
                _textList.RemoveAt(0);
                _text = nextText;
                textbox.text = "";
            }
        }

        if (animateText)
        {
            int nextIndex = textbox.text.Length + 1;
            if (nextIndex > _text.Length) return;
            string nextText = _text.Substring(0, nextIndex);
            textbox.text = nextText;
            if (nextIndex == _text.Length && _textList.Count > 0) waitForInput = true;
            timer = 0f;
            return;
        }

        textbox.text = _text;
        waitForInput = true;

    }

    void UpdateSprite()
    {
        if (sprite == null || fixedSize) return;
        int textLength = _text.Length;
        float spriteWidth = sprite.size.x;
        float spriteHeight = sprite.size.y;
        float textWidth = textbox.rectTransform.sizeDelta.x;
        float textHeight = textbox.rectTransform.sizeDelta.y;

        if (!fixedSize && textLength > maxCharsWide)
        {
            textWidth = 2.0f + (maxCharsWide * textCharacterWidth);
            textHeight = 0.5f + (textLength / maxCharsWide) * textRowHeight;
            Debug.Log($"Text height: {textHeight}");
            spriteWidth = textWidth + textPaddingSide;
            spriteHeight = textHeight + textPaddingTop;
        }

        Vector2 newSpriteSize = new Vector2(spriteWidth, spriteHeight);
        Vector2 newTextSize = new Vector2(textWidth, textHeight);
        textbox.rectTransform.sizeDelta = newTextSize;
        sprite.size = newSpriteSize;

    }
}
