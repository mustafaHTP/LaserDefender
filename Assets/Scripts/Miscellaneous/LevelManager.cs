using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float gameOverScreenDelay;

    public void LoadGame()
    {
        FindObjectOfType<ScoreKeeper>().ResetScore();
        MenuController.Instance.DisableAllMenus();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        MenuController.Instance.SwitchMainMenu();
    }

    public void LoadGameOver()
    {
        MenuController.Instance.DisableAllMenus();
        StartCoroutine(WaitAndLoad("GameOver", gameOverScreenDelay));
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }
}
