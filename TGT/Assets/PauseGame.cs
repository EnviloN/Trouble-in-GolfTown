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
            DisplayPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused == true) {  
            Resume();
            HideMenu();
        }

        if (isPaused) {
            PositionCanvas();
            Debug.Log("Positionning canvas");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Game presumably ended.");
                Application.Quit();
            }
        }
    }


    void PositionCanvas()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Menu");
        canvas.transform.position = player.transform.position + player.transform.forward * 10.0f + Vector3.up * 1.5f;
        canvas.transform.rotation = player.transform.rotation;
    }

    public void DisplayMainMenu() {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Menu");
        main_menu = GameObject.Find("MainMenuXR");
        canvas.SetActive(true);
        main_menu.SetActive(true);
        UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
        es.firstSelectedGameObject = GameObject.Find("SettingsButton");
        PositionCanvas();
        Debug.Log("Main menu displayed???");
    }

    public void DisplayPauseMenu() {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Menu");
        main_menu = GameObject.Find("MainMenuXR");
        canvas.SetActive(true);
        pause_menu.SetActive(true);
        UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
        es.firstSelectedGameObject = GameObject.Find("ResumeButton");
        PositionCanvas();
    }

    public void HideMenu() {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Menu");
        main_menu = GameObject.Find("MainMenuXR");
        canvas.SetActive(false);
        pause_menu.SetActive(false);
        main_menu.SetActive(false);
    }


    public void Pause() {
        isPaused = true;
        player = GameObject.FindGameObjectWithTag("Player");
        player.layer = 5;
        Debug.Log("Game Paused");
        //freeze player
        player.GetComponent<FreezeMovement>().Freeze();

        //change hands layer 
        //player.GetComponentInChildren< "XR Controller (Device-based)" > ();

        //freeze time
        Time.timeScale = 0;
    }


    public void Resume() {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        player.layer = 0;
        //InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Debug.Log("Resuming Game");
        isPaused = false;
        //unfreeze player
        player.GetComponent<FreezeMovement>().UnFreeze();
    }




}
