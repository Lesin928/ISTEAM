using UnityEngine; 

// BaseMonster를 상속 → 태양/플레이어 참조, 풀 반환 기능 등 기본 몬스터 기능 포함
public class SpaceShipRangedMonster : BaseMonster
{
    public GameObject projectilePrefab;         // 직선형 총알 프리팹
    public GameObject homingProjectile;         // 추적형 총알 프리팹

    public float attackCooldown = 7f;           // 공격 간격 (초)
    public float attackRange = 5f;              // 공격 사거리
    public float projectileSpeed = 7f;          // 총알 속도 (현재는 사용 안 함)

    // 공격 방식 열거형: 단일/퍼짐/추적
    public enum AttackType { Single, Spread, Homing }
    public AttackType attackType = AttackType.Single; // 기본값: 단일 발사

    private float attackTimer = 0f;             // 공격 타이머 (쿨다운 체크용)
    private float timeAlive = 0f;               // 생존 시간 (시간에 따라 쿨다운 조절)

    private Animator animator;                  // 애니메이션 제어용
    private Rigidbody2D rb;                     // 물리 이동용 리지드바디
    private Monster stat;                   // Monster 컴포넌트 캐싱

    // Start는 오브젝트가 활성화될 때 한 번 실행됨
    protected override void Start()
    {
        stat.moveSpeed = 0.5f;                         // 이동속도 초기화
        base.Start();                           // BaseMonster의 Start 호출 (태양/플레이어 참조 초기화 등)
        animator = GetComponent<Animator>();    // 애니메이터 컴포넌트 참조
        rb = GetComponent<Rigidbody2D>();       // 리지드바디 컴포넌트 참조
        stat = GetComponent<Monster>();             // Monster 컴포넌트 캐싱
    }
    void OnEnable()
    {
        stat = GetComponent<Monster>();         // Monster 컴포넌트 참조

        if (stat != null)
        {
            stat.SetHp(stat.maxHp);                     // 체력 설정
            stat.moveSpeed = 1f;                         // 이동속도 초기화
        }

        stat.moveSpeed = 0.5f;                         // 이동속도 초기화
        attackCooldown = 5f;                    // 쿨다운 초기화
        attackTimer = 0f;                       // 타이머 초기화
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded) return; // 오브젝트 풀링 시 호출되지 않도록 추가 

        // 경험치 업데이트
        UI_GameManager.instance.MonsterKill(20f);
        UI_GameManager.instance.UpdatePlayerEXP();
        stat.SetHp(stat.maxHp);                             // 체력 초기화
        attackTimer = 0f;                            // 공격 타이머 초기화 
        attackCooldown = 5f;                         // 쿨다운 초기화
        stat.moveSpeed = 0.5f;                         // 이동속도 초기화
    }


    // 매 프레임마다 호출되는 Update
    public void Update()
    { 
        if (solar == null || player == null) return; // 타겟이 없으면 행동 중단

        timeAlive += Time.deltaTime;            // 생존 시간 증가 
        attackTimer += Time.deltaTime;          // 공격 타이머 증가

        float distance = Vector2.Distance(transform.position, player.position); // 플레이어와의 거리 계산

        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            Attack();                           // 조건 충족 → 공격
            attackTimer = 0f;                   // 쿨다운 초기화
        }
        else
        {
            MoveTowardsSolar();                 // 사거리 밖이면 태양 방향으로 이동
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
        Vector2 dir = (solar.position - transform.position).normalized; // 태양 방향 벡터 계산

        // Monster 컴포넌트에서 이동 속도 가져와 이동
        Monster stat = GetComponent<Monster>();
        if (stat != null)
        {
            rb.linearVelocity = dir * stat.moveSpeed; // moveSpeed는 Monster → Character에 정의됨
        }

        animator?.SetBool("isMoving", rb.linearVelocity.magnitude > 0.1f); // 움직이는 중인지 애니메이션에 반영

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 방향을 각도로 변환
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 우주선 앞부분이 위쪽이므로 -90도 회전
    }

    // 애니메이션에서 호출되는 공격 트리거 함수
    void Attack()
    {
        animator?.SetTrigger("Attack"); // 애니메이션에서 ShootBullet() 이벤트 발생
    }

    // 애니메이션 이벤트에서 호출되어 총알 발사
    public void ShootBullet()
    {
        if (player == null) return;

        switch (attackType)
        {
            case AttackType.Single: ShootSingle(); break;
            case AttackType.Spread: ShootSpread(5, 45f); break;
            case AttackType.Homing: ShootHoming(); break;
        }
    }

    // 직선형 단일 총알 발사
    void ShootSingle()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 dir = (player.position - transform.position).normalized;
        bullet.GetComponent<MBullet>()?.SetDirection(dir); // 총알에 방향 지정
    }

    // 퍼짐형 총알 여러 개 발사
    void ShootSpread(int count, float spreadAngle)
    {
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + (spreadAngle / (count - 1)) * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * (player.position - transform.position).normalized;

            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<MBullet>()?.SetDirection(dir);
        }
    }

    // 추적형 총알 발사
    void ShootHoming()
    {
        GameObject bullet = Instantiate(homingProjectile, transform.position, Quaternion.identity);
        bullet.GetComponent<HomingBullet>()?.SetTarget(player); // 플레이어를 타겟으로 설정
    }
}
