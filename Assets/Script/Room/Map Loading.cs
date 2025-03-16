using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using TMPro;

public class MapLoading : MonoBehaviour
{
    private bool ready;
    public float LoadingWaitTime;
    //public TMP_Text Loading;
    public GameObject canvas;
    public UnityEvent mapGenerateEnd;
    private void Update()
    {
        if (LoadingWaitTime <= 0 && ready == false)
        {
            Destroy(canvas);
            ready = true;
            mapGenerateEnd.Invoke();
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
