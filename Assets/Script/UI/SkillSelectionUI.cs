// ¿¹½Ã: SkillSelectionUI.cs
using UnityEngine;

public class SkillSelectionUI : MonoBehaviour
{
    public Character playerCharacter;

    public void OnSelectFireball()
    {
        playerCharacter.SetSharedSkill(new DaggerThrow());
    }

    public void OnSelectIceBlast()
    {
        //playerCharacter.SelectSharedSkill("IceBlast");
    }

    public void OnSelectLightningStrike()
    {
        //playerCharacter.SelectSharedSkill("LightningStrike");
    }
}
