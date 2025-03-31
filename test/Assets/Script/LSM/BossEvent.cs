using Unity.Cinemachine;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public Player player;
    public GameObject boss; 
    public GameObject blackhole;
    public GameObject whitehole;
    private CinemachineConfiner2D confiner; // Confiner ����
    private CinemachinePositionComposer composer; // Composer ����

    public BoxCollider2D portal; // �� �ʰ� �������� �մ� �ٿ����
    public BoxCollider2D newBounds; // �� ���� Confiner �ٿ����

    private void Start()
    {
        player = FindAnyObjectByType<Player>(); // Player ã��
        confiner = FindAnyObjectByType<CinemachineConfiner2D>(); // Confiner ã��
        composer = FindAnyObjectByType<CinemachinePositionComposer>(); // Composer ã��
    }
     
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (boss != null)
            {
                AudioManager.Instance.PlaySFX("System", "System_Wormhole_Exit"); 
                player.SetHp(200); //�ִ�ü�� 
                //������ UI Ȱ��ȭ
                confiner.BoundingShape2D = newBounds;
                composer.Damping = Vector3.one;
            }
        }
    }    

    public void SetBoss()
    {
        AudioManager.Instance.PlaySFX("Boss", "Boss_Caution");
        boss.SetActive(true);
        UI_GameManager.instance.OnBossCanvas();
        //���� �̺�Ʈ �߻�
        blackhole.SetActive(true);
        whitehole.SetActive(true);
        confiner.BoundingShape2D = portal;
        composer.Damping = Vector3.zero;

    } 
}
