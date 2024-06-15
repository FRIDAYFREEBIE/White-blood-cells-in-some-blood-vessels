using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spanwer
{
    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

    [Header("MapContainer")]
    [SerializeField] private MapContainer mapContainer;

    [Header("Enemies")]
    [SerializeField] private Enemy[] enemies; // 0: 일반 적, 1: 보스 적

    [Header("Current Enemies")]
    [SerializeField] private List<Enemy> currentEnemies = new List<Enemy>();

    public void Start()
    {
        currentEnemies = new List<Enemy>();
    }

    public override void Spawn()
    {
        int currentStage = gameManager.CurrentStage();

        // Normal
        for (int i = 1; i <= currentStage * 2; i++)
        {
            SpawnEnemy(enemies[0]);
        }

        // Boss
        if (currentStage % 5 == 0)
        {
            SpawnEnemy(enemies[1]);
        }
    }


    // 스폰
    private void SpawnEnemy(Enemy enemyPrefab)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies.Add(spawnedEnemy);
    }

    // 랜덤 위치
    private Vector3 GetRandomSpawnPosition()
    {
        int width = mapContainer.Terrain.GetLength(0);
        int height = mapContainer.Terrain.GetLength(1);

        List<Vector3> validPositions = new List<Vector3>();

        Vector3 offset = new Vector3(width / 2f, height / 2f, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapContainer.Terrain[x, y] == 1)
                {
                    Vector3 position = new Vector3(x, y, 0) - offset;
                    validPositions.Add(position);
                }
            }
        }

        int randomIndex = Random.Range(0, validPositions.Count);
        return validPositions[randomIndex];
    }
}
