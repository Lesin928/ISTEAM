
using UnityEngine;

public abstract class SpawnFactry : MonoBehaviour
{   //최상위 팩토리 클래스. 각 스테이지별로 이 클래스를 상속받아 각자의 팩토리를 만들어준다.
    //몬스터 생성에 필요한 함수는 여기서 추가하고 오버라이드 하도록 한다
    public abstract void CreateMonster();
}