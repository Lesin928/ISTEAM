using System.Collections.Generic; // Dictionary, Queue 사용을 위해 포함
using UnityEngine; // Unity API 사용

// 다양한 오브젝트(총알, 이펙트 등)를 태그 기반으로 풀링 관리하는 매니저 클래스
public class MObjectPoolManager : MonoBehaviour
{
    // 풀 구성 정보를 담는 구조체 (Inspector에서 직접 설정)
    [System.Serializable]
    public class Pool
    {
        public string tag;              // 풀을 구분할 태그 (예: "MBullet", "Explosion")
        public GameObject prefab;       // 풀에 저장할 오브젝트 프리팹
        public int size = 10;           // 초기 생성 개수
    }

    public static MObjectPoolManager Instance; // 싱글톤 인스턴스

    public List<Pool> pools; // 풀 리스트 (인스펙터에서 설정 가능)

    private Dictionary<string, Queue<GameObject>> poolDic; // 태그 → 오브젝트 큐 딕셔너리

    void Awake()
    {
        Instance = this;                          // 싱글톤 인스턴스 설정
        poolDic = new Dictionary<string, Queue<GameObject>>(); // 딕셔너리 초기화

        // 설정된 각 풀 항목에 대해 오브젝트 미리 생성
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>(); // 새 큐 생성

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab); // 오브젝트 생성
                obj.SetActive(false);                      // 비활성화 상태로 시작
                objectQueue.Enqueue(obj);                  // 큐에 넣기
            }

            poolDic.Add(pool.tag, objectQueue); // 태그를 키로 하여 큐 등록
        }
    }

    // 풀에서 오브젝트를 꺼내 사용하는 함수
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDic.ContainsKey(tag))          // 태그가 존재하지 않으면
            return null;                        // null 반환

        // 큐에 오브젝트가 남아있으면 꺼내고, 없으면 새로 생성
        GameObject obj = poolDic[tag].Count > 0
            ? poolDic[tag].Dequeue()
            : Instantiate(GetPoolPrefab(tag));  // 프리팹으로 새 오브젝트 생성

        obj.transform.position = position;      // 위치 설정
        obj.transform.rotation = rotation;      // 회전 설정
        obj.SetActive(true);                    // 활성화

        return obj;                             // 호출자에게 반환
    }

    // 사용한 오브젝트를 다시 풀에 반납하는 함수
    public void ReturnToPool(string tag, GameObject obj)
    {
        obj.SetActive(false);              // 비활성화
        poolDic[tag].Enqueue(obj);         // 다시 큐에 넣기
    }

    // 특정 태그에 해당하는 프리팹을 찾는 함수
    public GameObject GetPoolPrefab(string tag)
    {
        foreach (var pool in pools)
        {
            if (pool.tag == tag)           // 태그가 일치하면
                return pool.prefab;        // 해당 프리팹 반환
        }

        return null;                       // 없으면 null 반환
    }
}
