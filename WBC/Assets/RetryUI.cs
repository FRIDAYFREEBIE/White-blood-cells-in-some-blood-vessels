using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryUI : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene("InGame");
    }
}
