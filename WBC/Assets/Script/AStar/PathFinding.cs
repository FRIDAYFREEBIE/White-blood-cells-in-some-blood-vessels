using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PathFinding : MonoBehaviour
{
    public Transform seeker; // 자신의 위치를 나타내는 Transform
    private Transform target; // 가장 가까운 타워의 위치를 나타내는 Transform
    public GridGenerator gridGenerator;

    void Awake()
    {
        gridGenerator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
    }

    void Update()
    {
        target = FindClosestTarget();
        if (target != null)
        {
            FindPath(seeker.position, target.position);
        }
        else
        {
            Debug.Log("target is NULL");
        }
    }

    Transform FindClosestTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        Transform closestTower = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(seeker.position, tower.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTower = tower.transform;
            }
        }

        return closestTower;
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = gridGenerator.NodeFromWorldPoint(startPos);
        Node targetNode = gridGenerator.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        Node closestNode = null;
        int closestNodeFCost = int.MaxValue;

        while (openSet.Count > 0) {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i ++) {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode) {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in gridGenerator.GetNeighbours(node)) {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

            if (node.hCost < closestNodeFCost && node.walkable) {
                closestNodeFCost = node.hCost;
                closestNode = node;
            }
        }

        if (closestNode != null) {
            RetracePath(startNode, closestNode);
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> tracePath = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            tracePath.Add(currentNode);
            currentNode = currentNode.parent;
        }
        tracePath.Reverse();

        gridGenerator.path = tracePath;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
