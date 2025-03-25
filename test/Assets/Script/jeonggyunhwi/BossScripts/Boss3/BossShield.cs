using UnityEngine;

public class BossShield : MonoBehaviour
{
    public float time = 0f;
    public int Shield_Point { get; set ; }
    public float current_time = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shield_Point = 30;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Shield_Point == 0)
        //{
        //    current_time += Time.deltaTime;
        //    Debug.Log(current_time);
        //    if(current_time >= 4)
        //    {
        //        gameObject.SetActive(true);
        //        Shield_Point = 30;
                
        //        Debug.Log("½¯µå Àç»ý¼º");
        //        current_time = 0;
        //    }
        //}
    }

    public void Damage_Shield(int att)
    {
        Shield_Point -= att;
        if(Shield_Point <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
