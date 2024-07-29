using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ani : MonoBehaviour
{
    public UnityEvent animeEnded;
    void animeEnd()
    {
        animeEnded.Invoke();
    }
}
