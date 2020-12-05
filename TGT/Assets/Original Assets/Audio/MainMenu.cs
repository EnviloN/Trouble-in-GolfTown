using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;
    public GameObject MusicPlayer;


    public IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        //mute music (just for now)
        MusicPlayer.GetComponent<AudioSource>().mute = true;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        //transition.SetTrigger("End");
    }

    public void PlayGame() {
        Debug.Log("Load Main Scene.");
        StartCoroutine(LoadScene());
        //LoadScene();
    }

    public void QuitGame() {

        Debug.Log("Game presumably ended.");

        Application.Quit();
    }

}
