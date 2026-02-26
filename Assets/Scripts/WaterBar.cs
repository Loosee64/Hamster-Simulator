using System.Collections;
using UnityEngine;

public class WaterBar : MonoBehaviour
{
    [SerializeField]
    RectTransform barValue;
    [SerializeField]
    int maxBar;

    float value = 0.0f;
    const int MAX_VALUE = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateBar();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addWater()
    {
        value += 10;
        updateBar();
    }

    public void updateBar()
    {
        float newHeight = (value / MAX_VALUE) * maxBar;

        if (newHeight > maxBar)
        {
            newHeight = maxBar;
        }

        barValue.sizeDelta = new Vector2(barValue.sizeDelta.x, newHeight);
    }

    public void decreaseValue()
    {
        if (value > 0.0f)
        {
            value--;
        }
        else
        {
            value = 0.0f;
        }
        updateBar();
    }
}
