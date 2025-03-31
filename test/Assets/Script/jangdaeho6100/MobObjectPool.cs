using System.Collections.Generic;
using UnityEngine;

// 몬스터 풀을 관리하는 클래스 
public class MobObjectPool : MonoBehaviour
{
    public GameObject[] monsterPrefabs;                    // 인스펙터에서 설정할 몬스터 프리팹 배열

    private Dictionary<int, Queue<GameObject>> poolDictionary;    // 프리팹 인덱스별 풀 큐
    private Dictionary<GameObject, int> objectToIndexMap;         // 오브젝트 → 인덱스 역참조 (반납용)

    // 초기화 시 딕셔너리만 생성 (프리팹 생성은 하지 않음)
    void Awake()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();     // 인덱스 → 큐 초기화
        objectToIndexMap = new Dictionary<GameObject, int>();          // 오브젝트 → 인덱스 초기화
    }

    // 풀에서 오브젝트 가져오기
    public GameObject GetFromPool(int index, Vector3 position)
    {
        // 해당 인덱스가 없다면 새로 큐를 만든다
        if (!poolDictionary.ContainsKey(index))
        {
            poolDictionary[index] = new Queue<GameObject>();           // 큐 생성
        }

        GameObject obj;

        // 큐에 남은 오브젝트가 있다면 꺼냄
        if (poolDictionary[index].Count > 0)
        {
            obj = poolDictionary[index].Dequeue();                     // 큐에서 하나 꺼냄
        }
        else
        {
            // 없다면 새로 생성
            obj = Instantiate(monsterPrefabs[index]);                  // 인스턴스 생성
            objectToIndexMap[obj] = index;                             // 오브젝트 → 인덱스 매핑
        }

        obj.transform.position = position;                             // 위치 설정
        obj.SetActive(true);                                           // 활성화
        return obj;                                                    // 오브젝트 반환
    }

    // 오브젝트를 풀로 되돌리기
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);                                          // 비활성화 처리

        // 해당 오브젝트가 어느 프리팹 인덱스에서 왔는지 확인
        if (objectToIndexMap.TryGetValue(obj, out int index))
        {
            poolDictionary[index].Enqueue(obj);                        // 해당 인덱스 큐에 다시 넣기
        }
    }
}