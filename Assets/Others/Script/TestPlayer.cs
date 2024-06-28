using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [Header("�κ��丮")]
    public Inven inven;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
                if (hit.collider != null)
                {
                    HitCheckObject(hit);
                }
        }
    }

    void HitCheckObject(RaycastHit hit)
    {
        IObjectItem clickInterface = hit.transform.gameObject.GetComponent<IObjectItem>();

        if (clickInterface != null)
        {
            Item item = clickInterface.ClickItem();
            print($"{item.itemName}");
            inven.AddItem(item);
        }
    }
}
