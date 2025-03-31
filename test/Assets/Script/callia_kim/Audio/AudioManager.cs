using System.Collections.Generic;
using UnityEngine;

/// 게임 내 사운드 재생을 담당하는 AudioManager.
/// 싱글톤 패턴을 사용하여 어디서든 쉽게 접근 가능.
public class AudioManager : MonoBehaviour
{
    public AudioLibrary audioLibrary; //AudioLibrary 참조
    public AudioSource bgmSource;     //BGM AudioSource
    public AudioSource sfxSource;     //SFX 단일 재생 AudioSource

    [Range(0f, 1f)] public float volume = 1f;    //볼륨 조절 변수 (0.0 ~ 1.0)

    //싱글톤 인스턴스
    private static AudioManager instance = null;

    //AudioManager의 초기화
    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 AudioManager 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다. 
            DontDestroyOnLoad(this.gameObject);

            //AudioLibrary 초기화
            audioLibrary.Init();
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 AudioManager이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 AudioManager)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }

    //AudioManager 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static AudioManager Instance
    {
        get { return instance ?? null; }
    }

    //볼륨을 조절하는 메서드
    public void SetVolume(string category, float volume)
    {
        if (category == "BGM")
        {
            bgmSource.volume = volume;  //BGM 볼륨 변경
        }
        else
        {
            sfxSource.volume = volume;  //효과음 볼륨 변경
        }
    }

    //브금 세팅 (상황에 따라 브금 세팅)
    ///<param name="clipName">재생할 클립 이름 (예: "UI_Popup_3", "Player_Attack_Gun_2")</param>
    ///<param name="volume">음량 크기 조절 (예: 0.1f = 볼륨을 10%로 조절)</param>
    public void PlayBGM(string clipName, float volume = 1.0f)
    {
        AudioClip clip = audioLibrary.GetClip("BGM", clipName); // "BGM" 카테고리에서 클립을 가져옴
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    //게임 정지 (중간에 스탑. 재생가능)
    public void PauseBgm()
    {
        //현재 재생 중인 경우만 일시 정지
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    //게임 진행 (이어서 재생)
    public void ContinueGame()
    {
        //정지된 상태일 때만 재개
        if (!bgmSource.isPlaying)
        {
            bgmSource.UnPause();
        }
    }

    //브금 재시작
    public void RestartBgm()
    {
        //현재 BGM 정지
        bgmSource.Stop();
        //처음부터 다시 재생
        bgmSource.Play();
    }

    //브금 중지 (아예 끔)
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    //효과음(SFX) 플레이
    ///<param name="category">사운드 카테고리 (예: "Player", "Monster")</param>
    ///<param name="clipName">재생할 클립 이름 (예: "UI_Popup_3", "Player_Attack_Gun_2")</param>
    ///<param name="volume">음량 크기 조절 (예: 0.1f = 볼륨을 10%로 조절)</param>
    public void PlaySFX(string category, string clipName, float volume = 1.0f)
    {
        //오디오 라이브러리에서 클립 검색
        AudioClip clip = audioLibrary.GetClip(category, clipName);
        if (clip != null)
        {
            //효과음 재생 (겹쳐서 재생 가능)
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
