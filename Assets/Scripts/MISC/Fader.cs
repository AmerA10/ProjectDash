using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Coroutine currentActiveFade = null;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start()
    {
        Debug.Log("started");
    }

    public void FadeOutImmediate()
    {
        canvasGroup.alpha = 1;
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
        while (!Mathf.Approximately(canvasGroup.alpha, target)) //alpha is not 1, update it until it is 
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
            yield return null; //null = 1 frame;
                               //moveAlpha until it is 1 by the frame and time
        }
        
    }

    // Start is called before the first frame update
}


