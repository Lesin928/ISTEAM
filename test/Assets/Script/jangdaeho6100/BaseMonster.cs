using UnityEngine;

// Monster.cs는 따로 프리팹에 붙이고, BaseMonster는 이동/공격 전용
public class BaseMonster : MonoBehaviour
{
    protected Transform solar; // 태양 위치 (근접 몬스터가 이걸 공격)
    protected Transform player; // 플레이어 위치 (원거리 몬스터가 이걸 공격)

    public int poolIndex; // 오브젝트 풀에서 되돌릴 때 식별용

    protected virtual void Awake()
    {
        // 태양과 플레이어 오브젝트를 태그로 찾음
        solar = GameObject.FindWithTag("Solar")?.transform;
        player = GameObject.FindWithTag("Player")?.transform;
    }

    // 자식 클래스에서 Start 필요 시 오버라이드
    protected virtual void Start() { }
     
    // 피해 처리 Monster 컴포넌트 호출
    public virtual void TakeDamage(float damage)
    {
        // Monster 스크립트 따로 붙어 있으므로 GetComponent로 호출
        Monster stats = GetComponent<Monster>();
        if (stats != null)
        {
            stats.currentHp -= damage;
            if (stats.currentHp <= 0)
            {
                Die(); // 죽으면 풀로 반환
            }
        }
    }

    // 사망 처리 (풀로 반환)
    protected virtual void Die()
    {
        gameObject.SetActive(false); // 비활성화
        TimerManager.Instance.killCount++; // 킬 카운트 증가
        MobObjectPool pool = FindAnyObjectByType<MobObjectPool>();
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
