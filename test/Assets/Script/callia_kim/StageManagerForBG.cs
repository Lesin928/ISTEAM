using UnityEngine;
using System.Collections;

public class StageManagerForBG : MonoBehaviour
{
    public GameObject[] backgroundPrefabs;  //각 스테이지 배경 프리팹 배열
    public GameObject[] bossPrefabs;    //각 스테이지 보스 프리팹 배열

    private GameObject currentBackground;   //현재 배경 오브젝트
    [HideInInspector] public GameObject currentBoss;    //현재 보스 오브젝트 (다른 스크립트에서 접근 가능)

    private int currentStage = 0;   //현재 스테이지 번호
    private bool isBossDead = false;    //보스의 상태 (죽었는지 여부)

    void Start()
    {
        //첫 스테이지 배경과 보스를 설정
        SetStage(currentStage);
    }

    //보스가 죽었을 때 호출되는 함수
    public void OnBossDeath()
    {
        //중복 호출 방지
        if (!isBossDead) 
        {
            isBossDead = true;

            //보스 죽으면 스테이지 전환 처리
            StartCoroutine(HandleStageTransition()); 
        }
    }

    //스테이지 전환을 처리하는 코루틴
    IEnumerator HandleStageTransition()
    {
        //2초 대기 후
        yield return new WaitForSeconds(2f);

        //현재 보스 오브젝트 삭제
        Destroy(currentBoss);

        //마지막 스테이지가 아니라면 다음 스테이지로 전환
        if (currentStage < backgroundPrefabs.Length - 1)
        {
            //이전 배경 삭제
            Destroy(currentBackground);

            //스테이지 번호 증가
            currentStage++;

            //새 스테이지 설정
            SetStage(currentStage);
        }

        //보스 상태 초기화
        isBossDead = false; 
    }

    //특정 스테이지의 배경과 보스를 설정하는 함수
    void SetStage(int stage)
    {
        //배경과 보스 프리팹이 해당 스테이지에 존재하는지 확인
        if (backgroundPrefabs.Length > stage)// && bossPrefabs.Length > stage)
        {
            //해당 스테이지의 배경과 보스를 생성
            currentBackground = Instantiate(backgroundPrefabs[stage], transform);
            /*

            currentBoss = Instantiate(bossPrefabs[stage], transform);
            //보스 오브젝트에서 BossForBG 컴포넌트를 가져오기
            BossForBG bossScript = currentBoss.GetComponent<BossForBG>();

            if (bossScript != null)
            {
                //보스에게 StageManager를 알려주기 (보스가 죽으면 알려줄 수 있도록)
                bossScript.stageManager = this;
            }

            */
        }
    }
}