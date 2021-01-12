using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;



public static class MouseOverUILayerObject
{

}

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
    public GameObject[] hands;

    public static GameObject IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return results[i].gameObject;
            }
        }

        return null;
    }

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

            GameObject hover_over = IsPointerOverUIObject();
            if (hover_over !=  null) {
                Debug.Log("Pointer over UI. "+ hover_over.name);
                EventSystem.current.SetSelectedGameObject(hover_over);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Game presumably ended.");
                Application.Quit();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Picked:   " + EventSystem.current.currentSelectedGameObject);
                Debug.Log("system:   " + EventSystem.current.name);
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
        canvas.SetActive(true);
        main_menu.SetActive(true);
        //clear
        EventSystem.current.SetSelectedGameObject(null);
        //set on play
        //EventSystem.current.SetSelectedGameObject(settingsBtn);
        PositionCanvas();
        //Debug.Log("Main menu displayed:"+ EventSystem.current.currentSelectedGameObject);
    }

    public void DisplayPauseMenu() {
        canvas.SetActive(true);
        pause_menu.SetActive(true);
        main_menu.SetActive(false);
        //clear
        EventSystem.current.SetSelectedGameObject(null);
        //set on resume
        //EventSystem.current.SetSelectedGameObject(resumeBtn);
        PositionCanvas();
        //Debug.Log("Pause menu displayed." + EventSystem.current.currentSelectedGameObject);
    }

    public void HideMenu() {
        canvas.SetActive(false);
        pause_menu.SetActive(false);
        main_menu.SetActive(false);
        settings_menu.SetActive(false);

        //clear
        EventSystem.current.SetSelectedGameObject(null);
    }


    public void Pause() {
        isPaused = true;
        Time.timeScale = 0;

        //freeze player
        player = GameObject.FindGameObjectWithTag("Player");
        SetLayerRecursively(player, 5);
        hands = GameObject.FindGameObjectsWithTag("Hands");

        foreach (GameObject hand in hands)
        {
            SetLayerRecursively(hand, 5);
        }

        player.GetComponent<FreezeMovement>().Freeze();
        
    }


    public void Resume() {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        SetLayerRecursively(player, 0);

        hands = GameObject.FindGameObjectsWithTag("Hands");

        foreach (GameObject hand in hands) {
            SetLayerRecursively(hand, 0);
        }

        isPaused = false;
        //unfreeze player
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FreezeMovement>().UnFreeze();
    }


    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }




}
