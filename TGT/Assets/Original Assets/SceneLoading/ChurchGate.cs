public class ChurchGate : SceneGate {
    public override void LoadScene() {
        StartCoroutine(SceneLoader.LoadScene("ChurchInterior", warpPosition));
    }
}
