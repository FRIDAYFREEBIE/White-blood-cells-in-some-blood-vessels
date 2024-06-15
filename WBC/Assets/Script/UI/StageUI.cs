using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField]  private GameManager gameManager;

    [Header("Text")]
    [SerializeField]  private TextMeshProUGUI text;

    void Update()
    {
        string newString;

        if(gameManager.CurrentStage() < 10)
        {
            newString = "0" + gameManager.CurrentStage();
        }
        else
            newString = gameManager.CurrentStage().ToString();

        text.text = newString;
    }
}
