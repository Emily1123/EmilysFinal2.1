using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
