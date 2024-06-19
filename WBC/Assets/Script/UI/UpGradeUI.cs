using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpGradeUI : MonoBehaviour
{
    public GameObject upgrade;

    public int temp = 0;

    void Start()
    {
        upgrade.SetActive(false);
    }

    public void OnClick()
    {
        if(temp%2 == 0 )
            upgrade.SetActive(true);
        else
            upgrade.SetActive(false);

        temp++;        
    }
}
