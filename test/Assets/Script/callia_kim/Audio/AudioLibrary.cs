using UnityEngine;
using System.Collections.Generic;

//ScriptableObject를 이용한 오디오 라이브러리 생성
//Unity의 Inspector에서 쉽게 설정할 수 있도록 함
[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    //사운드 카테고리를 정의하는 클래스
    [System.Serializable]   //Unity Inspector에서 표시되도록 설정
    public class SoundCategory
    {
        //사운드 카테고리 (예: "Player", "Monster" 등)
        public string categoryName;
        //해당 카테고리에 속하는 사운드 클립 배열
        public AudioClip[] clips;
    }

    //여러 카테고리를 배열로 저장
    public SoundCategory[] soundCategories;
    //사운드 조회를 위한 딕셔너리
    private Dictionary<string, AudioClip> soundDictionary;

    //사운드 Dictionary 초기화
    public void Init()
    {
        //새로운 딕셔너리 생성
        soundDictionary = new Dictionary<string, AudioClip>();

        //각 카테고리별로 사운드를 Dictionary에 추가
        foreach (var category in soundCategories)
        {
            foreach (var clip in category.clips)
            {
                //키 형식: "카테고리_사운드이름" (예: "Player_Attack")
                string key = $"{category.categoryName}_{clip.name}";
                soundDictionary[key] = clip;
            }
        }
    }

    //특정 카테고리에서 원하는 이름의 사운드 클립을 가져오기
    /// <param name="category">사운드 카테고리 (예: "Player")</param>
    /// <param name="clipName">사운드 이름 (예: "Attack")</param>
    /// <returns>찾은 오디오 클립 (없으면 null 반환)</returns>
    public AudioClip GetClip(string category, string clipName)
    {
        //카테고리와 클립이름을 조합한 키 생성
        string key = $"{category}_{clipName}";
        //존재하면 반환, 없으면 null
        return soundDictionary.ContainsKey(key) ? soundDictionary[key] : null;
    }
}