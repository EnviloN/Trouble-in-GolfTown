using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{

    public  GameObject canvas;
    public GameObject pause_menu;
    public GameObject main_menu;
    public GameObject settings_menu;

    public GameObject resumeBtn, volumeBtn, quitBtnPause;
    public GameObject playBtn, settingsBtn, quitBtn;
    public GameObject volumeBtnSett, resumeBtnSett;

    GameObject player;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            //Debug.Log("Picked:   " + EventSystem.current.currentSelectedGameObject);
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
        canvas.transform.position = player.transform.position + player.transform.forward * 30.0f + Vector3.up * 1.5f;
        canvas.transform.rotation = player.transform.rotation;
    }

    public void DisplayMainMenu() {
        this.canvas.SetActive(true);
        this.main_menu.SetActive(true);
        //clear
        EventSystem.current.SetSelectedGameObject(null);
        //set on play
        EventSystem.current.SetSelectedGameObject(settingsBtn);
        PositionCanvas();
        //Debug.Log("Main menu displayed:"+ EventSystem.current.currentSelectedGameObject);
    }

    public void DisplayPauseMenu() {
        this.canvas.SetActive(true);
        this.pause_menu.SetActive(true);
        this.main_menu.SetActive(false);
        //clear
        EventSystem.current.SetSelectedGameObject(null);
        //set on resume
        EventSystem.current.SetSelectedGameObject(quitBtnPause);
        this.PositionCanvas();
        //Debug.Log("Pause menu displayed." + EventSystem.current.currentSelectedGameObject);
    }

    public void HideMenu() {
        this.canvas.SetActive(false);
        this.pause_menu.SetActive(false);
        this.main_menu.SetActive(false);
        this.settings_menu.SetActive(false);

        //clear
        EventSystem.current.SetSelectedGameObject(null);
    }


    public void Pause() {
        isPaused = true;

        //freeze player
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FreezeMovement>().Freeze();
        Time.timeScale = 0;
    }


    public void Resume() {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").layer = 0;
        isPaused = false;
        //unfreeze player
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FreezeMovement>().UnFreeze();
    }




}
