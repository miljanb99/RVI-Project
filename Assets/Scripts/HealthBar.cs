using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image maxHP;
    [SerializeField]
    private Image currentHP;

    [SerializeField]
    private Color low;

    [SerializeField]
    private Color high;

    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        if (transform.parent != null)
            maxHP.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void SetHealth(float health, float maxHealth)
    {
        if(health < 0) health = 0;
        currentHP.transform.localScale = new Vector3(health / maxHealth, currentHP.transform.localScale.y);
        currentHP.color = Color.Lerp(low, high, health/maxHealth);
    }

    private void Update()
    {
        if(transform.parent != null)
            maxHP.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
