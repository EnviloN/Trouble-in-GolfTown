using UnityEngine;

public class DrunkenJoe : MonoBehaviour {
    private GameManager GM;
    // Start is called before the first frame update
    void Start() {
        GM = FindObjectOfType<GameManager>();
        var quest1Stage = (int) GM.GetGameStatus("quest1Stage");
        var quest2Stage = (int) GM.GetGameStatus("quest2Stage");
        var quest3Stage = (int) GM.GetGameStatus("quest3Stage");
        var numOfTowersDestroyed = (int) GM.GetGameStatus("numOfTowersDestroyed");
        //|| (numOfTowersDestroyed == 4 && quest3Stage == 0)
        if (!GM.debugMode && (quest1Stage == 4 || quest2Stage == 1 ))
            transform.gameObject.GetComponent<Animator>().SetBool("NeedsAttention", true);
    }
}
