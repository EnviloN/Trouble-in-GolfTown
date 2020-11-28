public class WorldGate : SceneGate
{ 
    public override void LoadScene() {
        base.LoadScene();
        SceneLoader.LoadMainScene();
    }
}
