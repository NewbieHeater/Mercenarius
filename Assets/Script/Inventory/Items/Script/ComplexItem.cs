using UnityEngine;

[CreateAssetMenu(fileName = "SingleEffectItem", menuName = "Add Item/SingleEffectItem")]
public class ComplexItem : Item_Equipment
{
    // Inspector에서 설정할 효과 종류와 파라미터
    public EffectType effectType;
    public float effectValue;
    public float effectDuration;

    private void OnEnable()
    {
        // 인스펙터에서 입력된 값으로 GenericItemEffect를 생성하여 할당
        effects = new ItemEffect[]
        {
            new GenericItemEffect
            {
                effectType = effectType,
                value = effectValue,
                duration = effectDuration
            }
        };
    }
}
