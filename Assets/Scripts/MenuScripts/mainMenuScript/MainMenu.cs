using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayStory()
    {
        SceneManager.LoadScene(2);
    }

    public void ToTutorial()
    {
        SceneManager.LoadScene(3);
    }

    public void ToOptions()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene(0);

    }

    public void ToCredits()
    {
        SceneManager.LoadScene(9);

    }


}
