using System.Collections.Generic;
using UnityEngine;

//�̱������� ����, ���� ���� �� ��ȭ, �ı� ���
public class WeaponManager : MonoBehaviour
{    
    public static WeaponManager Instance { get; private set; }
    GameObject player;
    AutoAttack autoAttack;

    [SerializeField] private List<GameObject> weaponPrefabs; // ���� ������ ���
    private Dictionary<string, GameObject> activeWeapons = new Dictionary<string, GameObject>(); // ���� ������ ����
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
        // �÷��̾� �±׸� ���� ������Ʈ�� �����ϴ��� Ȯ��
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
            return;
        }
        /*
        // �ν����Ϳ� ��ϵ� ���� �����յ��� ��� ����, �׽�Ʈ����
        foreach (GameObject prefab in weaponPrefabs)
        {
            RegisterWeapon(prefab);
        }*/

        autoAttack = player.GetComponent<AutoAttack>();
    }
     
    


    // ���ο� ���⸦ ����ϰ� �����ϴ� �Լ�
    public void RegisterWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null)
        {
            Debug.LogWarning("��ȿ���� ���� �����Դϴ�.");
            return;
        }
        else if (activeWeapons.ContainsKey(weaponPrefab.name))
        {
            Debug.Log("�̹� ��ϵ� �����Դϴ�.");
            // �̹� ��ϵ� ������ ��� ������
            return;
        }
        // ���� ���� �� �÷��̾��� �ڽ����� ����
        GameObject newWeapon = Instantiate(weaponPrefab, player.transform);
        activeWeapons.Add(weaponPrefab.name, newWeapon);

        // ���� Ŭ������ CreateWeapon() ����
        Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
        if (weaponComponent != null)
        { 
            weaponComponent.CreatWeapon();
            autoAttack.AddWeapon(weaponComponent);
        }
        else
        {
            Debug.LogWarning($"Weapon ������Ʈ�� {weaponPrefab.name}�� �����ϴ�.");
        }
    }

    public void RegisterWeaponNumber(int index)
    {
        RegisterWeapon(weaponPrefabs[index]);
    }

    // ���⸦ �����ϴ� �Լ�
    public void RemoveWeapon(string weaponName)
    {
        if (activeWeapons.ContainsKey(weaponName))
        {
            Destroy(activeWeapons[weaponName]); // ���� ������Ʈ ����
            activeWeapons.Remove(weaponName); // ��Ͽ��� ����
        }
        else
        {
            Debug.LogWarning($"{weaponName} ���Ⱑ ���� �������� �ʾҽ��ϴ�.");
        }
    }






}

