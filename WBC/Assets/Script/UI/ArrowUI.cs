using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    [Header("Select Menu")]
    [SerializeField] private RectTransform selectMenu;

    [Header("Arrows")]
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;

    private int temp = 0;

    public void OnClick()
    {
        if (temp % 2 == 0)
        {
            selectMenu.anchoredPosition = new Vector2(0, -185);
            upArrow.SetActive(true);
            downArrow.SetActive(false);
        }
        else
        {
            selectMenu.anchoredPosition = new Vector2(0, 0);
            upArrow.SetActive(false);
            downArrow.SetActive(true);
        }

        temp++; 
    }
}
