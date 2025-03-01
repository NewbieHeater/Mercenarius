using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_System : MonoBehaviour
{
    //private static bool msIsOpenMenu = false;

    //[SerializeField] private GameObject mMenuParent;


    //[Header("소리 설정 오브젝트")]
    //[SerializeField] private Slider mMasterVolumeSlider;
    //[SerializeField] private Slider mMusicVolumeSlider;
    //[SerializeField] private Slider mSFXVolumeSlider;


    //[Header("품질 설정 오브젝트")]
    //[SerializeField] private TMPro.TMP_Dropdown mQualityDropdown;


    //public void TryOpenMenu()
    //{
    //    if (msIsOpenMenu)
    //    {
    //        CloseMenu();
    //    }
    //    else
    //    {
    //        OpenMenu();
    //    }
    //    msIsOpenMenu = !msIsOpenMenu;
    //}

    //public void InitMenuLayouts()
    //{
    //    mBGMSoundSlider.SetValueWithoutNotify(OptionDataManager.Instance.OptionData.bgmVolume);
    //    mEffectSoundSlider.SetValueWithoutNotify(OptionDataManager.Instance.OptionData.effectVolume);
    //    mQualityDropdown.SetValueWithoutNotify(OptionDataManager.Instance.OptionData.currentSelectQuilityID);

    //    switch (OptionDataManager.Instance.OptionData.language)
    //    {
    //        case SystemLanguage.Korean: 
    //            {
    //                mLangDropdown.SetValueWithoutNotify(0);
    //                break;
    //            }

    //        case SystemLanguage.English:    
    //        default:
    //            {
    //                mLangDropdown.SetValueWithoutNotify(1);
    //                break;
    //            }
    //    }

    //    if (OptionDataManager.Instance.OptionData.isFPS)
    //    {
    //        PressFPSButton();
    //    }
    //    else
    //    {
    //        PressTPSButton();
    //    }

    //    mMenuParent.SetActive(false);
    //}

    //public void OpenMenu()
    //{
    //    mMenuParent.SetActive(true);

    //    //UtilityManager.UnlockCursor();
    //    //PlayerController.Lock();
    //    //Camera_FPS_TPS.Lock();
    //}

    //public void CloseMenu()
    //{
    //    mMenuParent.SetActive(false);

    //     //UtilityManager.LockCursor();
    //     //PlayerController.Unlock();
    //     //Camera_FPS_TPS.Unlock();
    //}

   

    //public void BlockPOV()
    //{
    //    mPOVBlocker.SetActive(true);
    //}

    //public void ReleasePOV()
    //{
    //    mPOVBlocker.SetActive(false);
    //}

    //public void SelectQualityDropdown()
    //{
    //    QualitySettings.SetQualityLevel(mQualityDropdown.value, true);
    //    OptionDataManager.Instance.OptionData.currentSelectQuilityID = mQualityDropdown.value;
    //    OptionDataManager.Instance.SaveOptionData();
    //}

    //public void BGMValueChanged()
    //{
    //    UtilityManager.mSound.SetBGMVolume(mBGMSoundSlider.value);
    //    OptionDataManager.Instance.OptionData.bgmVolume = mBGMSoundSlider.value;
    //    OptionDataManager.Instance.SaveOptionData();
    //}

    //public void EffectValueChanged()
    //{
    //    UtilityManager.mSound.SetEffectVolume(mEffectSoundSlider.value);
    //    OptionDataManager.Instance.OptionData.effectVolume = mEffectSoundSlider.value;
    //    OptionDataManager.Instance.SaveOptionData();
    //}

    //public void PressReloadButton()
    //{

    //}

    //public static bool GetIsMenuOpen()
    //{
    //    return msIsOpenMenu;
    //}
}
