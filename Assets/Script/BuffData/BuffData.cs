using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Buffs/New Buff")]
public class BuffData : ScriptableObject
{
    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField] private BuffType mBuffType;
    public BuffType buffType
    {
        get
        {
            return mBuffType;
        }
    }
    [Header("버프의 지속시간")]
    [SerializeField] private float mDuration;
    public float duration
    {
        get
        {
            return mDuration;
        }
    }
    [Header("버프의 값")]
    [SerializeField] private float mEffectValue;
    public float effectValue
    {
        get
        {
            return mEffectValue;
        }
    }
    [Header("버프가 중첩 가능한가?")]
    [SerializeField] private bool mIsStackable;
    public bool isStackable
    {
        get
        {
            return mIsStackable;
        }
    }
    [Header("버프창에서 보여질 버프 이미지")]
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
    // 여기에 추가적인 버프 타입들을 정의합니다.
}