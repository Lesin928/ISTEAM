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
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ������
            SetResolution();
        }
        else
        {
            Destroy(gameObject); // �ߺ� ���� ����
        }
    }

    private void SetResolution()
    {
        Screen.SetResolution(1920, 1080, true);
    }
}
 