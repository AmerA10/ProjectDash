using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] Image fadePanel;
    [SerializeField] Coroutine currentActiveFade = null;

    Color black, transparent;
    void Awake()
    {
        black = new Color(0, 0, 0, 255);
        transparent = new Color(0, 0, 0, 0);
    }
    void Start()
    {
        Debug.Log("started");
    }

    public void FadeOutImmediate()
    {
        fadePanel.color = black;
    }

    public IEnumerator FadeOutIn()
    {
        yield return FadeOut(3f);

        yield return FadeIn(1f);
    }

    public Coroutine FadeOut(float time)
    {
        return Fade(1, time);
    }

    public Coroutine FadeIn(float time)
    {
        return Fade(0, time);
    }

    //checks for active fades and cancels them if they exists
    //then calls the FadeRoutine to do the fade
    public Coroutine Fade(float target, float time)
    {
        //Cancel running coroutines
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }
        currentActiveFade = StartCoroutine(FadeRoutine(target, time));
        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(float target, float time)
    {
        Debug.Log("Staring fade");
        float alpha = fadePanel.color.a;
        while (!Mathf.Approximately(alpha, target)) //alpha is not 1, update it until it is 
        {
            alpha = Mathf.MoveTowards(alpha, target, Time.deltaTime / time);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null; //null = 1 frame;
                               //moveAlpha until it is 1 by the frame and time
        }

    }

    // Start is called before the first frame update
}


