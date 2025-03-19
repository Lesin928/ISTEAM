using System.Threading;
using UnityEngine;

public class PBullet : MonoBehaviour
{   //충돌시 몬스터에게 데미지를 가하는 역할
    //충돌시 사라지며 이펙트를 생성하는 역할


    //해당 무기 고유 공격력
    //발사 시점의 플레이어의 공격력
    private float Attack;
    [SerializeField]
    private GameObject effect;

    void Update()
    { 

    }

    //화면밖으로 나갈경우
    private void OnBecameInvisible()
    {
        //자기 자신 지우기
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(Attack);
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);
            //미사일 삭제
            Destroy(gameObject);
        }
    }
}