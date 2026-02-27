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
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddWater()
    {
        value += 10;
        UpdateBar();
    }

    public void UpdateBar()
    {
        float newHeight = (value / MAX_VALUE) * maxBar;

        if (newHeight > maxBar)
        {
            newHeight = maxBar;
        }

        barValue.sizeDelta = new Vector2(barValue.sizeDelta.x, newHeight);
    }

    public void DecreaseValue()
    {
        if (value > 0.0f)
        {
            value--;
        }
        else
        {
            value = 0.0f;
        }
        UpdateBar();
    }

    public bool CanDrink()
    {
        return value > 0;
    }
}
