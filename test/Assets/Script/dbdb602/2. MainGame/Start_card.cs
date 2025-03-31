using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 시대가 시작될 때의 카드 고르는 스크립트
/// </summary>
public class Start_card : MonoBehaviour
{
    public GameObject Canvas; // 비활성화 or 활성화 할 캔버스

    public Image ProfileImage; //메인 UI 상단바 프로필
    public Image BossPlayerImage; //보스때 플레이어 프로필
    public Sprite[] ProfileSprites; //변경할 이미지의 배열

    void Start()
    {/*
        if(Canvas != null)
        {
            // 시작 시에 캔버스 보이게
            Canvas.SetActive(true);
        }*/
    }

    // 클릭 시 호출될 메서드
    public void OnCardClick(int index)
    {
        Debug.Log("카드 인덱스 : " + index);
        // 카드 인덱스에 해당하는 이미지로 변경 => 메인때의 플레이어 프로필
        if (ProfileImage != null && index >= 0 && index < ProfileSprites.Length)
        {
            ProfileImage.sprite = ProfileSprites[index];
        }

        // 카드 인덱스에 해당하는 이미지로 변경 => 보스때의 플레이어 프로필
        if (BossPlayerImage != null && index >= 0 && index < ProfileSprites.Length)
        {
            BossPlayerImage.sprite = ProfileSprites[index];
        }

        if (Canvas != null)
        {
            Canvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("캔버스가 null입니다.");
        }  
    }
}
