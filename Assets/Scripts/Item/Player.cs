using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 싱글톤.
    private static Player instance;
    public static Player Instance { get { return instance; }  }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy() 
    {
        instance = null;
    }
    #endregion

    public int health = 3;
    public int maxHp = 3;
}
