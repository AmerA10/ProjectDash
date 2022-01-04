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
        currentSection.OnSectionTeleport += TransitionToNextSection;
    }


    private void TransitionToNextSection(Exit destination, SectionManager section)
    {
        StartCoroutine(Transition(destination, section));
    }

    private IEnumerator Transition(Exit destination, SectionManager section)
    {
        yield return fader.FadeOut(2f);
        Debug.Log("Waited");
        FindObjectOfType<PlayerController>().transform.position = destination.transform.position;
        GetComponent<DeathManager>().SetSpawnLocation(destination.transform);
        currentSection = section;
        yield return fader.FadeIn(1f);

    }
}
