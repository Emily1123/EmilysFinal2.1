using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public IEnumerator LoadSceneAfterDelay(string sceneName, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        StartCoroutine(LoadSceneAfterDelay("LevelSelect", 2.5f));
    }

    public void LoadBoxingScene()
    {
        SceneManager.LoadScene("Boxing");
    }

    public void LoadCityScene()
    {
        SceneManager.LoadScene("City");
    }

    public void LoadFantasyScene()
    {
        SceneManager.LoadScene("Fantasy");
    }

    public void LoadIndustryScene()
    {
        SceneManager.LoadScene("Industry");
    }

    public void LoadSpaceScene()
    {
        SceneManager.LoadScene("Space");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
