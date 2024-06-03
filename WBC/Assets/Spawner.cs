using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // 적 프리팹
    [SerializeField] private MapContainer mapContainer; // 맵 컨테이너

    public void OnClick()
    {
        int[,] terrain = mapContainer.Terrain; // 지형 데이터를 가져옴
        List<Vector2> spawnPositions = new List<Vector2>();

        // terrain 배열의 크기를 가져옵니다.
        int width = terrain.GetLength(0);
        int height = terrain.GetLength(1);

        // 중앙 기준으로 오프셋 계산
        Vector2 offset = new Vector2(width / 2f, height / 2f);

        // 지형 데이터에서 값이 1인 위치를 찾음
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrain[x, y] == 1)
                {
                    Vector2 spawnPosition = new Vector2(x, y) - offset;
                    spawnPositions.Add(spawnPosition);
                }
            }
        }

        // 스폰 위치가 존재하는 경우
        if (spawnPositions.Count > 0)
        {
            // 랜덤한 위치를 선택
            Vector2 randomPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];

            // 선택된 위치에 적 프리팹을 생성
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No valid spawn positions found in the terrain data.");
        }
    }
}
