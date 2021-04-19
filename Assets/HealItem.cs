using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{

    [SerializeField]
    private float heal = 25f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.transform.GetComponent<PlayerStats>();
            playerStats.SumarVida(heal);
            Destroy(gameObject);
        }
    }
}
