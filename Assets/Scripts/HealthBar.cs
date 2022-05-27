using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Color low;

    [SerializeField]
    private Color high;

    [SerializeField]
    private Vector3 offset;

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    private void Update()
    {
        if(transform.parent != null)
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
