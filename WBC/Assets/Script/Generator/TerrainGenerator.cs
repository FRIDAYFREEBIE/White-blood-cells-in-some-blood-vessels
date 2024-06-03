using UnityEngine;

// 지형 생성 스크립트
public class TerrainGenerator : Generator
{
    [Header("TerrainGenerator")]
    [SerializeField, Range(0,100)] private int randomFillPercent;
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Seed")]
    [SerializeField] private string seed;
    [SerializeField] private bool useRandomSeed;

    [Header("SmoothCoefficient")]
    [SerializeField] private int smooth;

    [Header("MapContainer")]
    [SerializeField] private MapContainer mapContainer;

    [HideInInspector] public int[,] terrain; // 지형을 저장 할 2차원 배열

    // Generate 메서드 오버라이드
    public override void Generate()
    {
        terrain = new int[width,height];

        RandomFillTerrain();

        for(int i = 0; i < smooth; i++)
        {
            SmoothTerrain();
        }

        mapContainer.Terrain = terrain;
    }

    // 지형을 채워주는 매서드
    void RandomFillTerrain()
    {
        // 랜덤 시드 생성
        if(useRandomSeed)
            seed = Time.time.ToString();

        System.Random rmd = new System.Random(seed.GetHashCode());

        // 지형 채우기
        // 0 = 비워지는 공간        
        // 1 = 채워지는 공간
        for(int x = 0; x < width ; x++)
        {
            for(int y = 0; y < height; y++)
            {
                terrain[x,y] = (rmd.Next(0,100) < randomFillPercent) ? 1 : 0; // 1 = 채워지는 공간 0 = 비워지는 공간
            }
        }
    }

    // 지형을 다듬는 매서드
    void SmoothTerrain()
    {
        for(int x = 0; x < width ; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int neighbourWall = GetSurroundingEndCount(x,y);

                if (neighbourWall > 4)
                    terrain[x,y] = 1;
                else if (neighbourWall < 4)
                    terrain[x,y] = 0;
            }
        }
    }

    // 인접 한 맵의 끝 부분의 갯수를 반환하는 매서드
    int GetSurroundingEndCount(int gridX, int gridY)
    {
        int wallCount = 0;

        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                        wallCount += terrain[neighbourX,neighbourY];
                }
                else
                    wallCount ++;
            }
        }

        return wallCount;
    }
}
