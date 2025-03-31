using UnityEngine; 

// BaseMonster를 상속 → 태양/플레이어 참조, 풀 시스템, 체력 시스템 포함
public class RangedMonster : BaseMonster
{
    public GameObject projectilePrefab;       // 직선형 총알 프리팹 (MBullet)
    public GameObject homingProjectile;       // 추적형 총알 프리팹 (HomingBullet)

    public float attackCooldown = 7f;         // 공격 쿨다운 시간
    public float attackRange = 5f;            // 플레이어를 공격할 최대 사거리
    public float projectileSpeed = 7f;        // 총알 속도 (현재 사용 안 함)

    // 공격 유형을 나타내는 열거형
    public enum AttackType { Single, Spread, Homing }
    public AttackType attackType = AttackType.Single; // 기본 공격 타입: 단일 발사

    private float attackTimer = 0f;           // 공격 간격을 계산하기 위한 타이머
    private float timeAlive = 0f;             // 몬스터가 살아있는 시간 (공격 속도 증가용)

    private Animator animator;                // 애니메이션 제어용
    private Rigidbody2D rb;                   // Rigidbody2D → 이동 제어용
    private Monster stat;                   // Monster 컴포넌트 캐싱
    // 게임 시작 시 호출 (컴포넌트 초기화)
    protected override void Start()
    {
        base.Start();                         // BaseMonster의 Start → 태양, 플레이어 참조 초기화
        animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 캐싱
        rb = GetComponent<Rigidbody2D>();     // 리지드바디 컴포넌트 캐싱
        stat = GetComponent<Monster>();             // Monster 컴포넌트 캐싱
        stat.moveSpeed = 0.5f;                         // 이동속도 초기화 
    }
    void OnEnable()
    {
        Monster stat = GetComponent<Monster>();            // Monster 컴포넌트 가져오기
        if (stat != null)
        {
            stat.SetHp(80);                                // 체력 초기화  
        } 
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded) return; // 오브젝트 풀링 시 호출되지 않도록 추가

        // 경험치 업데이트
        UI_GameManager.instance.MonsterKill(15f);
        UI_GameManager.instance.UpdatePlayerEXP();
        stat.SetHp(stat.maxHp);                             // 체력 초기화
        attackTimer = 0f;                            // 공격 타이머 초기화
        stat.moveSpeed = 0.5f;                         // 이동속도 초기화 
        attackCooldown = 5f;                         // 쿨다운 초기화
    }

    // 매 프레임 호출되는 메인 업데이트
    public void Update()
    {
        if (solar == null || player == null) return; // 타겟 없으면 아무것도 안 함

        timeAlive += Time.deltaTime; // 생존 시간 증가 
        attackTimer += Time.deltaTime; // 공격 타이머 증가

        float distance = Vector2.Distance(transform.position, player.position); // 플레이어와 거리 계산

        // 공격 사거리 안이고 쿨다운도 끝났다면 공격
        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            Attack();              // 공격 실행
            attackTimer = 0f;      // 쿨다운 초기화
        }
        else
        {
            MoveTowardsSolar();    // 조건 안 맞으면 태양 방향으로 이동
        }
        if (stat.currentHp <= 0)
        {
            Die();                                  // 체력이 0 이하면 사망 처리
            AudioManager.Instance.PlaySFX("Mob", "Mob_Death");
        }
    }

    // 태양을 향해 이동
    void MoveTowardsSolar()
    {
        Vector2 dir = (solar.position - transform.position).normalized; // 방향 벡터 계산

        // Monster.cs에 정의된 moveSpeed 사용
        Monster stat = GetComponent<Monster>(); // Monster 컴포넌트 참조
        if (stat != null)
        {
            rb.linearVelocity = dir * stat.moveSpeed; // 방향 * 속도 → 이동
        }

        animator.SetBool("isMoving", rb.linearVelocity.magnitude > 0.1f); // 움직이면 애니메이션 실행
        GetComponent<SpriteRenderer>().flipX = dir.x < 0; // 왼쪽이면 좌우 반전
    }

    // 공격 시작: 애니메이션 트리거 실행
    void Attack()
    {
        animator.SetTrigger("Attack"); // 공격 애니메이션 트리거 → 애니메이션 이벤트로 ShootBullet 호출됨
    }

    // 총알 발사 함수 (애니메이션 이벤트에서 호출됨)
    public void ShootBullet()
    {
        if (player == null) return;

        switch (attackType)
        {
            case AttackType.Single: ShootSingle(); break;           // 단일 총알
            case AttackType.Spread: ShootSpread(3, 45f); break;     // 퍼짐형 총알 5개, 45도 범위
            case AttackType.Homing: ShootHoming(); break;           // 추적형 총알
        }
    }

    // 단일 총알 발사
    void ShootSingle()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 총알 생성
        Vector2 dir = (player.position - transform.position).normalized; // 방향 계산
        bullet.GetComponent<MBullet>()?.SetDirection(dir); // 방향 설정
    }

    // 퍼지는 총알 여러 개 발사
    void ShootSpread(int count, float spreadAngle)
    {
        float startAngle = -spreadAngle / 2f; // 첫 번째 총알의 기준 각도

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + (spreadAngle / (count - 1)) * i; // 각도 계산
            Vector2 dir = Quaternion.Euler(0, 0, angle) * (player.position - transform.position).normalized;

            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 총알 생성
            bullet.GetComponent<MBullet>()?.SetDirection(dir); // 방향 설정
        }
    }

    // 추적형 총알 발사
    void ShootHoming()
    {
        GameObject bullet = Instantiate(homingProjectile, transform.position, Quaternion.identity); // 총알 생성
        bullet.GetComponent<HomingBullet>()?.SetTarget(player); // 타겟 설정
    }
}
