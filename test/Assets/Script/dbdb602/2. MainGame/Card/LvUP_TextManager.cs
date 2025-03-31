using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Player 메서드 여기에 추가했습니다.
/// 
/// 화면 카드 UI에 표시되는 새로운 스텟, 스텟 종류
/// </summary>
public class LvUP_TextManager : MonoBehaviour
{
    public static LvUP_TextManager Instance { get; private set; }  // 싱글톤 인스턴스
    public Player player;

    //텍스트 오브젝트
    public Text statusText;

    //3가지의 스텟 배열
    private string[] statusTextArray = { "공격력", "공격속도", "치명타 확률", "치명타 피해", "최대 체력 증가", "현재 체력 증가", "방어력"};

    private int rarity = 0; // 기본값: 일반
    private int selectedStatType = -1; //선택된 스텟 타입 저장

    // 등급별 스탯 증가량 (일반, 고급, 희귀, 신화)
    private int[] attackPowerIncrease = { 1, 3, 6, 10 };          //공격력
    private float[] attackSpeedIncrease = { 1f, 3f, 6f, 10f };    //공격속도
    private float[] criticalHitIncrease = { 1f, 3f, 6f, 10f };    //치명타 확률
    private float[] criticalDamageIncrease = { 1f, 3f, 6f, 10f };    //치명타 피해
    private int[] MaxHPIncrease = { 1, 3, 6, 10 };    //최대체력
    private int[] currentHPIncrease = { 1, 3, 6, 10 };    //현재체력
    private int[] armorIncrease = { 1, 3, 6, 10 };    //방어력

    //새로운 카드 스텟
    public Text NewAttackPower;     //추가 될 공격력
    public Text NewAttackSpeed;     //추가 될 공격속도
    public Text NewCriticalHit;     //치명타 확률
    public Text NewcriticalDamage;  //치명타 피해
    public Text NewMaxHP;           //최대체력
    public Text NewcurrentHP;       //현재체력
    public Text Newarmor;           //방어력

    // 선택된 스탯 기록을 저장할 리스트
    private List<int> drawnStats = new List<int>();

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        // 싱글톤 패턴: 이미 인스턴스가 있으면 새로 생성하지 않음
        if (Instance == null)
        {
            Instance = this;
        }
    }


    //Player 스크립트 참조
    //private Player player;


    //Player 연결
    //private void Start()
    //{
    //    player = FindObjectOfType<Player>();

    //    if (player == null)
    //    {
    //        Debug.LogError("Player 스크립트를 찾을 수 없습니다!");
    //    }
    //}

    //외부에서 rarity 값 설정 (등급)
    public void SetRarity(int newRarity)
    {
        rarity = newRarity;
        //Debug.Log("set : " + rarity);
        DrawStatus(new List<int>()); // rarity 설정 후 즉시 DrawStatus 호출
    }


    //랜덤으로 스탯 출력 및 등급에 맞는 수치 텍스트 표시
    //Player의 공격력 및 공속 및 치명타 증가 적용 주석처리
    public void DrawStatus(List<int> drawnStats)
    {
        // 랜덤 스탯 출력 + 수치
        int statType;

        // 중복되지 않는 스탯 선택
        do
        {
            statType = Random.Range(0, 7); // 공격력, 공속, 치명타 중 하나를 선택
        } 
        while (drawnStats.Contains(statType));

        drawnStats.Add(statType); // 선택된 스탯 기록
        selectedStatType = statType;  // 선택된 스탯 타입을 저장

        // 번호에 따라 텍스트 새로운 수치 텍스트 표시
        switch (statType)
        {
            case 0: // 공격력
                NewAttackPower.text = $"+ {attackPowerIncrease[rarity]}";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 1: // 공격속도
                NewAttackPower.text = "";
                NewAttackSpeed.text = $"+ {attackSpeedIncrease[rarity]}%";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 2: // 치명타 확률
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = $"+ {criticalHitIncrease[rarity]}%";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 3: // 치명타 피해
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = $"+ {criticalDamageIncrease[rarity]}%";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 4: // 최대 체력 증가
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = $"+ {MaxHPIncrease[rarity]}";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = "";
                break;

            case 5: // 현재 체력 증가
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = $"+ {currentHPIncrease[rarity]}";
                Newarmor.text = "";
                break;

            case 6: // 방어력
                NewAttackPower.text = "";
                NewAttackSpeed.text = "";
                NewCriticalHit.text = "";
                NewcriticalDamage.text = "";
                NewMaxHP.text = "";
                NewcurrentHP.text = "";
                Newarmor.text = $"+ {armorIncrease[rarity]}";
                break;
        }


        //출력한대로 텍스트에 써짐
        statusText.text = statusTextArray[statType];

        // 선택된 랜덤 스텟 로그 출력
        //Debug.Log($"선택된 랜덤 스텟: {statusText.text}");
    }

    // 버튼 클릭 시 선택된 스탯 적용
    public void OnStatClicked(int index)
    { 
        Debug.Log("선택된 스탯: " + statusTextArray[selectedStatType]);
        WeaponManager.Instance.RegisterWeaponNumber(index);

        // selectedStatType에 따라 클릭된 스탯을 처리
        switch (selectedStatType)
        {
            case 0:
                // 공격력 적용 
                player.SetAttack(attackPowerIncrease[rarity]);
                break;

            case 1:
                // 공격속도 적용 
                player.SetAttackSpeed(attackSpeedIncrease[rarity]);
                break;

            case 2:
                // 치명타 확률 적용 
                player.SetCritical(criticalHitIncrease[rarity]);
                break;

            case 3:
                // 치명타 피해 적용 
                player.SetCriticalDamage(criticalDamageIncrease[rarity]);
                break;

            case 4:
                // 최대 체력 증가 적용 
                player.SetMaxHp(MaxHPIncrease[rarity]);
                break;

            case 5:
                // 현재 체력 증가 적용 
                player.HealHp(currentHPIncrease[rarity]);
                break;

            case 6:
                // 방어력 증가 적용 
                player.SetArmor(armorIncrease[rarity]);
                break; 
        }
    }

}
