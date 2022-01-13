using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneExit : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    //ok so for a scene exit I need a few things
    //first thing is the next scene That i need to use
    //The second is to figure out how to load the scene properly and how to place the player inside of the proper section and such

    [SerializeField] int sceneID;
    bool isInteractbale = true;
    
    void Start()
    {
        
        
    }


    // Update is called once per frame
    void Update()
    {

    }


    public bool IsInteractable()
    {
        return isInteractbale;
    }

    public void HandleInteraction()
    {
       StartCoroutine(SwitchScene());
    }
    private IEnumerator SwitchScene()
    {
        FindObjectOfType<PlayerInput>().enabled = false;
        isInteractbale = false;
        Fader fader = FindObjectOfType<Fader>();

        this.transform.parent = null;
        DontDestroyOnLoad(gameObject);
        yield return fader.FadeOut(1f);
        yield return SceneManager.LoadSceneAsync(sceneID);
        Debug.Log("recieving player");
        FindObjectOfType<LevelManager>().RecievePlayer();
        Destroy(fader);
        Destroy(gameObject);

    }
    public void HandleReset()
    {
        throw new System.NotImplementedException();
    }

    public bool CanReset()
    {
        throw new System.NotImplementedException();
    }
}
