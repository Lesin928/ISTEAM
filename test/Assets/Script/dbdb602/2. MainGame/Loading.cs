using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour
{
    public Canvas loadingCanvas; // 로딩 화면 캔버스
    public GameObject ancientStartCard; // Ancient Start Card 오브젝트
    private Animator ancientAnimator; // Animator 컴포넌트

    void Start()
    {
        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(true); // 로딩 캔버스 활성화
        }

        if (ancientStartCard != null)
        {
            ancientAnimator = ancientStartCard.GetComponent<Animator>();
            ancientStartCard.SetActive(false); // Ancient Start Card 비활성화
        }

        StartCoroutine(ShowLoadingScreen(3f)); // 3초 후 로딩 종료
    }

    IEnumerator ShowLoadingScreen(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(false); // 로딩 캔버스 비활성화
        }

        if (ancientStartCard != null)
        {
            ancientStartCard.SetActive(true); // Ancient Start Card 활성화

            if (ancientAnimator != null)
            {
                ancientAnimator.Play("애니메이션_이름"); // 애니메이션 실행
            }
        }
    }
}
