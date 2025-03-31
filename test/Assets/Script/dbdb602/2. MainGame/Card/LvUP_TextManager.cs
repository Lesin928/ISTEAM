using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Player �޼��� ���⿡ �߰��߽��ϴ�.
/// 
/// ȭ�� ī�� UI�� ǥ�õǴ� ���ο� ����, ���� ����
/// </summary>
public class LvUP_TextManager : MonoBehaviour
{
    public static LvUP_TextManager Instance { get; private set; }  // �̱��� �ν��Ͻ�
    public Player player;

    //�ؽ�Ʈ ������Ʈ
    public Text statusText;

    //3������ ���� �迭
    private string[] statusTextArray = { "���ݷ�", "���ݼӵ�", "ġ��Ÿ Ȯ��", "ġ��Ÿ ����", "�ִ� ü�� ����", "���� ü�� ����", "����"};

    private int rarity = 0; // �⺻��: �Ϲ�
    private int selectedStatType = -1; //���õ� ���� Ÿ�� ����

    // ��޺� ���� ������ (�Ϲ�, ���, ���, ��ȭ)
    private int[] attackPowerIncrease = { 1, 3, 6, 10 };          //���ݷ�
    private float[] attackSpeedIncrease = { 1f, 3f, 6f, 10f };    //���ݼӵ�
    private float[] criticalHitIncrease = { 1f, 3f, 6f, 10f };    //ġ��Ÿ Ȯ��
    private float[] criticalDamageIncrease = { 1f, 3f, 6f, 10f };    //ġ��Ÿ ����
    private int[] MaxHPIncrease = { 1, 3, 6, 10 };    //�ִ�ü��
    private int[] currentHPIncrease = { 1, 3, 6, 10 };    //����ü��
    private int[] armorIncrease = { 1, 3, 6, 10 };    //����

    //���ο� ī�� ����
    public Text NewAttackPower;     //�߰� �� ���ݷ�
    public Text NewAttackSpeed;     //�߰� �� ���ݼӵ�
    public Text NewCriticalHit;     //ġ��Ÿ Ȯ��
    public Text NewcriticalDamage;  //ġ��Ÿ ����
    public Text NewMaxHP;           //�ִ�ü��
    public Text NewcurrentHP;       //����ü��
    public Text Newarmor;           //����

    // ���õ� ���� ����� ������ ����Ʈ
    private List<int> drawnStats = new List<int>();

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        // �̱��� ����: �̹� �ν��Ͻ��� ������ ���� �������� ����
        if (Instance == null)
        {
            Instance = this;
        }
    }


    //Player ��ũ��Ʈ ����
    //private Player player;


    //Player ����
    //private void Start()
    //{
    //    player = FindObjectOfType<Player>();

    //    if (player == null)
    //    {
    //        Debug.LogError("Player ��ũ��Ʈ�� ã�� �� �����ϴ�!");
    //    }
    //}

    //�ܺο��� rarity �� ���� (���)
    public void SetRarity(int newRarity)
    {
        rarity = newRarity;
        //Debug.Log("set : " + rarity);
        DrawStatus(new List<int>()); // rarity ���� �� ��� DrawStatus ȣ��
    }


    //�������� ���� ��� �� ��޿� �´� ��ġ �ؽ�Ʈ ǥ��
    //Player�� ���ݷ� �� ���� �� ġ��Ÿ ���� ���� �ּ�ó��
    public void DrawStatus(List<int> drawnStats)
    {
        // ���� ���� ��� + ��ġ
        int statType;

        // �ߺ����� �ʴ� ���� ����
        do
        {
            statType = Random.Range(0, 7); // ���ݷ�, ����, ġ��Ÿ �� �ϳ��� ����
        } 
        while (drawnStats.Contains(statType));

        drawnStats.Add(statType); // ���õ� ���� ���
        selectedStatType = statType;  // ���õ� ���� Ÿ���� ����

        // ��ȣ�� ���� �ؽ�Ʈ ���ο� ��ġ �ؽ�Ʈ ǥ��
        switch (statType)
        {
            case 0: // ���ݷ�
                NewAttackPower.text = $"+ {attackPowerIncrease[rarity]}";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 1: // ���ݼӵ�
                NewAttackPower.text = "";
                NewAttackSpeed.text = $"+ {attackSpeedIncrease[rarity]}%";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 2: // ġ��Ÿ Ȯ��
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = $"+ {criticalHitIncrease[rarity]}%";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 3: // ġ��Ÿ ����
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = $"+ {criticalDamageIncrease[rarity]}%";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 4: // �ִ� ü�� ����
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = $"+ {MaxHPIncrease[rarity]}";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 5: // ���� ü�� ����
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = $"+ {currentHPIncrease[rarity]}";
                Newarmor.text = "";
                break;

            case 6: // ����
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = $"+ {armorIncrease[rarity]}";
                break;
        }


        //����Ѵ�� �ؽ�Ʈ�� ����
        statusText.text = statusTextArray[statType];

        // ���õ� ���� ���� �α� ���
        //Debug.Log($"���õ� ���� ����: {statusText.text}");
    }

    // ��ư Ŭ�� �� ���õ� ���� ����
    public void OnStatClicked(int index)
    { 
        Debug.Log("���õ� ����: " + statusTextArray[selectedStatType]);
        WeaponManager.Instance.RegisterWeaponNumber(index);

        // selectedStatType�� ���� Ŭ���� ������ ó��
        switch (selectedStatType)
        {
            case 0:
                // ���ݷ� ���� 
                player.SetAttack(attackPowerIncrease[rarity]);
                break;

            case 1:
                // ���ݼӵ� ���� 
                player.SetAttackSpeed(attackSpeedIncrease[rarity]);
                break;

            case 2:
                // ġ��Ÿ Ȯ�� ���� 
                player.SetCritical(criticalHitIncrease[rarity]);
                break;

            case 3:
                // ġ��Ÿ ���� ���� 
                player.SetCriticalDamage(criticalDamageIncrease[rarity]);
                break;

            case 4:
                // �ִ� ü�� ���� ���� 
                player.SetMaxHp(MaxHPIncrease[rarity]);
                break;

            case 5:
                // ���� ü�� ���� ���� 
                player.HealHp(currentHPIncrease[rarity]);
                break;

            case 6:
                // ���� ���� ���� 
                player.SetArmor(armorIncrease[rarity]);
                break; 
        }
    }

}
