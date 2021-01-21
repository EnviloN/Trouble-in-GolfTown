using UnityEngine;

public class DrunkenJoe : MonoBehaviour {
    private GameManager GM;
    // Start is called before the first frame update
    void Start() {
        GM = FindObjectOfType<GameManager>();
        var quest1Stage = (int) GM.GetGameStatus("quest1Stage");
        var quest2Stage = (int) GM.GetGameStatus("quest2Stage");
        if (!GM.debugMode && (quest1Stage == 4 || quest2Stage == 1))
            transform.gameObject.GetComponent<Animator>().SetBool("NeedsAttention", true);
    }
}
