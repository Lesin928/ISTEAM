using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

/// <summary>
/// 카드 생성하는 스크립트
/// 등급에 따른 이미지 교체 및 무기 3가지 종류 배열
/// 
/// 캔버스에 그려질 배경, 볼륨, 텍스트 캔버스의 하위객체로 소환 (카드도 마찬가지)
/// </summary>
public class UI_CardManager : MonoBehaviour
{

    public static UI_CardManager Instance { get; private set; }
    public static bool IsCardDrawing { get; private set; } // 카드 그리기 진행 중인지 나타내는 플래그 추가


    // 무기 카드 배열 3가지
    public GameObject[] Card;

    // 카드 등급 이미지 배열 4가지 (일반, 고급, 희귀, 신화)
    public Sprite[] Color_Grade;

    //카드 생성 위치
    private Vector3[] spawnPositions = new Vector3[] 
    {
        new Vector3(-468, -64, 0),   // 첫 번째 카드 위치
        new Vector3(0, -64, 0),   // 두 번째 카드 위치
        new Vector3(468, -64, 0)    // 세 번째 카드 위치
    };

    //부모 객체의 캔버스
    public Canvas canvas;

    //하위 오브젝트로 들어갈 캔버스 객체들
    public GameObject Background;
    public GameObject text;
    public GameObject Volume;

    //프리팹 인스턴스 생성
    private GameObject backgroundInstance;
    private GameObject textInstance;
    private GameObject VolumeInstance;


    // 접근자 메서드 추가 (배경, 텍스트, 볼륨)
    public GameObject GetBackgroundInstance()
    {
        return backgroundInstance;
    }

    public GameObject GetTextInstance()
    {
        return textInstance;
    }

    public GameObject GetVolumeInstance()
    {
        return VolumeInstance;
    }


    UI_CardManager cardManager = UI_CardManager.Instance;
    private List<GameObject> cardInstances = new List<GameObject>(); // 카드 인스턴스를 저장할 리스트

    // 카드 인스턴스를 반환하는 메서드
    public List<GameObject> GetCardInstances()
    {
        return cardInstances;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있으면 제거
        }
    }

    //코루틴
    public void DrawCards()
    {
        StartCoroutine(Card_Background());
        StartCoroutine(Card_Volume());
        StartCoroutine(DrawCard());
        StartCoroutine(CardText()); 
    }

    public void KeepUIActive()
    {
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f; // UI를 보이게 설정
    }

    //배경 호출
    IEnumerator Card_Background()
    {
        if (Background != null)
        {
            // 프리팹을 인스턴스화하여 Hierarchy에 추가
            backgroundInstance = Instantiate(Background, canvas.transform);
            backgroundInstance.SetActive(true);
        }
        yield return null; // 한 프레임 기다리기
    }

    //빤짝 효과 볼륨 호출
    IEnumerator Card_Volume()
    {
        if (Volume != null)
        {
            // 프리팹을 인스턴스화하여 Hierarchy에 추가
            VolumeInstance = Instantiate(Volume, canvas.transform);
            VolumeInstance.SetActive(true);
        }

        yield return null; // 한 프레임 기다리기
    }


    //카드 생성 및 호출
    IEnumerator DrawCard()
    {
        List<int> drawnCardTypes = new List<int>(); // 이미 뽑힌 카드 종류 저장 리스트

        for (int i = 0; i < 3; i++)
        {
            // 1️⃣ 카드 종류(검, 활, 마법서) 랜덤 선택
            GameObject cardPrefab = Card[Random.Range(0, Card.Length)];

            int rarity = GetRandomRarity(); // 카드의 랜덤 등급 결정

            int cardType = Random.Range(0, 3); // 카드 종류 1, 2, 3

            // 중복 체크 (같은 카드가 나오면 다시 뽑기)
            if (drawnCardTypes.Contains(cardType))
            {
                i--; // 다시 뽑기 위해 인덱스를 하나 감소
                continue;
            }

            // 카드 생성
            GameObject spawnedCard = Instantiate(cardPrefab, spawnPositions[i], Quaternion.identity, canvas.transform);
            RectTransform rectTransform = spawnedCard.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = spawnPositions[i];
            }

            // 카드 생성 코드 중 등급 이미지 적용 부분: 객체 이름이 Image인 것을 찾아 이미지 교체함
            Image rarityImageComponent = spawnedCard.transform.Find("Image")?.GetComponent<Image>();
            if (rarityImageComponent != null)
            {
                // Color_Grade 배열의 rarity 값에 맞는 이미지로 설정
                rarityImageComponent.sprite = Color_Grade[rarity];
            }

            // LvUP_TextManager를 통해 스탯 및 레벨 설정
            LvUP_TextManager lvUpTextManager = spawnedCard.GetComponent<LvUP_TextManager>();
            if (lvUpTextManager != null)
            {
                lvUpTextManager.SetRarity(rarity); // rarity 설정
                lvUpTextManager.DrawStatus(new List<int>()); // 스탯을 랜덤하게 설정
            }

            cardInstances.Add(spawnedCard);

            // 카드 등장 지연
            //yield return new WaitForSeconds(0.0833f); // 약 5프레임 (60fps 기준) 대기
            yield return new WaitForSecondsRealtime(0.0833f);


        }

    }

    //카드 등급 랜덤 결정
    private int GetRandomRarity()
    {
        int random = Random.Range(0, 100);

        if (random <= 40)
        {
            Debug.Log("Rarity: 0 (일반)");
            return 0; // 일반
        }
        else if (random > 40 && random <= 70)
        {
            Debug.Log("Rarity: 1 (고급)");
            return 1; // 고급
        }
        else if (random > 70 && random <= 90)
        {
            Debug.Log("Rarity: 2 (희귀)");
            return 2; // 고급
        }
        else
        {
            Debug.Log("Rarity: 3 (신화)");
            return 3; // 신화
        }
    }


    //텍스트 호출
    IEnumerator CardText()
    {
        if (text != null)
        {
            // 프리팹을 인스턴스화하여 Hierarchy에 추가
            textInstance = Instantiate(text, canvas.transform);
            textInstance.SetActive(true);
        }

        //textInstance = Instantiate(text, canvas.transform);
        yield return null; // 한 프레임 기다리기
    }

}