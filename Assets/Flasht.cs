using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasht : MonoBehaviour
{
    private float gapTime; //闪烁的间隔时间，在Unity中修改
    private float temp;
    bool isInvincible = true;
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //temp += Time.deltaTime;
        //if(temp >= gapTime)
        //{
        //    if(IsDisplay)
        //    {
        //        Object.gameObject.SetActive(false);
        //        IsDisplay = false;
        //        temp = 0;
        //    }
        //    else
        //    {
        //        Object.gameObject.SetActive(true);
        //        IsDisplay = true;
        //        temp = 0;
        //    }
        //} 
        if (isInvincible)
        {
            //2
            gapTime += Time.deltaTime;

            //3
            if (gapTime < 3f)
            {
                float remainder = gapTime % 0.5f;
                renderer.enabled = remainder > 0.15f;
            }
            //4
            else
            {
                renderer.enabled = true;
                isInvincible = false;
            }
        }
    }
}
