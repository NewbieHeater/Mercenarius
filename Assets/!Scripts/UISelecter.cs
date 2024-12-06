using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelecter : MonoBehaviour
{
    public void Select(int index)
    {
        CharacterManager.Instance.SelectCharacter(index);
        Destroy(gameObject);
    }
}
