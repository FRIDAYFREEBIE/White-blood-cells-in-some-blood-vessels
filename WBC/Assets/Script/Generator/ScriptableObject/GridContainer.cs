using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGridContainer", menuName = "Grid Container")]
public class GridContainer : ScriptableObject
{
    private Grid grid;

    public Grid Grid
    {
        get
        {
            return grid;
        }
        set
        {
            grid = value;
        }
    }
}