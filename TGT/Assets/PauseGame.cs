using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{

    GameObject canvas;
    public GameObject pause_menu;
    GameObject main_menu;
    GameObject player;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Menu");
        main_menu = GameObject.Find("MainMenuXR");
        
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused == true) {  
            Resume();
        }

        if (isPaused) {
            PositionCanvas();
        }
    }


    void PositionCanvas()
    {
        canvas.transform.position = player.transform.position + player.transform.forward * 10.0f + Vector3.up * 0.0f;
        canvas.transform.rotation = player.transform.rotation;
    }


    public void Pause() {
        isPaused = true;
        Debug.Log("Game Paused");
        //freeze player
        player.GetComponent<FreezeMovement>().Freeze();

        canvas.SetActive(true);
        pause_menu.SetActive(true);

        //freeze time
        //InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        Time.timeScale = 0;

    }


    public void Resume() {
        Time.timeScale = 1;
        //InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Debug.Log("Resuming Game");
        isPaused = false;
        //unfreeze player
        player.GetComponent<FreezeMovement>().UnFreeze();

        //enable pause menu
        //main_menu.SetActive(false);
        pause_menu.SetActive(false);

        //display canvas
        canvas.SetActive(false);
    }




}
