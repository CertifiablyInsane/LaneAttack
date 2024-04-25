using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void SetValue(float value, float max)
    {
        slider.value = value / max;
    }
}
