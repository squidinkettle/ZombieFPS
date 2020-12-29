using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void RetryButton()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
