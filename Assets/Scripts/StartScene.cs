using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneManager.LoadScene("1floor");
    }
}
