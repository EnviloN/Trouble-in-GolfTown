public class WorldGate : SceneGate
{ 
    public override void LoadScene() {
        var gm = FindObjectOfType<GameManager>();
        var tutorialStage = (int) gm.GetGameStatus("tutorialStage");
        if (tutorialStage == 2)
            gm.SetGameStatus("tutorialStage", 3);

        if (GM.debugMode || (int) FindObjectOfType<GameManager>().GetGameStatus("tutorialStage") >= 2)
            StartCoroutine(SceneLoader.LoadScene("World", warpPosition));
    }
}
