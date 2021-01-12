using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public static class MouseOverUILayerObject
{

}

public class PauseGame : MonoBehaviour
{

    public  GameObject canvas;
    public GameObject main_menu;
    public GameObject settings_menu;

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
        isPaused = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
            Pause();
            DisplayMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused == true) {  
            Resume();
            HideMenu();
        }

        if (isPaused) {
            
            GameObject hover_over = IsPointerOverUIObject();
            if (hover_over !=  null) {
                //Debug.Log("Pointer over UI. "+ hover_over.name);
                EventSystem.current.SetSelectedGameObject(hover_over);
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.H))
                {
                    //Debug.Log("Mouse position:" + Input.mousePosition);
                    Button btn = hover_over.GetComponent<Button>();
                    if (btn != null) {
                        btn.onClick.Invoke();
                    }
                    //Debug.Log("Clicked " + hover_over.name);
                    //Debug.Log("Picked:   " + EventSystem.current.currentSelectedGameObject);
                }
            }

                       

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
        canvas.transform.position = player.transform.position + player.transform.forward * 10.0f + Vector3.up * 1.5f;
        canvas.transform.rotation = player.transform.rotation;
    }

    public void DisplayMainMenu() {
        canvas.SetActive(true);
        main_menu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        PositionCanvas();
    }


    public void HideMenu() {
        canvas.SetActive(false);
        main_menu.SetActive(false);
        settings_menu.SetActive(false);
    }


    public void Pause() {
        isPaused = true;
        Time.timeScale = 0;
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
