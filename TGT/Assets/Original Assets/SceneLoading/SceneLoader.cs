using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject MusicPlayer;

    private GameObject player;
    private DialogueManager dm;
    private GameObject canvas;
    private bool isPaused;
    private PauseGame pauser;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Menu");
        dm = FindObjectOfType<DialogueManager>();
        pauser = gameObject.GetComponent<PauseGame>();
        LoadIntroScene();
    }

    private void Update()
    {}

    private static void LoadSceneAdditively(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void LoadIntroScene() {
        SceneManager.LoadScene("World", LoadSceneMode.Single);
        LoadSceneAdditively("Town");
        LoadSceneAdditively("Dock");
        LoadSceneAdditively("MagnatesResidence");
        LoadSceneAdditively("TrainStation");
        LoadSceneAdditively("Cemetery");
        LoadSceneAdditively("MinigolfCourses");
        LoadSceneAdditively("Towers");

        //ground Player movement
        //isPaused = true;
        pauser.Pause();
        pauser.DisplayMainMenu();
        
        //player.GetComponent<FreezeMovement>().Freeze();
    }


    public void StartGame() {
        //fadeout audio
        StartCoroutine(AudioFadeOut(MusicPlayer.GetComponent<AudioSource>(), 2.0f));
        //unfreeze player
        //player.GetComponent<FreezeMovement>().UnFreeze();
        //disable menu
        pauser.HideMenu();
        pauser.Resume();
        //canvas.SetActive(false); //this would be nice with animation
        //isPaused = false;
    }

    public void LoadMainScene() {
        SceneManager.LoadScene("World", LoadSceneMode.Single);
        LoadSceneAdditively("Town");
        LoadSceneAdditively("Dock");
        LoadSceneAdditively("MagnatesResidence");
        LoadSceneAdditively("TrainStation");
        LoadSceneAdditively("Cemetery");
        LoadSceneAdditively("MinigolfCourses");
        LoadSceneAdditively("Towers");
        
    }

    private void LoadSaloonScene() {
        SceneManager.LoadScene("SaloonInterior", LoadSceneMode.Single);
    }
    private void LoadChurchScene() {
        SceneManager.LoadScene("ChurchInterior", LoadSceneMode.Single);
    }

    public IEnumerator LoadScene(string sceneName, Vector3 warpPos) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        player.transform.position = warpPos;
        switch (sceneName) {
            case "World":
                LoadMainScene();
                break;
            case "SaloonInterior":
                LoadSaloonScene();
                break;
            case "ChurchInterior":
                LoadChurchScene();
                break;
        }
        dm.UpdateGraphs();

        transition.SetTrigger("End");
    }

    public IEnumerator AudioFadeOut(AudioSource audioSource, float FadeTime)
    {

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            Debug.Log("Fade out Coroutine running. vol: " + audioSource.volume);
            Debug.Log("time scale: " + Time.timeScale);
            yield return null;

        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        Debug.Log("Fade out Coroutine ended.");
    }
}
