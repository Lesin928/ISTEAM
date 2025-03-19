using UnityEngine;

//게임의 핵심 진행을 관리하는 클래스
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmPrefab;

    //싱글톤으로 구현
    private static SoundManager instance = null;
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
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }

    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static SoundManager Instance
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

    //브금 세팅 (상황에 따라 브금 세팅)
    public void InitBgm() 
    {

    }

    //게임 정지 (중간에 스탑. 재생가능)
    public void PauseBgm()
    {

    }

    //게임 진행 (이어서 재생)
    public void ContinueGame()
    {

    }

    //브금 재시작
    public void RestartBgm()
    {

    }

    //브금 중지 (아예 끔)
    public void StopBgm()
    {

    }






}
