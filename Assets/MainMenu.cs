using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject _MainMenu;
    public GameObject _MissionBriefing;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MissionBriefing()
    {
        _MainMenu.SetActive(false);
        _MissionBriefing.SetActive(true);
    }

    public void BackToMain()
    {
        _MainMenu.SetActive(true);
        _MissionBriefing.SetActive(false);
    }

    public void ToggleMusicOn(bool toggle)
    {
        PlayerPrefs.SetInt("Music", 1);
    }


    public void ToggleMusicOff(bool toggle)
    {
        PlayerPrefs.SetInt("Music", 0);
    }
}
