using UnityEngine;

public class Main_BGM : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_Stage_Ancient");
    }
}