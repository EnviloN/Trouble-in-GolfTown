using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOut : MonoBehaviour
{

    public float fadeSpeed = .05f;
    public IEnumerator ScaleOutTower()
    {

        while (this.transform.localScale.y > 0.0)
        {
            Vector3 objectScale = this.transform.localScale;
            float fadeAmount = (fadeSpeed * Time.deltaTime);

            Debug.Log("localScale: " + objectScale + fadeAmount);
            objectScale = new Vector3(objectScale.x - fadeAmount, objectScale.y - fadeAmount, objectScale.z - fadeAmount);
            this.transform.localScale = objectScale;
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleOutTower());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
