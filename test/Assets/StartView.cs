using UnityEngine;

public class StartView : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_Title");
    }
    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX("UI", "UI_Select");
    }
}