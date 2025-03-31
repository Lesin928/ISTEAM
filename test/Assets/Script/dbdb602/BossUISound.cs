using UnityEngine;

public class BossUISound : MonoBehaviour
{
    public AudioSource VS_audioSource;
    public AudioClip VS_soundClip;

    public AudioSource HP_audioSource;
    public AudioClip HP_soundClip;

    // Animation Event�� ���� ȣ��� �Լ�
    public void VS_PlaySound()
    {
        if (VS_audioSource != null && VS_soundClip != null)
        {
            VS_audioSource.PlayOneShot(VS_soundClip);
        }
        else
        {
            Debug.LogWarning("AudioSource �Ǵ� SoundClip�� �������� �ʾҽ��ϴ�.");
        }
    }


    // Animation Event�� ���� ȣ��� �Լ�
    public void HP_PlaySound()
    {
        if (HP_audioSource != null && HP_soundClip != null)
        {
            HP_audioSource.PlayOneShot(HP_soundClip);
        }
        else
        {
            Debug.LogWarning("AudioSource �Ǵ� SoundClip�� �������� �ʾҽ��ϴ�.");
        }
    }
}
