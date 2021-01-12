using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PauseMenu : MonoBehaviour
{

    public void ResumeGame()
    {
        Debug.Log("Game Resumed.");
        GameObject loader = GameObject.FindGameObjectWithTag("SceneLoader");
        loader.GetComponent<PauseGame>().Resume();
        loader.GetComponent<PauseGame>().HideMenu();
    }

    public void QuitGame()
    {
        Debug.Log("Game presumably ended.");
        Application.Quit();
    }

}
