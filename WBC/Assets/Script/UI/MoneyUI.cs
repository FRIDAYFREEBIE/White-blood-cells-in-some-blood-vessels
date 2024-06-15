using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField]  private GameManager gameManager;

    [Header("Text")]
    [SerializeField]  private TextMeshProUGUI text;

    void Update()
    {
        text.text = gameManager.CurrentMoney().ToString() + "$";
    }
}
