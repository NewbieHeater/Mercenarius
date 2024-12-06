using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParts
{
    public void EnPart<T>() where T : Parts;
    public void DePart<T>() where T : Parts;
}
