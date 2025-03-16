// ¿¹½Ã: SkillSelectionUI.cs
using UnityEngine;

public class SkillSelectionUI : MonoBehaviour
{
    private Character playerCharacter;
    private void Start()
    {
        playerCharacter = CharacterManager.Instance.character.GetComponent<Character>();
    }

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
