using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{
    public AudioSource moveSFXSource;   //플레이어 이동 효과음 오디오 소스
    public AudioClip moveClip;  //이동 사운드 클립

    private bool isMoving = false;  //이동 상태 체크
    private Vector3 lastPosition;   //마지막 위치 저장

    [Range(0f, 1f)] public float volume = 0.05f;    //볼륨 조절 변수 (0.0 ~ 1.0)

    void Start()
    {
        //오디오 클립 설정
        moveSFXSource.clip = moveClip;
        //이동 사운드 반복 설정
        moveSFXSource.loop = true;
        //초기 볼륨 적용
        moveSFXSource.volume = volume;
        //시작 위치 설정
        lastPosition = transform.position;
    }

    void Update()
    {
        //플레이어가 이동 중인지 체크
        isMoving = transform.position != lastPosition;
        lastPosition = transform.position;

        //이동 중이고 사운드가 재생되지 않았다면
        if (isMoving && !moveSFXSource.isPlaying)
        {
            moveSFXSource.Play();
        }
        //멈췄을 때 효과음이 자연스럽게 끝날 때까지 기다림
        else if (!isMoving && moveSFXSource.isPlaying)
        {
            StartCoroutine(StopSoundAfterClipEnds());
        }

        //볼륨 실시간 적용
        moveSFXSource.volume = volume;
    }

    //효과음이 끝날 때까지 기다렸다가 멈추는 코루틴
    private System.Collections.IEnumerator StopSoundAfterClipEnds()
    {
        float clipLength = moveSFXSource.clip.length;
        yield return new WaitForSeconds(clipLength);    //클립 길이만큼 대기
        moveSFXSource.Stop(); // 정지

        AudioManager.Instance.PlayBGM("");
    }
}