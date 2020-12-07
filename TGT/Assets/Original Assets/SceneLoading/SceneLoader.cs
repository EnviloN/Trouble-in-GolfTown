﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public Animator transition;
    public float transitionTime = 1f;

    private GameObject player;
    private DialogueManager dm;
    private GameManager GM;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        dm = FindObjectOfType<DialogueManager>();
        GM = FindObjectOfType<GameManager>();

        if (GM.debugMode) {
            LoadMainScene();
            return;
        }

        LoadChurchScene();
        player.transform.position = new Vector3(-1.74f, 1.34f, -1.93f); // church by the bed

    }

    private static void LoadSceneAdditively(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void LoadMainScene() {
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
        dm.RetreivePersistantStatuses();
        dm.UpdateGraphs();

        transition.SetTrigger("End");
    }
}
