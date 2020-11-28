public class SaloonGate : SceneGate
{
    public override void LoadScene() {
        StartCoroutine(SceneLoader.LoadScene("SaloonInterior", warpPosition));
    }
}
