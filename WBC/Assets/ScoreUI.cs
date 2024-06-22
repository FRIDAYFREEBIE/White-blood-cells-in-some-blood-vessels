using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public StageInfoContainer stageInfoContainer;

    void Start()
    {
        text.text = "STAGE: "+ stageInfoContainer.Stage.ToString();
    }
}
