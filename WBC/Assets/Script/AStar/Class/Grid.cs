using UnityEngine;

public class Grid
{
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Node[,] grid;

    public float nodeDiameter;
    public int gridSizeX, gridSizeY;

    public Grid(Vector2 gridWorldSize, float nodeRadius, Node[,] grid)
    {
        this.gridWorldSize = gridWorldSize;
        this.nodeRadius = nodeRadius;

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        this.grid = grid;
    }
}
