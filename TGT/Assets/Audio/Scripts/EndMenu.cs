using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeepPlaying()
    {
        GameObject scene_loader = GameObject.Find("SceneLoader");
        scene_loader.GetComponent<PauseGame>().Resume();
        scene_loader.GetComponent<PauseGame>().HideMenu();
    }

    public void QuitGame() {
        Debug.Log("Game presumably ended.");
        Application.Quit();
    }
    
}
