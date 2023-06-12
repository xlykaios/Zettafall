using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    [SerializeField]
    public Slider slider;


    public void MaxHealthSet(int MaxHealth)
    {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
    }

    public void HealthbarUpdate(int health)
    {
        slider.value = health;
    }
}
