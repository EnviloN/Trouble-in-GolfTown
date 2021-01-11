using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MainMenu : MonoBehaviour
{
    private GameObject loader;

    public void PlayGame() {
        Debug.Log("Play.");
        
        loader = GameObject.FindGameObjectWithTag("SceneLoader");
        loader.GetComponent<SceneLoader>().StartGame();

        gameObject.SetActive(false);
    }

    public void QuitGame() {

        Debug.Log("Game presumably ended.");
        Application.Quit();
    }

}
