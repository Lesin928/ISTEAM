using UnityEngine;
using UnityEngine.UI;

public class OnClickButtonPlaySFX : MonoBehaviour
{
    void Start()
    {
        //현재 버튼에 클릭 이벤트 추가
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX("UI", "UI_Select");
    }
}