using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main_UI");
    }
}
