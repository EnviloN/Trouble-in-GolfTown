using UnityEngine;

public class ScenePersistent : MonoBehaviour
{
    void Awake()
    {
        this.transform.SetParent(null);
        DontDestroyOnLoad(transform.gameObject);
    }
}
