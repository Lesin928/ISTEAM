using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f; 
    public GameObject effect;

    void Update()
    {
        //미사일 위쪽방향으로 움직이기
        //위의 방향 * 스피드 * 타임
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    //화면밖으로 나갈경우
    private void OnBecameInvisible()
    {
        //자기 자신 지우기
        Destroy(gameObject);
    } 

}