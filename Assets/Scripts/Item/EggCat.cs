using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCat : Item
{
    public GameObject Cat;

    public override void UseItem()
    {
        Instantiate(Cat, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("GROUND"))   //바닥에 그라운드 태그 넣어야해요
        {
            UseItem();
        }
    }
}