using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject HUD;
    public GameObject CockPit;
    public GameObject CockPitAnim;

    public GameObject Music;

    void Start()
    {
        WinScreen.SetActive(false);

        if (PlayerPrefs.GetInt("Music")==0)
        {
            Music.SetActive(false);
        }
    }

    public void HasLanded()
    {
        Invoke("GameFinished", 10);
    }

    private void GameFinished()
    {
        HUD.SetActive(false);
        CockPit.SetActive(false);
        CockPitAnim.SetActive(false);
        WinScreen.SetActive(true);
        Invoke("BackToMainMenu", 5f);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
