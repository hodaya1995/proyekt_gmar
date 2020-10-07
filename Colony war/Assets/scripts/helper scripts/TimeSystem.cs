using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using DentedPixel;
using System.Collections.Specialized;
using UnityEngine.UI;

public class TimeSystem : Building
{
    public GameObject bar;
    public int time;

    public void AnimateBar()
    {
        bar.SetActive(true);
        LeanTween.scaleX(bar, 1, time).setOnComplete(reverseScaleX);
    }

    public void reverseScaleX()
    {
        bar.transform.localScale += new Vector3(-1, 0, 0);
        bar.SetActive(false);
        OpenPanel();
    }
    /*
    public void GoldCheck()
    {
        
        int cost;
        if (this.name == "archer building") cost = 50;
        else if (this.name == "stable building") cost = 60;
        else cost = 40;

        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;

        GameObject text = child.transform.Find("Text").gameObject;
        Text context = text.GetComponent<Text>();


        if (int.Parse(context.text) >= cost) AnimateBar();
        else OpenPanel();
        
        AnimateBar();
    }
*/
}