using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Parts : MonoBehaviour
{
    public virtual void Ability() { }

    public virtual void DePart()
    {
        Destroy(this);
    }
}
