using UnityEngine;
using UnityEngine.SceneManagement;

public class End_SceneChange : MonoBehaviour
{
    public void OnClickStart()
    { 
        SceneManager.LoadScene("Start");
    }
}

