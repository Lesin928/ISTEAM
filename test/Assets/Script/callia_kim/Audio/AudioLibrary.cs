using UnityEngine;
using System.Collections.Generic;

//ScriptableObject�� �̿��� ����� ���̺귯�� ����
//Unity�� Inspector���� ���� ������ �� �ֵ��� ��
[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    //���� ī�װ��� �����ϴ� Ŭ����
    [System.Serializable]   //Unity Inspector���� ǥ�õǵ��� ����
    public class SoundCategory
    {
        //���� ī�װ� (��: "Player", "Monster" ��)
        public string categoryName;
        //�ش� ī�װ��� ���ϴ� ���� Ŭ�� �迭
        public AudioClip[] clips;
    }

    //���� ī�װ��� �迭�� ����
    public SoundCategory[] soundCategories;
    //���� ��ȸ�� ���� ��ųʸ�
    private Dictionary<string, AudioClip> soundDictionary;

    //���� Dictionary �ʱ�ȭ
    public void Init()
    {
        //���ο� ��ųʸ� ����
        soundDictionary = new Dictionary<string, AudioClip>();

        //�� ī�װ����� ���带 Dictionary�� �߰�
        foreach (var category in soundCategories)
        {
            foreach (var clip in category.clips)
            {
                //Ű ����: "ī�װ�_�����̸�" (��: "Player_Attack")
                string key = $"{category.categoryName}_{clip.name}";
                soundDictionary[key] = clip;
            }
        }
    }

    //Ư�� ī�װ����� ���ϴ� �̸��� ���� Ŭ���� ��������
    /// <param name="category">���� ī�װ� (��: "Player")</param>
    /// <param name="clipName">���� �̸� (��: "Attack")</param>
    /// <returns>ã�� ����� Ŭ�� (������ null ��ȯ)</returns>
    public AudioClip GetClip(string category, string clipName)
    {
        //ī�װ��� Ŭ���̸��� ������ Ű ����
        string key = $"{category}_{clipName}";
        //�����ϸ� ��ȯ, ������ null
        return soundDictionary.ContainsKey(key) ? soundDictionary[key] : null;
    }
}