using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // 버튼 클릭 시 호출되는 함수
    public void QuitApplication()
    {
        // 애플리케이션 종료
        Application.Quit();
    }
}