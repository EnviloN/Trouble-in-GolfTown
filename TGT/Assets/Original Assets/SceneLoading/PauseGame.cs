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

    public  GameObject canvas;
    public GameObject main_menu;
    public GameObject settings_menu;

    public GameObject MusicPlayer;
    public float audioFadeOut = 1.0f;

    GameObject player;
    public static bool isPaused;
    public GameObject[] hands;
    private XRRayInteractor rayInteractor;
    private bool isXR = false;

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
        if (Input.GetMouseButtonDown(0))// || Input.GetKeyDown(KeyCode.H) || Input.GetMouseButtonDown(0))
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
        TryInitializeXR();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
            Pause();
            DisplayMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused == true) {  
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
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Game presumably ended.");
                Application.Quit();
            }
        }

        if (!isXR)
        {
            TryInitializeXR();
        }
    }

    void TryInitializeXR()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<XRRig>())
        {
            isXR = true;
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
                SetRaycast(true);
            }
        }
    }

    void PositionCanvas()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<XRRig>())
        {
            canvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 10.0f + Vector3.up * 1.2f;
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
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
    }

    public void Pause()
    {
        //fadein music
        StartCoroutine(AudioFadeIn(MusicPlayer.GetComponent<AudioSource>(), audioFadeOut));
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        // SetLayerRecursively(player, 5);
        // hands = GameObject.FindGameObjectsWithTag("Hands");
        // foreach (GameObject hand in hands)
        // {
        //     SetLayerRecursively(hand, 5);
        // }
        player.GetComponent<FreezeMovement>().Freeze();
        SetRaycast(true);
    }

    public void Resume() {
        Cursor.visible = false;
        Time.timeScale = 1;
        //fadeout audio
        StartCoroutine(AudioFadeOut(MusicPlayer.GetComponent<AudioSource>(), audioFadeOut));
        player = GameObject.FindGameObjectWithTag("Player");
        // SetLayerRecursively(player, 0);
        // hands = GameObject.FindGameObjectsWithTag("Hands");
        // foreach (GameObject hand in hands)
        // {
        //     SetLayerRecursively(hand, 0);
        // }
        isPaused = false;
        player.GetComponent<FreezeMovement>().UnFreeze();
        SetRaycast(false);
    }

    void SetRaycast(bool pause) {
        if (player.GetComponent<XRRig>())
        {
            rayInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
            rayInteractor.velocity = pause ? 50 : 5;
        }
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
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.fixedUnscaledDeltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator AudioFadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        float startVolume = 0f;
        audioSource.volume = 0f;

        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += startVolume * Time.fixedUnscaledDeltaTime / FadeTime;
            //Debug.Log("fading audio in. "+ audioSource.volume);
            yield return Time.fixedUnscaledDeltaTime;
        }
        
        audioSource.volume = 0.5f;
    }
}
