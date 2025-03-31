using Unity.Cinemachine;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public Player player;
    public GameObject boss; 
    public GameObject blackhole;
    public GameObject whitehole;
    private CinemachineConfiner2D confiner; // Confiner 참조
    private CinemachinePositionComposer composer; // Composer 참조

    public BoxCollider2D portal; // 새 맵과 기존맵을 잇는 바운더리
    public BoxCollider2D newBounds; // 새 맵의 Confiner 바운더리

    private void Start()
    {
        player = FindAnyObjectByType<Player>(); // Player 찾기
        confiner = FindAnyObjectByType<CinemachineConfiner2D>(); // Confiner 찾기
        composer = FindAnyObjectByType<CinemachinePositionComposer>(); // Composer 찾기
    }
     
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (boss != null)
            {
                AudioManager.Instance.PlaySFX("System", "System_Wormhole_Exit"); 
                player.SetHp(200); //최대체력 
                //보스전 UI 활성화
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
        //보스 이벤트 발생
        blackhole.SetActive(true);
        whitehole.SetActive(true);
        confiner.BoundingShape2D = portal;
        composer.Damping = Vector3.zero;

    } 
}
