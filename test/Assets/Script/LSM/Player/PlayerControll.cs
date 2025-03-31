using System.Collections;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed; // 이동 속도      
    private Player player;

    private void Start()
    {
        // Player 컴포넌트를 현재 객체에서 바로 가져오기
        player = GetComponent<Player>();

        if (player != null)
        {
            moveSpeed = player.moveSpeed; // Player의 이동 속도 값 가져오기
        }
        else
        {
            moveSpeed = 1;
        }
    }
    public void Stop()
    {
        if (moveSpeed != 0)
        {
            moveSpeed = 0;
        }
    }
    public void StartMove()
    {
        if (moveSpeed == 0)
        {
            moveSpeed = player.moveSpeed;
        }
    }

    void Update()
    {
        //이동 처리
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);

    }
}