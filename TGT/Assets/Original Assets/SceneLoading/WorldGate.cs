public class WorldGate : SceneGate
{ 
    public override void LoadScene() {
        StartCoroutine(SceneLoader.LoadScene("World", warpPosition));
    }
}
