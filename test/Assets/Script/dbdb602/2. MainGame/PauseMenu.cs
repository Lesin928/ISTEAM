using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI; //���� �� ĵ����

    private bool isPaused = false;

    //ESC ������ ���� ���߱�
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC Ű�� ����
        {
            TogglePause();
        }
    }

    //���� �����Ǵ� ��
    void TogglePause()
    {
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;  // ���� �Ͻ� ����
        }
        else
        {
            Time.timeScale = 1f;  // ���� �簳
        }
    }

    //����ϱ�
    public void Continue()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    //�ٽ� �����ϱ�
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

