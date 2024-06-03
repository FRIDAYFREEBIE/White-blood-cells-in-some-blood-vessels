using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownArrows : MonoBehaviour
{
    [Header("Arrows")]
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;

    private void Start()
    {
        upArrow.SetActive(false);
        downArrow.SetActive(true);
    }
}
