using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetMax(int max)
    {
        slider.maxValue = max;
        //fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(int currentValue)
    {
        slider.value = currentValue;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
