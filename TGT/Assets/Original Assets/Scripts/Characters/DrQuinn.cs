using UnityEngine;
using UnityEngine.SceneManagement;

public class DrQuinn : MonoBehaviour {
    private GameManager GM;

    void Start() {
        GM = FindObjectOfType<GameManager>();

        var sceneName = SceneManager.GetActiveScene().name;
        var tutorialStage = (int) GM.GetGameStatus("tutorialStage");
        if (sceneName == "ChurchInterior" && (tutorialStage >= 2 && tutorialStage <= 5) ||
            sceneName == "World" && tutorialStage > 4) {
            gameObject.SetActive(false);
            return;
        }

        if (!GM.debugMode && tutorialStage == 0 || tutorialStage == 3) 
            transform.gameObject.GetComponent<Animator>().SetBool("NeedsAttention", true);
    }

}
