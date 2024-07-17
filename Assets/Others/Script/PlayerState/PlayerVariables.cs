using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public WeaponTypeCode weaponTypeCode { get; set; }
    public string weaponName { get; set; }
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int Damage { get; set; }
    public float originalSpeed { get; set; }
    public float AttackSpeed { get; set; }
    public float originalDashSpeed { get; set; }
    public float originalDashPower { get; set; }
    public float originalDashCoolDown { get; set; }

    public PlayerStat()
    {

    }

    public PlayerStat(WeaponTypeCode weaponTypeCode, string weaponName, int maxHp, int damage, float originalSpeed, float AttackSpeed, float originalDashSpeed, float originalDashPower, float originalDashCoolDown)
    {
        this.weaponTypeCode = weaponTypeCode;
        this.weaponName = weaponName;
        this.maxHp = maxHp;
        curHp = maxHp;
        this.Damage = damage;
        this.originalSpeed = originalSpeed;
        this.AttackSpeed = AttackSpeed;
        this.originalDashSpeed = originalDashSpeed;
        this.originalDashPower = originalDashPower;
        this.originalDashCoolDown = originalDashCoolDown;
    }

    public PlayerStat SetUnitStat(WeaponTypeCode weaponTypeCode)
    {
        PlayerStat playerStat = null;

        switch (weaponTypeCode)
        {
            case WeaponTypeCode.Spear: //이름, 최대체력, 공격력, 속도, 공격속도, 대쉬속도, 대쉬파워, 대쉬쿨타임 _순서
                playerStat = new PlayerStat(weaponTypeCode, "Spear", 150, 20, 3f, 4f, 6f, 4f, 2f);
                break;
            case WeaponTypeCode.Double_Dager:
                playerStat = new PlayerStat(weaponTypeCode, "Double_Dager", 100, 5, 4f, 1f, 6f, 5f, 2f);
                break;
            case WeaponTypeCode.Lance:
                playerStat = new PlayerStat(weaponTypeCode, "Lance", 100, 30, 4f, 8f, 5f, 5f, 2f);
                break;
        }
        return playerStat;
    }
}
