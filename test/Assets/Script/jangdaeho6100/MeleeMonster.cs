using UnityEngine;

// BaseMonster를 상속받음 → 체력, 이동속도, 능력치는 모두 Monster.cs에서 관리됨
public class MeleeMonster : BaseMonster
{
    [Header("근접 몬스터 전용 속성")]
    public float attackRange = 1.5f;        // 태양 공격 사거리
    public float attackCooldown = 4f;       // 공격 간격
    public float damage = 10f;              // 태양에 줄 피해량

    private float attackTimer = 0f;         // 공격 쿨다운 계산용 타이머
    private bool isAttacking = false;       // 공격 중인지 여부

    private Animator animator;              // 애니메이션 제어
    private Rigidbody2D rb;                 // 물리 기반 이동 제어
    private Monster stat;                   // Monster 컴포넌트 캐싱 
    // 초기화 (애니메이션, 리지드바디 설정)
    protected override void Start()
    {
        stat.moveSpeed = 0.5f;                         // 이동속도 초기화
        base.Start();                               // BaseMonster의 Start() 호출 (기본 초기화)
        animator = GetComponent<Animator>();        // 애니메이터 가져오기
        rb = GetComponent<Rigidbody2D>();           // 리지드바디 가져오기
        stat = GetComponent<Monster>();             // Monster 컴포넌트 캐싱 
    }
    void OnEnable()
    {
        if (stat == null)                            // stat이 없으면
            stat = GetComponent<Monster>();          // Monster 컴포넌트 가져오기

        stat.SetHp(stat.maxHp);                             // 체력 초기화
        attackTimer = 0f;                            // 공격 타이머 초기화
        stat.moveSpeed = 0.5f;                         // 이동속도 초기화
        damage = 10f;                                // 피해량 초기화
        attackCooldown = 4f;                         // 쿨다운 초기화
    }
    // 매 프레임 실행
    public void Update()
    {
        if (solar == null || stat == null) return;  // 태양 또는 스탯이 없으면 아무것도 하지 않음

        attackTimer += Time.deltaTime;              // 쿨다운 타이머 증가

        float distance = Vector2.Distance(transform.position, solar.position); // 태양까지 거리 계산

        // 사정거리 이내 && 쿨다운 완료 && 공격 중이 아닐 때
        if (distance <= attackRange && attackTimer >= attackCooldown && !isAttacking)
        {
            Attack();                               // 공격 시작
            attackTimer = 0f;                       // 타이머 초기화
        }
        else if (!isAttacking)
        {
            MoveTowardsSolar();                     // 공격 못 하면 이동
        }

        if(stat.currentHp <= 0)
        {
            Die();                                  // 체력이 0 이하면 사망 처리
            AudioManager.Instance.PlaySFX("Mob", "Mob_Death");
        }
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded) return; // 오브젝트 풀링 시 호출되지 않도록 추가 
        // 경험치 업데이트
        UI_GameManager.instance.MonsterKill(10f);
        UI_GameManager.instance.UpdatePlayerEXP();
    }

    // 태양 방향으로 이동
    void MoveTowardsSolar()
    {
        Vector2 dir = (solar.position - transform.position).normalized; // 방향 계산

        rb.linearVelocity = dir * stat.moveSpeed;                       // Monster.cs의 moveSpeed 사용

        animator?.SetBool("isMoving", rb.linearVelocity.magnitude > 0.1f); // 움직임 여부 애니메이션에 반영
        GetComponent<SpriteRenderer>().flipX = dir.x < 0;                  // 좌우 반전
    }

    // 공격 시작
    void Attack()
    {
        isAttacking = true;                     // 상태 전환
        animator?.SetTrigger("Attack");         // 애니메이션 트리거

        Invoke(nameof(DamageSolar), 0.3f);      // 딜레이 후 데미지 주기
        Invoke(nameof(ResetAttack), 0.5f);      // 딜레이 후 상태 초기화
    }

    // 태양에게 데미지를 입힘
    void DamageSolar()
    {
        solar?.GetComponent<Solar>()?.TakeDamage(damage); // 태양에 데미지 전달
    }

    // 공격 상태 초기화
    void ResetAttack()
    {
        isAttacking = false;                    // 다시 이동/공격 가능 상태로 전환
    }
}
