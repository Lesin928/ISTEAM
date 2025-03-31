using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    // ������Ʈ Ǯ�� �Բ� ����� �������� ������ ��ųʸ�
    // ���� �ٸ� Ÿ���� Ǯ�� �����ϱ� ���� object�� ����
    private Dictionary<string, object> poolDictionary = new Dictionary<string, object>();

    // �� Ű�� �ش��ϴ� �������� �����ϴ� ��ųʸ�
    private Dictionary<string, GameObject> poolPrefabs = new Dictionary<string, GameObject>(); 

    void Awake()
    {
        if (Instance == null) 
            Instance = this; 
        else 
            Destroy(gameObject); 
    }
    //Ǯ ����, �����հ� ũ�� ����
    public void CreatePool<T>(string key,T  prefab, int size) where T : MonoBehaviour
    { 
        if (!poolDictionary.ContainsKey(key))
        {
            Queue<T> poolQueue = new Queue<T>();      
            // poolDictionary[key] = new Queue<GameObject>();
            poolPrefabs[key] = prefab.gameObject;  // prefab.gameObject�� ��ȯ

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
                return newObj.GetComponent<T>(); // �� ������Ʈ���� T ������Ʈ ��ȯ
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
//���Ž� �ڵ�
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    // ������Ʈ Ǯ�� �Բ� ����� �������� ������ ��ųʸ�
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    // �� Ű�� �ش��ϴ� �������� �����ϴ� ��ųʸ�
    private Dictionary<string, GameObject> poolPrefabs = new Dictionary<string, GameObject>();
    private int maxPoolSize = 100;

    void Awake()
    {
        if (Instance == null) 
            Instance = this; 
        else 
            Destroy(gameObject); 
    }
    //Ǯ ����, �����հ� ũ�� ����
    public void CreatePool<T>(string key, GameObject prefab, int size) where T : MonoBehaviour
    { 
        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();
            poolPrefabs[key] = prefab.gameObject;  // prefab.gameObject�� ��ȯ

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
            return obj.GetComponent<T>(); // GameObject���� T ������Ʈ ��ȯ
        }
        else if (poolPrefabs.ContainsKey(key)) // Ǯ�� ������Ʈ�� ���� ���, �� ������Ʈ ����
        {
            // ���� Ǯ�� ������Ʈ�� ���� ���¶��, maxPoolSize�� üũ�Ͽ� �� ������Ʈ ����
            if (poolDictionary[key].Count < maxPoolSize)
            {
                GameObject newObj = Instantiate(poolPrefabs[key]);
                newObj.SetActive(true);
                return newObj.GetComponent<T>(); // �� ������Ʈ���� T ������Ʈ ��ȯ
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