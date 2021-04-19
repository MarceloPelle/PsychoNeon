using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField]
    private float enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 100f;
    }
    private void Update()
    {

    }

    public void DamageEnemyHealth(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("la vida del enemigo es " + enemyHealth);

        if(enemyHealth <= 0)
        {
            DestroyEnemy();
            EnemyDrop enemyDrop = GetComponent<EnemyDrop>();
            enemyDrop.DropAnItem();
        }

    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
