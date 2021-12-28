using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    // Start is called before the first frame update
    public void LoadNextLevel()
    {
        SceneChanger.LoadNextLevel();
    }
    public void Exit()
    {
        SceneChanger.Exit();
    }

}
public static class SceneChanger {
    public static void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings <= (SceneManager.GetActiveScene().buildIndex + 1))
        {
            Debug.Log("Scene does not exist");
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void Exit()
    {
        Application.Quit();
    }
}
    


