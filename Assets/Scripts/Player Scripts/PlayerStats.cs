using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float health;

    private float maxHealth = 1f;

    private float displayhealth;

    [SerializeField]
    private Image healthStats, staminaStats;

    private void Start()
    {
        maxHealth = 1f;
    }
    public void DisplayHealthStats(float healthValue)
    {
        Debug.Log(healthValue);

        healthValue /= 100f;
        Debug.Log("Tengo que restar :" + healthValue);

        healthStats.fillAmount = healthStats.fillAmount - healthValue;
        Debug.Log("vida es : " + healthStats.fillAmount);


        if (healthStats.fillAmount == 0)
        {
            SceneManagment sceneManagment = GetComponent<SceneManagment>();
            sceneManagment.ResetScene();
        }

    }
    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;


        staminaStats.fillAmount = staminaValue;
    }
    public void RestarVida(float damage)
    {

        health -= damage;

    }
    public void SumarVida(float heal)
    {
        heal /= 100f;

        healthStats.fillAmount += heal;

        if(healthStats.fillAmount >= maxHealth)
        {
            healthStats.fillAmount = maxHealth;
        }
    }
}
