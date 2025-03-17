using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

//플레이어의 자동 공격을 구현한 클래스, 각기 다른 공격 방식을 받아온다
public class AutoAttack : MonoBehaviour
{ 
    public GameObject bullet; // 총알
    public Transform pos; // 총알 생성 위치
    public float bulletSpeed = 10f; // 총알 속도   
    public float attackSpeed; //공격 속도                              

    private void Start()
    {
        //플레이어 에게서
        //공격속도 받아오기
        //총알속도 받아오기
    }

    void Update()
    { 
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    { 
        // 마우스 방향 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 환경에서 Z 좌표 고정
        Vector2 direction = (mousePosition - pos.position).normalized; // 방향 벡터로 정규화

        // 총알 생성 및 방향 설정
        GameObject newBullet = Instantiate(bullet, pos.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed; //  일정 속도로 발사
        }

        // 🔹 파워 값에 따른 발사 속도 조절
        float fireDelay = Mathf.Clamp(0.2f - (attackSpeed * 0.02f), 0.05f, 0.2f);

        yield return new WaitForSeconds(fireDelay); 
    }
}
