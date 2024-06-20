using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTextUI : MonoBehaviour
{
    enum TextType
    {
        Enemy,
        Tower
    }

    [Header("TextType")]
    [SerializeField] private TextType type;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;

    private BasicEnemy basicEnemy;
    private BasicTower basicTower;

    private int level;

    void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();  
        basicTower = GetComponent<BasicTower>();
    }

    void Update()
    {
        if(type == TextType.Enemy && basicEnemy != null)
            level = basicEnemy.level;
        else if(type == TextType.Tower && basicTower != null)
            level = basicTower.ReturnStat().level;

        text.text = level.ToString();
    }
}