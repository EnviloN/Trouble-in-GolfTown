using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IanMcGimme : MonoBehaviour {
    private GameManager GM;
    void Start() {
        GM = FindObjectOfType<GameManager>();

        var gameEnd = (int) GM.GetGameStatus("gameEnd");
        if (gameEnd == 1) {
            gameObject.SetActive(false);
            return;
        }
    }

}
