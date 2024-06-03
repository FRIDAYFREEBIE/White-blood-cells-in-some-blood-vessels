using UnityEngine;

[ExecuteInEditMode]
public class TerrainGeneratorDebugger : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private MapGenerator mapGenerator;
    //[SerializeField] private GridGenerator gridGenerator;

    public void Start()
    {
        terrainGenerator.Generate();
        mapGenerator.Generate();
    }

    // void OnDrawGizmos()
    // {
    //     if (terrainGenerator == null || terrainGenerator.terrain == null)
    //         return;

    //     int[,] terrain = terrainGenerator.terrain;
    //     int width = terrain.GetLength(0);
    //     int height = terrain.GetLength(1);

    //     float tileSize = 1f;

    //     for (int x = 0; x < width; x++)
    //     {
    //         for (int y = 0; y < height; y++)
    //         {
    //             Gizmos.color = (terrain[x, y] == 1) ? Color.black : Color.white;

    //             Vector3 pos = new Vector3(-width / 2f + x + 0.5f, height - y - 1, -height / 2f + y + 0.5f) * tileSize;
    //             Gizmos.DrawCube(transform.position + pos, Vector3.one * tileSize);
    //         }
    //     }
    // }
}
