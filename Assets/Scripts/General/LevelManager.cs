using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathManager))]
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Fader fader;
    [SerializeField] private SectionManager currentSection;
    [SerializeField] private Transform sceneStart;
    private PlayerController player;
    private DeathManager dm;
    [SerializeField] private CameraController mainCamera;

    [SerializeField] float fadeOutTime = 2f;
    [SerializeField] float fadeInTime = 1f;

    void Start()
    {
        dm = GetComponent<DeathManager>();
        dm.SetSpawnLocation(sceneStart);
        player = FindObjectOfType<PlayerController>();
        currentSection.OnSectionTeleport += TransitionToNextSection; 
        SetUpPlayer();
        SetCameraClamps();
    }

    private void SetUpPlayer()
    {
/*        player.OnDeath += currentSection.ResetIInteractables;*/
        player.transform.position = currentSection.GetSpawnLocation().position;
    }

    public void RecievePlayer()
    {
        player.GetComponent<PlayerInput>().enabled = false;
        fader.FadeOutImmediate();
        Debug.Log("Starting scene");
        if(sceneStart == null)
        {
            Debug.LogError("sceneStart location not set, no place to put player");
        }
        player.transform.position = sceneStart.position;
        GetComponent<DeathManager>().SetSpawnLocation(sceneStart);
        StartCoroutine(StartScene());
  
    }

    private IEnumerator StartScene()
    {
        //Put the player at the starting point
        //Probably do any saving stuff that needs to be done
        yield return fader.FadeIn(fadeInTime);
        player.GetComponent<PlayerInput>().enabled = true;
    }


    private void TransitionToNextSection(Exit destination, SectionManager section)
    {
        StartCoroutine(Transition(destination, section));
    }

    private IEnumerator Transition(Exit destination, SectionManager section)
    {
        player.GetComponent<PlayerInput>().enabled = false;
        yield return fader.FadeOut(fadeOutTime);
        Debug.Log("Waited");
        player.transform.position = destination.transform.position;
        GetComponent<DeathManager>().SetSpawnLocation(destination.transform);
/*        player.OnDeath -= currentSection.ResetIInteractables;*/
        currentSection.OnSectionTeleport -= TransitionToNextSection;
        currentSection = section;
/*        player.OnDeath += currentSection.ResetIInteractables;*/
        currentSection.OnSectionTeleport += TransitionToNextSection;
        SetCameraClamps();
        yield return fader.FadeIn(fadeInTime);
        player.GetComponent<PlayerInput>().enabled = true;
    }

    private void SetCameraClamps()
    {
        Debug.Log("Setting up clamps");
        mainCamera.SetClamp(currentSection.GetXClamp(), currentSection.GetYClamp());
    }
}
