using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewMapContainer", menuName = "Map Container")]
public class MapContainer : ScriptableObject
{
    [Header("Tile Prefab")]
    [SerializeField, Tooltip("1")] private GameObject baseTile; // 기본 타일 (1)
    [SerializeField, Tooltip("0")] private GameObject emptyTile; // 빈 타일 (0)

    [SerializeField] private int[,] terrain; // 지형을 저장 할 2차원 배열
    
    public GameObject BaseTile
    {
        get{return baseTile;}
        set{baseTile = value;}
    }

    public GameObject EmptyTile
    {
        get{return emptyTile;}
        set{emptyTile = value;}
    }


    public int[,] Terrain
    {
        get{return terrain;}
        set{terrain = value;}
    }
}
