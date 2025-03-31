using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{
    public AudioSource moveSFXSource;   //�÷��̾� �̵� ȿ���� ����� �ҽ�
    public AudioClip moveClip;  //�̵� ���� Ŭ��

    private bool isMoving = false;  //�̵� ���� üũ
    private Vector3 lastPosition;   //������ ��ġ ����

    [Range(0f, 1f)] public float volume = 0.05f;    //���� ���� ���� (0.0 ~ 1.0)

    void Start()
    {
        //����� Ŭ�� ����
        moveSFXSource.clip = moveClip;
        //�̵� ���� �ݺ� ����
        moveSFXSource.loop = true;
        //�ʱ� ���� ����
        moveSFXSource.volume = volume;
        //���� ��ġ ����
        lastPosition = transform.position;
    }

    void Update()
    {
        //�÷��̾ �̵� ������ üũ
        isMoving = transform.position != lastPosition;
        lastPosition = transform.position;

        //�̵� ���̰� ���尡 ������� �ʾҴٸ�
        if (isMoving && !moveSFXSource.isPlaying)
        {
            moveSFXSource.Play();
        }
        //������ �� ȿ������ �ڿ������� ���� ������ ��ٸ�
        else if (!isMoving && moveSFXSource.isPlaying)
        {
            StartCoroutine(StopSoundAfterClipEnds());
        }

        //���� �ǽð� ����
        moveSFXSource.volume = volume;
    }

    //ȿ������ ���� ������ ��ٷȴٰ� ���ߴ� �ڷ�ƾ
    private System.Collections.IEnumerator StopSoundAfterClipEnds()
    {
        float clipLength = moveSFXSource.clip.length;
        yield return new WaitForSeconds(clipLength);    //Ŭ�� ���̸�ŭ ���
        moveSFXSource.Stop(); // ����

        AudioManager.Instance.PlayBGM("");
    }
}