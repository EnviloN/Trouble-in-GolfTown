using UnityEngine;
using UnityEngine.UI;

public class PowerBarController : MonoBehaviour
{
    public int maxHeight = 200;

    protected float onePercentHeight;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        onePercentHeight = maxHeight / 100;
        rectTransform = GetComponent<RectTransform>();

        SetBarToPercents(0f);
    }

    public void SetBarToPercents(float percents)
    {
        if (percents == 0f)
        {
            enableComponents(false);
        } else
        {
            enableComponents(true);
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, percents * onePercentHeight);
    }

    protected void enableComponents(bool enable)
    {
        transform.parent.gameObject.GetComponent<Image>().enabled = enable;
        transform.parent.gameObject.GetComponent<Outline>().enabled = enable;
    }
}
