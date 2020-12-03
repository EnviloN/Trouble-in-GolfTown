using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    private void LoadMainScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
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
