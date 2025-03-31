using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    // 오브젝트 풀과 함께 사용할 프리팹을 저장할 딕셔너리
    // 서로 다른 타입의 풀을 관리하기 위해 object로 저장
    private Dictionary<string, object> poolDictionary = new Dictionary<string, object>();

    // 각 키에 해당하는 프리팹을 저장하는 딕셔너리
    private Dictionary<string, GameObject> poolPrefabs = new Dictionary<string, GameObject>(); 

    void Awake()
    {
        if (Instance == null) 
            Instance = this; 
        else 
            Destroy(gameObject); 
    }
    //풀 생성, 프리팹과 크기 결정
    public void CreatePool<T>(string key,T  prefab, int size) where T : MonoBehaviour
    { 
        if (!poolDictionary.ContainsKey(key))
        {
            Queue<T> poolQueue = new Queue<T>();      
            // poolDictionary[key] = new Queue<GameObject>();
            poolPrefabs[key] = prefab.gameObject;  // prefab.gameObject로 변환

            for (int i = 0; i < size; i++)
            { 
                T obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                poolQueue.Enqueue(obj);
            }
            poolDictionary.Add(key, poolQueue);
        }
    }

    public T GetFromPool<T>(string key) where T : MonoBehaviour
    {
        if (poolDictionary.ContainsKey(key))
        {
            Queue<T> poolQueue = poolDictionary[key] as Queue<T>;
            if (poolQueue != null && poolQueue.Count > 0)
            {
                T obj = poolQueue.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                GameObject newObj = Instantiate(poolPrefabs[key]);
                newObj.SetActive(true);
                return newObj.GetComponent<T>(); // 새 오브젝트에서 T 컴포넌트 반환
            }

        }
        else
        {
            Debug.LogError($"No pool found with key: {key}");
        }
        return null;
    }

    public void ReturnToPool<T>(string key, T obj) where T : MonoBehaviour
    {
        if (poolDictionary.ContainsKey(key))
        {
            Queue<T> poolQueue = poolDictionary[key] as Queue<T>;
            if (poolQueue != null)
            {
                obj.gameObject.SetActive(false);
                poolQueue.Enqueue(obj);
            }
        }
    }

}


/*
//레거시 코드
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    // 오브젝트 풀과 함께 사용할 프리팹을 저장할 딕셔너리
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    // 각 키에 해당하는 프리팹을 저장하는 딕셔너리
    private Dictionary<string, GameObject> poolPrefabs = new Dictionary<string, GameObject>();
    private int maxPoolSize = 100;

    void Awake()
    {
        if (Instance == null) 
            Instance = this; 
        else 
            Destroy(gameObject); 
    }
    //풀 생성, 프리팹과 크기 결정
    public void CreatePool<T>(string key, GameObject prefab, int size) where T : MonoBehaviour
    { 
        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();
            poolPrefabs[key] = prefab.gameObject;  // prefab.gameObject로 변환

            for (int i = 0; i < size; i++)
            { 
                GameObject obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                poolDictionary[key].Enqueue(obj.gameObject);
            }
        }
    }

    public T GetFromPool<T>(string key) where T : MonoBehaviour
    {
        if (poolDictionary.ContainsKey(key) && poolDictionary[key].Count > 0)
        {
            GameObject obj = poolDictionary[key].Dequeue();
            obj.SetActive(true);
            return obj.GetComponent<T>(); // GameObject에서 T 컴포넌트 반환
        }
        else if (poolPrefabs.ContainsKey(key)) // 풀에 오브젝트가 없을 경우, 새 오브젝트 생성
        {
            // 만약 풀에 오브젝트가 없는 상태라면, maxPoolSize를 체크하여 새 오브젝트 생성
            if (poolDictionary[key].Count < maxPoolSize)
            {
                GameObject newObj = Instantiate(poolPrefabs[key]);
                newObj.SetActive(true);
                return newObj.GetComponent<T>(); // 새 오브젝트에서 T 컴포넌트 반환
            }
        }
        return null;
    }

    public void ReturnToPool(string key, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[key].Enqueue(obj);
    }

} 
*/