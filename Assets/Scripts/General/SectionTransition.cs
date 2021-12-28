using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTransition : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Fader fader;
    [SerializeField] private SectionManager currentSection;

    void Start()
    {
        currentSection.OnSectionExit += TransitionToNextSection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TransitionToNextSection()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return fader.FadeOut(2f);
        Debug.Log("Waited");
        FindObjectOfType<PlayerController>().transform.position = currentSection.GetNextSection().GetSectionSpawnLocation();
        currentSection = currentSection.GetNextSection();
        yield return fader.FadeIn(1f);

    }
}
