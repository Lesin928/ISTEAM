using UnityEngine;
using UnityEngine.UI;

public class Start_card : MonoBehaviour
{
    public GameObject Canvas; // 비활성화 or 활성화 할 캔버스

    void Start()
    {
        if(Canvas != null)
        {
            // 시작 시에 캔버스 보이게
            Canvas.SetActive(true);
        }
    }

    // 클릭 시 호출될 메서드
    public void OnCardClick(int index)
    {
        Debug.Log("카드: " + index);
        if (Canvas != null)
        {
            Debug.Log("캔버스 비활성화 시도 중...");
            Canvas.SetActive(false);
            Debug.Log("캔버스 상태: " + Canvas.activeSelf);
        }
        else
        {
            Debug.LogWarning("캔버스가 null입니다.");
        }

    }
}
