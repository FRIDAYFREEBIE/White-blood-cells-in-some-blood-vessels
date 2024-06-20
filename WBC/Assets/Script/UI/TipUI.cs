using UnityEngine;
using UnityEngine.EventSystems;

public class TipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;

    void Start()
    {
        popup.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (popup != null)
        {
            popup.SetActive(true); // 팝업 오브젝트를 활성화합니다.
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (popup != null)
        {
            popup.SetActive(false); // 팝업 오브젝트를 비활성화합니다.
        }
    }
}
