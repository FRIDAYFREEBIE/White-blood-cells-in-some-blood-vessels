using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Generator
{
    [Header("MapContainer")]
    [SerializeField] private MapContainer mapContainer;

    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

    [Header("Prefab")]
    [SerializeField] private GameObject baseTilePrefab; // 기본 타일 (1)
    [SerializeField] private GameObject emptyTilePrefab; // 빈 타일 (0)

    private int[,] terrain;

    [Header("Generation Settings")]
    [SerializeField] private int tilesPerFrame = 10; // 프레임마다 생성할 타일 수

    // Generate 메서드 오버라이드
    public override void Generate()
    {
        // 기존 자식 오브젝트를 모두 삭제
        ClearChildren();

        terrain = mapContainer.Terrain;

        baseTilePrefab = mapContainer.BaseTile;
        emptyTilePrefab = mapContainer.EmptyTile;

        if (Application.isPlaying)
            StartCoroutine(GenerateTiles());
    }

    private void ClearChildren()
    {
        // 플레이 모드에서 실행할 경우 Destroy 사용
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private IEnumerator GenerateTiles()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain data is null.");
            yield break;
        }

        // terrain 배열의 크기를 가져옵니다.
        int width = terrain.GetLength(0);
        int height = terrain.GetLength(1);

        // 중앙 기준으로 오프셋 계산
        Vector3 offset = new Vector3(width / 2f, height / 2f, 0);

        // 타일 생성 루프
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tilePrefab = terrain[x, y] == 1 ? baseTilePrefab : emptyTilePrefab;
                Vector3 position = new Vector3(x, y, 0) - offset;

                Instantiate(tilePrefab, position, Quaternion.identity, transform);

                if ((x * height + y) % tilesPerFrame == 0)
                {
                    yield return null; // 다음 프레임까지 대기
                }
            }
        }

        gameManager.StartGame();
        yield break;
    }
}
