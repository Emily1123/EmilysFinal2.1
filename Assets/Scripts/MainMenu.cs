using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameSetting Setting;
    public Toggle EasyToggle;
    public Toggle NormalToggle;
    public Toggle HardToggle;

    public IEnumerator LoadSceneAfterDelay(string sceneName, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }

    public void NoMusic()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("music");
        Destroy(obj);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Mode");
    }

    public void LoadDifficulty()
    {
        SceneManager.LoadScene("Difficulty");
    }

    public void LoadSinglePlayerControls()
    {
        SceneManager.LoadScene("SingleControls");
    }

    public void Load2PlayerControls()
    {
        SceneManager.LoadScene("2PlayerControls");
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

    public void LoadBoxingScene()
    {
        StartCoroutine(LoadSceneAfterDelay("Boxing", 2.5f));
    }

    public void LoadCityScene()
    {
        StartCoroutine(LoadSceneAfterDelay("City", 2.5f));
    }

    public void LoadFantasyScene()
    {
        StartCoroutine(LoadSceneAfterDelay("Fantasy", 2.5f));
    }

    public void LoadIndustryScene()
    {
        StartCoroutine(LoadSceneAfterDelay("Industry", 2.5f));
    }

    public void LoadSpaceScene()
    {
        StartCoroutine(LoadSceneAfterDelay("Space", 2.5f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
