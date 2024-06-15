using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfoContainer", menuName = "Stage Info Container")]

public class StageInfoContainer : ScriptableObject
{
    [Header("Stage")]
    [SerializeField] private int stage;
    [SerializeField] private int money;

    public void StartGame()
    {
        stage = 1;
        money = 0;
    }

    public int Stage
    {
        get{return stage;}
        set{stage = value;}
    }

    public int Money
    {
        get{return money;}
        set{money = value;}
    }
}