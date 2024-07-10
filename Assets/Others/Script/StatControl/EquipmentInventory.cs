using UnityEngine;
using TMPro;

public class EquipmentInventory : InventoryBase
{
    public static bool IsInventoryActive = false;  // �κ��丮 Ȱ��ȭ �Ǿ��°�?

    [Header("���� ���� ��ġ�� ǥ���� �ؽ�Ʈ �󺧵�")]
    [SerializeField] private TextMeshProUGUI mDamageLabel;
    [SerializeField] private TextMeshProUGUI mDefenseLabel;


    new private void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        //�ɼ��� �����ִ°�� ��Ȱ��ȭ
        //if (GameMenuManager.IsOptionActive) { return; }

        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Equipment")))
        {
            if (mInventoryBase.activeInHierarchy)
            {
                mInventoryBase.SetActive(false);
                IsInventoryActive = false;
            }
            else
            {
                mInventoryBase.SetActive(true);
                IsInventoryActive = true;
            }
        }
    }
}