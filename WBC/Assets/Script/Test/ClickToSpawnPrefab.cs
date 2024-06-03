using UnityEngine;

public class ClickToSpawnPrefab : MonoBehaviour
{
    public GameObject prefab1; // 첫 번째 프리팹
    public GameObject prefab2; // 두 번째 프리팹

    private bool canSpawn = true; // 프리팹 생성 가능 여부를 나타내는 플래그
    int temp = 0;

    void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸는지 확인하고 프리팹 생성이 가능한 상태인지 확인
        if (Input.GetMouseButtonDown(0) && canSpawn)
        {
            // 마우스 클릭 위치를 Ray로 변환
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // 충돌한 지점이 있거나 없어도 프리팹을 생성
            Vector3 spawnPosition = hit.collider != null ? hit.point : Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0; // Z 위치를 0으로 고정

            if(temp == 0)
            {
                Instantiate(prefab1, spawnPosition, Quaternion.identity);
                temp++;
            }
            else if (temp == 1)
                Instantiate(prefab2, spawnPosition, Quaternion.identity);
            else
                return;           
        }
    }
}
