
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // 開始遊戲
    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("SampleScene");
    }

    // 重新開始
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 離開遊戲
    public void QuitGame()
    {
       
        Application.Quit();
    }
}
