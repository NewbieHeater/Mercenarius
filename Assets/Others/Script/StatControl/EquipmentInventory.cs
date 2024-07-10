using UnityEngine;
using TMPro;

public class EquipmentInventory : InventoryBase
{
    public static bool IsInventoryActive = false;  // 인벤토리 활성화 되었는가?

    [Header("현재 계산된 수치를 표현할 텍스트 라벨들")]
    [SerializeField] private TextMeshProUGUI mDamageLabel;
    [SerializeField] private TextMeshProUGUI mDefenseLabel;


    new private void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        //옵션이 켜져있는경우 비활성화
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