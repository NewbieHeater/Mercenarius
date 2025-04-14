using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeySetting.keys[KeyAction.UP]))
        {
            Debug.Log("Up");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.DOWN]))
        {
            Debug.Log("DOWN");
        }

        if (Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
        {
            Debug.Log("LEFT");
        }

        else if (Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
        {
            Debug.Log("RIGHT");
        }
    }
}
