using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour  
{
    private static StartScene instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
            SetResolution();
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    private void SetResolution()
    {
        Screen.SetResolution(1920, 1080, true);
    }
}
 