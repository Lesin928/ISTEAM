using System.Collections.Generic; // Dictionary, Queue ����� ���� ����
using UnityEngine; // Unity API ���

// �پ��� ������Ʈ(�Ѿ�, ����Ʈ ��)�� �±� ������� Ǯ�� �����ϴ� �Ŵ��� Ŭ����
public class MObjectPoolManager : MonoBehaviour
{
    // Ǯ ���� ������ ��� ����ü (Inspector���� ���� ����)
    [System.Serializable]
    public class Pool
    {
        public string tag;              // Ǯ�� ������ �±� (��: "MBullet", "Explosion")
        public GameObject prefab;       // Ǯ�� ������ ������Ʈ ������
        public int size = 10;           // �ʱ� ���� ����
    }

    public static MObjectPoolManager Instance; // �̱��� �ν��Ͻ�

    public List<Pool> pools; // Ǯ ����Ʈ (�ν����Ϳ��� ���� ����)

    private Dictionary<string, Queue<GameObject>> poolDic; // �±� �� ������Ʈ ť ��ųʸ�

    void Awake()
    {
        Instance = this;                          // �̱��� �ν��Ͻ� ����
        poolDic = new Dictionary<string, Queue<GameObject>>(); // ��ųʸ� �ʱ�ȭ

        // ������ �� Ǯ �׸� ���� ������Ʈ �̸� ����
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>(); // �� ť ����

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab); // ������Ʈ ����
                obj.SetActive(false);                      // ��Ȱ��ȭ ���·� ����
                objectQueue.Enqueue(obj);                  // ť�� �ֱ�
            }

            poolDic.Add(pool.tag, objectQueue); // �±׸� Ű�� �Ͽ� ť ���
        }
    }

    // Ǯ���� ������Ʈ�� ���� ����ϴ� �Լ�
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDic.ContainsKey(tag))          // �±װ� �������� ������
            return null;                        // null ��ȯ

        // ť�� ������Ʈ�� ���������� ������, ������ ���� ����
        GameObject obj = poolDic[tag].Count > 0
            ? poolDic[tag].Dequeue()
            : Instantiate(GetPoolPrefab(tag));  // ���������� �� ������Ʈ ����

        obj.transform.position = position;      // ��ġ ����
        obj.transform.rotation = rotation;      // ȸ�� ����
        obj.SetActive(true);                    // Ȱ��ȭ

        return obj;                             // ȣ���ڿ��� ��ȯ
    }

    // ����� ������Ʈ�� �ٽ� Ǯ�� �ݳ��ϴ� �Լ�
    public void ReturnToPool(string tag, GameObject obj)
    {
        obj.SetActive(false);              // ��Ȱ��ȭ
        poolDic[tag].Enqueue(obj);         // �ٽ� ť�� �ֱ�
    }

    // Ư�� �±׿� �ش��ϴ� �������� ã�� �Լ�
    public GameObject GetPoolPrefab(string tag)
    {
        foreach (var pool in pools)
        {
            if (pool.tag == tag)           // �±װ� ��ġ�ϸ�
                return pool.prefab;        // �ش� ������ ��ȯ
        }

        return null;                       // ������ null ��ȯ
    }
}
