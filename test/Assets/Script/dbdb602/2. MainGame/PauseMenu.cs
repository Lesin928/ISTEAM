using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI; //퍼즈 걸 캔버스

    private bool isPaused = false;

    //ESC 누르면 게임 멈추기
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 키를 감지
        {
            TogglePause();
        }
    }

    //게임 정지되는 것
    void TogglePause()
    {
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;  // 게임 일시 정지
        }
        else
        {
            Time.timeScale = 1f;  // 게임 재개
        }
    }

    //계속하기
    public void Continue()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    //다시 시작하기
    public void Restart(string sceneName)
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main_UI");
    }

    public void QuitGame(string sceneName)
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Start");
    }
}

