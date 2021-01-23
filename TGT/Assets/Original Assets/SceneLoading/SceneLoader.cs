using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public Animator transition;
    public Animator XRTransition;
    public float transitionTime = 1f;

    public AudioClip doorOpeningSound;
    public AudioClip doorClosingSound;

    private GameObject player;
    private DialogueManager dm;
    private PauseGame pauser;
    private AudioSource audioSource;
    private GameManager GM;
    private XRDetection detection;
    private bool start;

    private void Start() {
        dm = FindObjectOfType<DialogueManager>();
        GM = FindObjectOfType<GameManager>();
        pauser = gameObject.GetComponent<PauseGame>();
        audioSource = gameObject.GetComponent<AudioSource>();
        detection = FindObjectOfType<XRDetection>();
        start = true;
        LoadIntroScene();
    }

    private void Update()
    {}

    private static void LoadSceneAdditively(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }


    private void LoadIntroScene() {
        audioSource.volume = 0;
        SceneManager.LoadScene("World", LoadSceneMode.Single);
        LoadSceneAdditively("Town");
        LoadSceneAdditively("Dock");
        LoadSceneAdditively("MagnatesResidence");
        LoadSceneAdditively("TrainStation");
        LoadSceneAdditively("Cemetery");
        LoadSceneAdditively("MinigolfCourses");
        LoadSceneAdditively("Towers");
        //prepare main menu
        pauser.Pause();
        pauser.DisplayMainMenu();
    }


    public void StartGame() {
        if (start && !GM.debugMode)
        {
            start = false;
            StartCoroutine(LoadScene("ChurchInterior", new Vector3(-1.74f, 1.34f, -1.93f), true));
        }
        pauser.HideMenu();
        pauser.Resume();
        audioSource.volume = 1;
 
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

    public IEnumerator LoadScene(string sceneName, Vector3 warpPos, bool mute = false) {

        if (mute == false) {
            audioSource.clip = doorOpeningSound;
            audioSource.Play();
        }
        
        transition.SetTrigger("Start");
        if (detection.isXR)
        {
            XRTransition.SetTrigger("Start");
        }
        yield return new WaitForSeconds(transitionTime);

        player = GameObject.FindGameObjectWithTag("Player");
        if (detection.isXR)
        {
            player.transform.position = warpPos + Vector3.down;
        }
        else
        {
            player.transform.position = warpPos;
        }
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
        dm.RetreivePersistantStatuses();

        DummyPlayer dummyPlayerScript = player.GetComponent<DummyPlayer>();
        if (dummyPlayerScript)
        {
            dummyPlayerScript.OnSceneChange();
        }

        transition.SetTrigger("End");
        if (mute == false) {
            audioSource.clip = doorClosingSound;
            audioSource.Play();
        }
        
        if (detection.isXR)
        {
            XRTransition.SetTrigger("End");
        }
    }
}
