using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public static class MouseOverUILayerObject
{

}

public class PauseGame : MonoBehaviour
{

    public GameObject canvas;
    public GameObject main_menu;
    public GameObject settings_menu;
    public GameObject help_menu;

    public GameObject MusicPlayer;
    public float audioFadeOut = 1.0f;

    GameObject player;
    public static bool isPaused;
    private bool gameStarted = false;
    private bool isXR;
    private GameObject[] hands;
    private XRRayInteractor rayInteractor;

    private static GameObject IsPointerOverUIObject()
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

    private bool PointerPressedDown()
    {
        if (Input.GetMouseButtonDown(0))// || Input.GetKeyDown(KeyCode.H) || Input.GetMouseButtonDown(1))
        {
            return true;
        }
        else return false;
    }

    void Start()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isXR = false;
        XRDetection detection = GameObject.Find("GameManager").GetComponent<XRDetection>();
        if (detection.isXR)
        {
            XRSetup();
        }
        detection.XRReady.AddListener(ready => XRSetup());
    }

    void XRSetup() {
        isXR = true;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<XRInteractions>().menuButtonPress.AddListener(pressed =>
        {
            if (pressed && !isPaused)
            {
                Pause();
                DisplayMainMenu();
            }
            else if (pressed && isPaused)
            {
                Resume();
                HideMenu();
            }
        });
        if (isPaused)
        {
            SetLongRaycast(true);
            SetVisibleHands(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
            Pause();
            DisplayMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused == true && gameStarted) {  
            Resume();
            Cursor.visible = false;
            HideMenu();
        }

        if (isPaused)
        {
            if (!Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Game presumably ended.");
                Application.Quit();
            }
        }
    }

    void PositionCanvas()
    {
        if (!isXR)
        {
            canvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 10.0f + Vector3.up * 1.2f;
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            canvas.transform.position = player.transform.position + player.transform.forward * 10.0f + Vector3.up * 1.2f;
            canvas.transform.rotation = player.transform.rotation;
        }
    }

    public void DisplayMainMenu()
    {
        canvas.SetActive(true);
        main_menu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        PositionCanvas();
    }

    public void HideMenu()
    {
        canvas.SetActive(false);
        main_menu.SetActive(false);
        settings_menu.SetActive(false);
        help_menu.SetActive(false);
    }

    public void Pause()
    {
        //fadein music
        StartCoroutine(AudioFadeIn(MusicPlayer.GetComponent<AudioSource>(), audioFadeOut));
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FreezeMovement>().Freeze();
        if (isXR) {
            SetLongRaycast(true);
            SetVisibleHands(true);
        }
    }

    public void Resume() {
        gameStarted = true;
        Cursor.visible = false;
        Time.timeScale = 1;
        //fadeout audio
        StartCoroutine(AudioFadeOut(MusicPlayer.GetComponent<AudioSource>(), audioFadeOut));
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FreezeMovement>().UnFreeze();
        if (isXR) {
            SetLongRaycast(false);
            SetVisibleHands(false);
        }
    }

    void SetLongRaycast(bool value) {
        rayInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rayInteractor.velocity = value ? 50 : 5;
    }

    void SetVisibleHands(bool value) {
        GameObject.Find("LeftHand Controller").layer = value ? 5 : 0;
        GameObject.Find("RightHand Controller").layer = value ? 5 : 0;
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

    public IEnumerator AudioFadeOut(AudioSource audioSource, float FadeTime)
    {

        float startVolume = audioSource.volume;//.5f

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.fixedUnscaledDeltaTime / FadeTime;
            //yield return null;
            yield return new WaitForSecondsRealtime(0.1f);

        }

        audioSource.Stop();
        audioSource.volume = 0.0f;
    }

    public IEnumerator AudioFadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        float startVolume = 0.5f;
        audioSource.volume = 0f;

        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += startVolume * Time.fixedUnscaledDeltaTime / FadeTime;
        
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
        audioSource.volume = 0.5f;
    }
}
