using System.Collections.Generic;
using UnityEngine;

public class MBulletPool : MonoBehaviour
{
    public static MBulletPool Instance { get; private set; } // �̱��� �ν��Ͻ�

    [Header("�Ѿ� ������ ��� (�ε��� ����)")]
    public GameObject[] bulletPrefabs; // �Ѿ� ������ �迭 (�ν����Ϳ��� ����)

    private Dictionary<int, Queue<GameObject>> poolDictionary;    // �ε����� Ǯ
    private Dictionary<GameObject, int> objectToIndexMap;         // ������Ʈ �� �ε��� ����

    void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<int, Queue<GameObject>>();
        objectToIndexMap = new Dictionary<GameObject, int>();
    }

    // �Ѿ� ��������
    public GameObject GetFromPool(int index, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(index))
        {
            poolDictionary[index] = new Queue<GameObject>(); // ������ �� ť ����
        }

        GameObject obj;

        if (poolDictionary[index].Count > 0)
        {
            obj = poolDictionary[index].Dequeue(); // ����
        }
        else
        {
            obj = Instantiate(bulletPrefabs[index]); // ������ ���� ����
            objectToIndexMap[obj] = index;           // ����
        }

        obj.transform.position = position; // ��ġ ����
        obj.SetActive(true);               // Ȱ��ȭ
        return obj;                        // ��ȯ
    }

    // �Ѿ� ��ȯ�ϱ�
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false); // ��Ȱ��ȭ

        if (objectToIndexMap.TryGetValue(obj, out int index))
        {
            poolDictionary[index].Enqueue(obj); // �ش� ť�� �ٽ� �ֱ�
        }
        else
        {
            Destroy(obj); // Ǯ���� ��ü�� �ƴϸ� ����
        }
    }
}
