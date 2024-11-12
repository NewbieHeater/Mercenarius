using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private List<IBuff> activeBuffs = new List<IBuff>();

    public void AddBuff(IBuff buff, GameObject target)
    {
        var existingBuff = activeBuffs.Find(b => b.BuffName == buff.BuffName);
        if (existingBuff != null)
        {
            existingBuff.Remove(target);
            activeBuffs.Remove(existingBuff);
        }

        activeBuffs.Add(buff);
        buff.Apply(target);
        StartCoroutine(RemoveBuffAfterDuration(buff, target));
    }

    private System.Collections.IEnumerator RemoveBuffAfterDuration(IBuff buff, GameObject target)
    {
        yield return new WaitForSeconds(buff.Duration);
        buff.Remove(target);
        activeBuffs.Remove(buff);
    }

    public void RemoveBuff(IBuff buff, GameObject target)
    {
        if (activeBuffs.Contains(buff))
        {
            buff.Remove(target);
            activeBuffs.Remove(buff);
        }
    }
}
