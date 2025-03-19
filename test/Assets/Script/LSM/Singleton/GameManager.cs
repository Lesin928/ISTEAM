using UnityEngine;

//게임의 핵심 진행을 관리하는 클래스

public class GameManager : MonoBehaviour
{
    //싱글톤으로 구현
    private static GameManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다. 
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameManager이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameManager)을 삭제해준다.
            Destroy(this.gameObject);
        }

    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. 
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    //게임 초기 세팅
    public void InitGame() 
    {

    }

    //게임 정지
    public void PauseGame()
    {

    }

    //게임 진행
    public void ContinueGame()
    {

    }

    //게임 재시작
    public void RestartGame()
    {

    }

    //게임 중지
    public void StopGame()
    {

    }






}
