using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    float damage = 20f;

    [SerializeField]
    float displayDamage = 20f;

    Rigidbody rb;

    [SerializeField]
    float speed = 500f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed * Time.deltaTime);

        Invoke("DestroyProjectile", 1.25f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerStats playerStats = collision.transform.GetComponent<PlayerStats>();
            playerStats.DisplayHealthStats(displayDamage);
            Destroy(gameObject);
        }

    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
