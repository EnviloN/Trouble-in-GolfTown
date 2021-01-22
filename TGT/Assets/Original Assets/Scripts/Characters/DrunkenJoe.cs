using UnityEngine;

public class DrunkenJoe : MonoBehaviour {
    private GameManager GM;
    // Start is called before the first frame update
    void Start() {
        GM = FindObjectOfType<GameManager>();
        var quest1Stage = (int) GM.GetGameStatus("quest1Stage");
        if (!GM.debugMode && quest1Stage == 4)
            transform.gameObject.GetComponent<Animator>().SetBool("NeedsAttention", true);
    }
}
