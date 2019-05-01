using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    public GameSetting Setting;
    public Toggle EasyToggle;
    public Toggle NormalToggle;
    public Toggle HardToggle;
    public bool isthereP2;

    private static SelectMode _instance;

    public static SelectMode Instance { get { return _instance; } }


    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetSinglePlayer()
    {
        isthereP2 = false;
    }

    public void Set2Player()
    {
        isthereP2 = true;
    }

    public void EasySelected()
    {
        Setting.Set_DifficultyLevel(GameSetting.DifficultyLevel.Easy);
    }

    public void NormalSelected()
    {
        Setting.Set_DifficultyLevel(GameSetting.DifficultyLevel.Normal);
    }

    public void HardSelected()
    {
        Setting.Set_DifficultyLevel(GameSetting.DifficultyLevel.Hard);
    }

    void SetOptionsUI()
    {
        GameSetting.DifficultyLevel difficultyLevel = Setting.Get_DifficultyLevel();
        switch (difficultyLevel)
        {
            case GameSetting.DifficultyLevel.Easy:
                EasyToggle.isOn = true;
                break;
            case GameSetting.DifficultyLevel.Normal:
                NormalToggle.isOn = true;
                break;
            case GameSetting.DifficultyLevel.Hard:
                HardToggle.isOn = true;
                break;
            default:
                break;
        }
    }
}
