using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipTextUI : MonoBehaviour
{
    [SerializeField] private TowerType towerType;
    [SerializeField] private TextMeshProUGUI text;


    void Start()
    {
        switch (towerType)
        {
            case TowerType.Scout:
                text.text = "Type: Scout\nprice: 50";
                break;
            case TowerType.Shotgun:
                text.text = "Type: Shotgun\nprice: 100";
                break;
            case TowerType.Ranger:
                text.text = "Type: Ranger\nprice: 200";
                break;
            case TowerType.Railgun:
                text.text = "Type: Railgun\nprice: 100000";
                break;
            case TowerType.Farm:
                text.text = "Type: Farm\nprice: 500";
                break;
            case TowerType.Commander:
                text.text = "Type: Commander\nprice: 500";
                break;
            case TowerType.Freezer:
                text.text = "Type: Freezer\nprice: 500";
                break;
            default:
                break;
        }
    }
}
