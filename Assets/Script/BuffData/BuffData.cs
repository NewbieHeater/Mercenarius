using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Buffs/New Buff")]
public class BuffData : ScriptableObject
{
    [Header("�κ��丮���� ������ �������� �̹���")]
    [SerializeField] private BuffType mBuffType;
    public BuffType buffType
    {
        get
        {
            return mBuffType;
        }
    }
    [Header("������ ���ӽð�")]
    [SerializeField] private float mDuration;
    public float duration
    {
        get
        {
            return mDuration;
        }
    }
    [Header("������ ��")]
    [SerializeField] private float mEffectValue;
    public float effectValue
    {
        get
        {
            return mEffectValue;
        }
    }
    [Header("������ ��ø �����Ѱ�?")]
    [SerializeField] private bool mIsStackable;
    public bool isStackable
    {
        get
        {
            return mIsStackable;
        }
    }
    [Header("����â���� ������ ���� �̹���")]
    [SerializeField] private Sprite mBuffImage;
    public Sprite buffImage
    {
        get
        {
            return mBuffImage;
        }
    }
}
public enum BuffType
{
    HpRegen,
    Slow,
    AttackIncrease,
    Bleed,
    // ���⿡ �߰����� ���� Ÿ�Ե��� �����մϴ�.
}