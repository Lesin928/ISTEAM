using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade_In_Out : MonoBehaviour
{

    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut()
    {
        float fadecount = 0.0f;
        while (fadecount < 1.0f){
            fadecount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadecount);
        }
    }
    IEnumerator FadeIn()
    {
        float fadecount = 1.0f;
        while (fadecount >= 0.0f)
        {
            fadecount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadecount);
        }
    }
}
