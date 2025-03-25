using System.Collections.Generic;
using UnityEngine;

public class Gauge_Controller : MonoBehaviour
{
    public List<Transform> obj;
    public List<GameObject> guage_bar;
    new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        for (int i = 0; i < obj.Count; i++)
        {
            guage_bar[i].transform.position = obj[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            guage_bar[i].transform.position = camera.WorldToScreenPoint(obj[i].position + new Vector3(0, 1.5f, 0));
        }
    }
}
