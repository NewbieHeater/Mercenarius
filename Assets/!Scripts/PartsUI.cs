using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsUI : MonoBehaviour
{
    GameObject player;
    IParts parts;

    void Start()
    {
        player = CharacterManager.Instance.GetCharacter();
        parts = player.GetComponent<IParts>();
    }

    public void EnScope()
    {
        if (parts == null)
        {
            Debug.Log("해당 캐릭터는 파츠를 장착할 수 없습니다.");
            return;
        }

        if (player.GetComponent<Scope>() != null)
        {
            Debug.Log("해당 캐릭터는 이미 해당 파츠를 장착하고 있습니다.");
            return;
        }

        parts.EnPart<Scope>();
        Debug.Log("파츠 장착 완료!");
    }

    public void EnGrib()
    {
        if (parts == null)
        {
            Debug.Log("해당 캐릭터는 파츠를 장착할 수 없습니다.");
            return;
        }

        if (player.GetComponent<Grib>() != null)
        {
            Debug.Log("해당 캐릭터는 이미 해당 파츠를 장착하고 있습니다.");
            return;
        }

        parts.EnPart<Grib>();
        Debug.Log("파츠 장착 완료!");
    }

    public void DeScope()
    {
        if (parts == null)
        {
            Debug.Log("해당 캐릭터는 파츠를 장착할 수 없습니다.");
            return;
        }

        if (player.GetComponent<Scope>() == null)
        {
            Debug.Log("해당 캐릭터는 해당 파츠를 장착하고 있지 않습니다.");
            return;
        }

        parts.DePart<Scope>();
        Debug.Log("파츠 해제 완료!");
    }

    public void DeGrib()
    {
        if (parts == null)
        {
            Debug.Log("해당 캐릭터는 파츠를 장착할 수 없습니다.");
            return;
        }

        if (player.GetComponent<Grib>() == null)
        {
            Debug.Log("해당 캐릭터는 해당 파츠를 장착하고 있지 않습니다.");
            return;
        }

        parts.DePart<Grib>();
        Debug.Log("파츠 해제 완료!");
    }
}
