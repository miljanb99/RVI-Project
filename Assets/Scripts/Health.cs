using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health;
    int maxHealth;

    [SerializeField]
    private GameObject objectToDestroy;

    public HealthBar healthBar;

    private Color myColor;


    private void Awake()
    {
        myColor = GetComponent<SpriteRenderer>().color;
        maxHealth = health;
        healthBar.SetHealth(health, maxHealth);
    }

    public void SetupMaxHP(int maxHealth = 100)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
        
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = myColor;
    }

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        health -= amount;
        healthBar.SetHealth(health, maxHealth);
       
        StartCoroutine(VisualIndicator(Color.red));

        if(health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        bool wouldBeOverMaxHealth = health + amount > maxHealth;
        StartCoroutine(VisualIndicator(Color.green));


        if (wouldBeOverMaxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
    }

    public void setHealth(int amount)
    {
        health = amount;
    }

    private void Die()
    {       
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() != null)
        {
            if (objectToDestroy.CompareTag("Level1Boss"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().bossDestroyed();
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().updateNumberOfKilledEnemies();
            }
        }
        if (objectToDestroy.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().Defeat();
        }
        else
        {
            Destroy(objectToDestroy);
        }
    }

}
