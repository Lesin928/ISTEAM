using UnityEngine;
using UnityEngine.UI;

public class OnClickButtonPlaySFX : MonoBehaviour
{
    void Start()
    {
        //���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX("UI", "UI_Select");
    }
}