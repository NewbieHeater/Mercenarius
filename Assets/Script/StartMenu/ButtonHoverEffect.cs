using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverSprite; // ��ư ���� ǥ�õ� ��������Ʈ
    private Vector2 offset = new Vector2(-180, 0); // ��ư ���� ��������Ʈ ��ġ ������ (x: ������, y: ����)

    private RectTransform buttonRect; // ��ư�� RectTransform
    private RectTransform spriteRect; // ��������Ʈ�� RectTransform

    private void Start()
    {
        // ��ư�� ��������Ʈ�� RectTransform ��������
        buttonRect = GetComponent<RectTransform>();
        hoverSprite.SetActive(true);
        if (hoverSprite != null)
        {
            spriteRect = hoverSprite.GetComponent<RectTransform>();
            //hoverSprite.SetActive(false); // �⺻������ ��Ȱ��ȭ
        }
    }

    // ���콺�� �÷��� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null && buttonRect != null && spriteRect != null)
        {
            // ��ư�� ��ġ�� �������� ��������Ʈ ��ġ ����
            Vector3 buttonPosition = buttonRect.position;
            spriteRect.position = buttonPosition + new Vector3(offset.x, offset.y, 0);
            hoverSprite.SetActive(true); // ��������Ʈ Ȱ��ȭ
        }
    }

    // ���콺�� ���� �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            //hoverSprite.SetActive(true); // ��������Ʈ ��Ȱ��ȭ
        }
    }
}