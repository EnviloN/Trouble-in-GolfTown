using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MusicPlayer;
    private GameObject loader;

    public void PlayGame() {
        Debug.Log("Hit Play.");
        //LoadScene();
        //this.enabled = false;
        MusicPlayer.GetComponent<AudioSource>().mute = true;

        //SceneManager.LoadScene("Main", LoadSceneMode.Single);
        //transition.SetTrigger("End");
        Debug.Log("Load Main Scene via loader.");
        
        loader = GameObject.FindGameObjectWithTag("SceneLoader");
        loader.GetComponent<SceneLoader>().LoadMainScene();

        gameObject.SetActive(false);
    }

    public void QuitGame() {

        Debug.Log("Game presumably ended.");
        Application.Quit();
    }

}
