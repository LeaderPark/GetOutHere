using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Item
{
    private bool isUsed = true;

    public override void UseItem()
    {
        if (isUsed)
        {
            if (Player.Instance.health < Player.Instance.maxHp)
            {
                Player.Instance.health++;
            }
            isUsed = false;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PLAYER") && collision.transform.position.y < transform.position.y)
        {
            UseItem();
        }
    }
}
