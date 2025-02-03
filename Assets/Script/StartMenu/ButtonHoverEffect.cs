using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverSprite; // 버튼 옆에 표시될 스프라이트
    private Vector2 offset = new Vector2(-180, 0); // 버튼 기준 스프라이트 위치 오프셋 (x: 오른쪽, y: 위쪽)

    private RectTransform buttonRect; // 버튼의 RectTransform
    private RectTransform spriteRect; // 스프라이트의 RectTransform

    private void Start()
    {
        // 버튼과 스프라이트의 RectTransform 가져오기
        buttonRect = GetComponent<RectTransform>();
        hoverSprite.SetActive(true);
        if (hoverSprite != null)
        {
            spriteRect = hoverSprite.GetComponent<RectTransform>();
            //hoverSprite.SetActive(false); // 기본적으로 비활성화
        }
    }

    // 마우스를 올렸을 때 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null && buttonRect != null && spriteRect != null)
        {
            // 버튼의 위치를 기준으로 스프라이트 위치 설정
            Vector3 buttonPosition = buttonRect.position;
            spriteRect.position = buttonPosition + new Vector3(offset.x, offset.y, 0);
            hoverSprite.SetActive(true); // 스프라이트 활성화
        }
    }

    // 마우스를 뗐을 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            //hoverSprite.SetActive(true); // 스프라이트 비활성화
        }
    }
}