using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : Item
{
    public GameObject player;

    public override void UseItem()
    {
        player.transform.Translate( new Vector3(0, player.transform.position.y+2.7f, 0));

        Destroy(this);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("STAIR"))
        {
            Debug.Log(col);
            UseItem();
        }
        else if (col.gameObject.CompareTag("CLEARSTAIR"))
        {
            SceneManager.LoadScene("GameClear");
        }
    }
}
