using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOSkill : ScriptableObject
{
    public float damage;
    public float Cooltime;
    public float SkillDuration;
    public float SkillDistance;

    public string animationName;
    public Sprite icon;
}