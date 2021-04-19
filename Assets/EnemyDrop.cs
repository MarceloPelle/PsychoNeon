using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDrop : MonoBehaviour
{
    public List<GameObject> droppableItems;

    public int[] table = {
    60, //Heal
    30, //Pistol
    20, //Shotgun
    15 //Rifle
    };

    public int total;
    public int randomNumber;

    public void DropAnItem()
    {
        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                Instantiate(droppableItems[i], transform.position, Quaternion.identity);
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }
}
