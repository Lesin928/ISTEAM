using UnityEngine;

//������ �ٽ� ������ �����ϴ� Ŭ����
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmPrefab;

    //�̱������� ����
    private static SoundManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�. 
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            Destroy(this.gameObject);
        }

    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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

    //��� ���� (��Ȳ�� ���� ��� ����)
    public void InitBgm() 
    {

    }

    //���� ���� (�߰��� ��ž. �������)
    public void PauseBgm()
    {

    }

    //���� ���� (�̾ ���)
    public void ContinueGame()
    {

    }

    //��� �����
    public void RestartBgm()
    {

    }

    //��� ���� (�ƿ� ��)
    public void StopBgm()
    {

    }






}
