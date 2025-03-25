using UnityEngine;

//무기 추상 클래스
//모든 무기는 해당 클래스를 상속함
public abstract class Weapon : MonoBehaviour
{
    public GameObject weapon; //던져질 무기 
    public Transform weaponPos;  // 무기 생성 위치
    public float attackSpeed = 1f; // 공격 속도
    public float bulletSpeed = 10f; // 총알 속도  
    //public WeaponObjectPool bulletPool;  // 총알 풀링 시스템 
    public string defaultPrefabPath = "Prifeb/LSM/Weapon/Stone_flying.prefab"; // 기본 프리팹 경로 (Resources 폴더 내)

    protected virtual void Start()
    {
        if (weapon == null)
        {
            GameObject defaultPrefab = Resources.Load<GameObject>(defaultPrefabPath);
            if (defaultPrefab != null)
            {
                weapon = Instantiate(defaultPrefab); 
            } 
        }
    } 

    public abstract void WeaponAttack(); // 각 무기별 공격 방식 정의
    
}