using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class MapLoading : MonoBehaviour
{
    private bool ready;
    public float LoadingWaitTime;
    //public TMP_Text Loading;
    public GameObject canvas;

    private void Update()
    {
        if (LoadingWaitTime <= 0 && ready == false)
        {
            Destroy(canvas);
            ready = true;
        }
        else
        {
            if (LoadingWaitTime >= 0)
            {
                //Loading.text = "Loading...";
                LoadingWaitTime -= Time.deltaTime;
            }
        }
    }
}
