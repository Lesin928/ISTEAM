using System.Collections.Generic;
using UnityEngine;

//싱글톤으로 구현, 무기 생성 및 강화, 파괴 담당
public class WeaponManager : MonoBehaviour
{    
    public static WeaponManager Instance { get; private set; }
    GameObject player;
    AutoAttack autoAttack;

    [SerializeField] private List<GameObject> weaponPrefabs; // 무기 프리팹 목록
    private Dictionary<string, GameObject> activeWeapons = new Dictionary<string, GameObject>(); // 현재 장착된 무기
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    } 
    

    private void Start()
    {
        // 플레이어 태그를 가진 오브젝트가 존재하는지 확인
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
            return;
        }
        /*
        // 인스펙터에 등록된 무기 프리팹들을 모두 장착, 테스트용임
        foreach (GameObject prefab in weaponPrefabs)
        {
            RegisterWeapon(prefab);
        }*/

        autoAttack = player.GetComponent<AutoAttack>();
    }
     
    


    // 새로운 무기를 등록하고 장착하는 함수
    public void RegisterWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null)
        {
            Debug.LogWarning("유효하지 않은 무기입니다.");
            return;
        }
        else if (activeWeapons.ContainsKey(weaponPrefab.name))
        {
            Debug.Log("이미 등록된 무기입니다.");
            // 이미 등록된 무기인 경우 레벨업
            return;
        }
        // 무기 생성 및 플레이어의 자식으로 설정
        GameObject newWeapon = Instantiate(weaponPrefab, player.transform);
        activeWeapons.Add(weaponPrefab.name, newWeapon);

        // 무기 클래스의 CreateWeapon() 실행
        Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
        if (weaponComponent != null)
        { 
            weaponComponent.CreatWeapon();
            autoAttack.AddWeapon(weaponComponent);
        }
        else
        {
            Debug.LogWarning($"Weapon 컴포넌트가 {weaponPrefab.name}에 없습니다.");
        }
    }

    public void RegisterWeaponNumber(int index)
    {
        RegisterWeapon(weaponPrefabs[index]);
    }

    // 무기를 제거하는 함수
    public void RemoveWeapon(string weaponName)
    {
        if (activeWeapons.ContainsKey(weaponName))
        {
            Destroy(activeWeapons[weaponName]); // 무기 오브젝트 삭제
            activeWeapons.Remove(weaponName); // 목록에서 제거
        }
        else
        {
            Debug.LogWarning($"{weaponName} 무기가 현재 장착되지 않았습니다.");
        }
    }






}

