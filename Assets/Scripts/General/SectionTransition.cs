using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTransition : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Fader fader;
    [SerializeField] private SectionManager currentSection;
    [SerializeField] private Transform sceneStart;


    void Start()
    {
        Debug.Log("!!!!!!!!!Starting Section Transition");
        currentSection.OnSectionTeleport += TransitionToNextSection;
    }

    public void RecievePlayer()
    {
        FindObjectOfType<PlayerInput>().enabled = false;
        fader.FadeOutImmediate();
        Debug.Log("Starting scene");
        if(sceneStart == null)
        {
            Debug.LogError("sceneStart location not set, no place to put player");
        }
        FindObjectOfType<PlayerController>().transform.position = sceneStart.position;
        GetComponent<DeathManager>().SetSpawnLocation(sceneStart);
        StartCoroutine(StartScene());
  
    }

    public IEnumerator StartScene()
    {
        //Put the player at the starting point
        //Probably do any saving stuff that needs to be done
        yield return fader.FadeIn(1f);
        FindObjectOfType<PlayerInput>().enabled = true;
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
