using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{
    [Header("moveSpeed")]
    [SerializeField] private float moveSpeed; // 적의 이동 속도
    private Transform target; // 가장 가까운 타워의 위치를 나타내는 Transform
    private GridGenerator gridGenerator;
    private List<Node> currentPath = new List<Node>(); // 현재 이동 경로를 저장하는 리스트

    void Awake()
    {
        gridGenerator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
    }

    void Update()
    {
        target = FindClosestTarget();
        if (target != null)
        {
            FindPath(transform.position, target.position);
            MoveAlongPath(); // 경로 따라 이동하는 메서드 호출
        }
    }

    Transform FindClosestTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        Transform closestTower = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
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

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in gridGenerator.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

            if (node.hCost < closestNodeFCost && node.walkable)
            {
                closestNodeFCost = node.hCost;
                closestNode = node;
            }
        }

        if (closestNode != null)
        {
            RetracePath(startNode, closestNode);
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        currentPath = path; // 경로를 currentPath에 저장
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    void MoveAlongPath()
    {
        if (currentPath == null || currentPath.Count == 0)
            return;

        // 현재 위치에서 첫 번째 노드로 이동
        Node targetNode = currentPath[0];
        Vector3 targetPosition = gridGenerator.WorldPointFromNode(targetNode); // 월드 포인트로 변환

        // 현재 위치에서 목표 위치까지의 방향 계산
        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        transform.position = newPosition;

        // 목표 위치에 도달했으면 해당 노드를 경로에서 제거
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPath.RemoveAt(0);
        }
    }
}
