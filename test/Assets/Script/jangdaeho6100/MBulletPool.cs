using System.Collections.Generic;
using UnityEngine;

public class MBulletPool : MonoBehaviour
{
    public static MBulletPool Instance { get; private set; } // 싱글톤 인스턴스

    [Header("총알 프리팹 목록 (인덱스 기준)")]
    public GameObject[] bulletPrefabs; // 총알 프리팹 배열 (인스펙터에서 설정)

    private Dictionary<int, Queue<GameObject>> poolDictionary;    // 인덱스별 풀
    private Dictionary<GameObject, int> objectToIndexMap;         // 오브젝트 → 인덱스 매핑

    void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<int, Queue<GameObject>>();
        objectToIndexMap = new Dictionary<GameObject, int>();
    }

    // 총알 가져오기
    public GameObject GetFromPool(int index, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(index))
        {
            poolDictionary[index] = new Queue<GameObject>(); // 없으면 새 큐 생성
        }

        GameObject obj;

        if (poolDictionary[index].Count > 0)
        {
            obj = poolDictionary[index].Dequeue(); // 꺼냄
        }
        else
        {
            obj = Instantiate(bulletPrefabs[index]); // 없으면 새로 생성
            objectToIndexMap[obj] = index;           // 매핑
        }

        obj.transform.position = position; // 위치 설정
        obj.SetActive(true);               // 활성화
        return obj;                        // 반환
    }

    // 총알 반환하기
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false); // 비활성화

        if (objectToIndexMap.TryGetValue(obj, out int index))
        {
            poolDictionary[index].Enqueue(obj); // 해당 큐에 다시 넣기
        }
        else
        {
            Destroy(obj); // 풀링된 객체가 아니면 제거
        }
    }
}
