using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUVScroll : MonoBehaviour
{
    public RawImage image;
    public float speed = 0.05f;
    public float uv_x = 0;
    public float uv_y = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uv_x += speed * Time.deltaTime;
        uv_y += speed * Time.deltaTime;
        image.uvRect = new Rect(uv_x, uv_y, 1, 1);
    }
}
