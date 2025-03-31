using System.Collections.Generic;
using UnityEngine;

/// ���� �� ���� ����� ����ϴ� AudioManager.
/// �̱��� ������ ����Ͽ� ��𼭵� ���� ���� ����.
public class AudioManager : MonoBehaviour
{
    public AudioLibrary audioLibrary; //AudioLibrary ����
    public AudioSource bgmSource;     //BGM AudioSource
    public AudioSource sfxSource;     //SFX ���� ��� AudioSource

    [Range(0f, 1f)] public float volume = 1f;    //���� ���� ���� (0.0 ~ 1.0)

    //�̱��� �ν��Ͻ�
    private static AudioManager instance = null;

    //AudioManager�� �ʱ�ȭ
    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� AudioManager �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�. 
            DontDestroyOnLoad(this.gameObject);

            //AudioLibrary �ʱ�ȭ
            audioLibrary.Init();
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� AudioManager�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� AudioManager)�� �������ش�.
            Destroy(this.gameObject);
        }
    }

    //AudioManager �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
    public static AudioManager Instance
    {
        get { return instance ?? null; }
    }

    //������ �����ϴ� �޼���
    public void SetVolume(string category, float volume)
    {
        if (category == "BGM")
        {
            bgmSource.volume = volume;  //BGM ���� ����
        }
        else
        {
            sfxSource.volume = volume;  //ȿ���� ���� ����
        }
    }

    //��� ���� (��Ȳ�� ���� ��� ����)
    ///<param name="clipName">����� Ŭ�� �̸� (��: "UI_Popup_3", "Player_Attack_Gun_2")</param>
    ///<param name="volume">���� ũ�� ���� (��: 0.1f = ������ 10%�� ����)</param>
    public void PlayBGM(string clipName, float volume = 1.0f)
    {
        AudioClip clip = audioLibrary.GetClip("BGM", clipName); // "BGM" ī�װ����� Ŭ���� ������
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    //���� ���� (�߰��� ��ž. �������)
    public void PauseBgm()
    {
        //���� ��� ���� ��츸 �Ͻ� ����
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    //���� ���� (�̾ ���)
    public void ContinueGame()
    {
        //������ ������ ���� �簳
        if (!bgmSource.isPlaying)
        {
            bgmSource.UnPause();
        }
    }

    //��� �����
    public void RestartBgm()
    {
        //���� BGM ����
        bgmSource.Stop();
        //ó������ �ٽ� ���
        bgmSource.Play();
    }

    //��� ���� (�ƿ� ��)
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    //ȿ����(SFX) �÷���
    ///<param name="category">���� ī�װ� (��: "Player", "Monster")</param>
    ///<param name="clipName">����� Ŭ�� �̸� (��: "UI_Popup_3", "Player_Attack_Gun_2")</param>
    ///<param name="volume">���� ũ�� ���� (��: 0.1f = ������ 10%�� ����)</param>
    public void PlaySFX(string category, string clipName, float volume = 1.0f)
    {
        //����� ���̺귯������ Ŭ�� �˻�
        AudioClip clip = audioLibrary.GetClip(category, clipName);
        if (clip != null)
        {
            //ȿ���� ��� (���ļ� ��� ����)
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
