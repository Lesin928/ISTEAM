using UnityEngine;
using System.Collections;

public class StageManagerForBG : MonoBehaviour
{
    public GameObject[] backgroundPrefabs;  //�� �������� ��� ������ �迭
    public GameObject[] bossPrefabs;    //�� �������� ���� ������ �迭

    private GameObject currentBackground;   //���� ��� ������Ʈ
    [HideInInspector] public GameObject currentBoss;    //���� ���� ������Ʈ (�ٸ� ��ũ��Ʈ���� ���� ����)

    private int currentStage = 0;   //���� �������� ��ȣ
    private bool isBossDead = false;    //������ ���� (�׾����� ����)

    void Start()
    {
        //ù �������� ���� ������ ����
        SetStage(currentStage);
    }

    //������ �׾��� �� ȣ��Ǵ� �Լ�
    public void OnBossDeath()
    {
        //�ߺ� ȣ�� ����
        if (!isBossDead) 
        {
            isBossDead = true;

            //���� ������ �������� ��ȯ ó��
            StartCoroutine(HandleStageTransition()); 
        }
    }

    //�������� ��ȯ�� ó���ϴ� �ڷ�ƾ
    IEnumerator HandleStageTransition()
    {
        //2�� ��� ��
        yield return new WaitForSeconds(2f);

        //���� ���� ������Ʈ ����
        Destroy(currentBoss);

        //������ ���������� �ƴ϶�� ���� ���������� ��ȯ
        if (currentStage < backgroundPrefabs.Length - 1)
        {
            //���� ��� ����
            Destroy(currentBackground);

            //�������� ��ȣ ����
            currentStage++;

            //�� �������� ����
            SetStage(currentStage);
        }

        //���� ���� �ʱ�ȭ
        isBossDead = false; 
    }

    //Ư�� ���������� ���� ������ �����ϴ� �Լ�
    void SetStage(int stage)
    {
        //���� ���� �������� �ش� ���������� �����ϴ��� Ȯ��
        if (backgroundPrefabs.Length > stage && bossPrefabs.Length > stage)
        {
            //�ش� ���������� ���� ������ ����
            currentBackground = Instantiate(backgroundPrefabs[stage], transform);
            currentBoss = Instantiate(bossPrefabs[stage], transform);

            //���� ������Ʈ���� BossForBG ������Ʈ�� ��������
            BossForBG bossScript = currentBoss.GetComponent<BossForBG>();

            if (bossScript != null)
            {
                //�������� StageManager�� �˷��ֱ� (������ ������ �˷��� �� �ֵ���)
                bossScript.stageManager = this;
            }
        }
    }
}