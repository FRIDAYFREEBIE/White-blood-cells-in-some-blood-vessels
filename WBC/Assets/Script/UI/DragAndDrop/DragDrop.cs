using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject towerPrefab;  // 사용할 프리팹
    public LayerMask placeablelayer; // 충돌을 감지할 레이어 마스크

    private GameObject currentDraggedPrefab;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));

        currentDraggedPrefab = Instantiate(towerPrefab, worldPosition, Quaternion.identity);
        offset = currentDraggedPrefab.transform.position - worldPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentDraggedPrefab != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
            Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, placeablelayer);

            if (hit.collider != null)
                currentDraggedPrefab.transform.position = hit.point + (Vector2)offset;
            else
                Destroy(currentDraggedPrefab);

            currentDraggedPrefab = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentDraggedPrefab != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
            currentDraggedPrefab.transform.position = worldPosition + offset;
        }
    }
}
