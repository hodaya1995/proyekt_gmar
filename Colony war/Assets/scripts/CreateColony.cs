using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateColony : MonoBehaviour
{
    public static int totalSoldier = 0, totalArcher = 0, totalHorse = 0, totalWorker = 0, totalAxe = 0;
    GameObject copiedSoldier;

    public void CreateAnArcher()
    {

        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;

        GameObject text = child.transform.Find("Text").gameObject;
        Text context = text.GetComponent<Text>();

        int currGold = int.Parse(context.text);
        if (currGold >= 50)
        {

            totalArcher++;
            totalSoldier++;

            Flock flock = this.transform.parent.parent.parent.parent.GetComponent<Flock>();

            copiedSoldier = flock.CreateNewSoldier(20f, 10, 1); //add to the unit an soldier
            copiedSoldier.SetActive(false);
            Invoke("TimerBarHelper", 5f); //activate soldier in 5s

            Resources.gold -= 50;

            context.text = "" + Resources.gold; //set the gold bar
        }

    }


    public void CreateAnWorker()
    {
        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;

        GameObject text = child.transform.Find("Text").gameObject;
        Text context = text.GetComponent<Text>();

        int currGold = int.Parse(context.text);
        if (currGold >= 40)
        {

            totalWorker++;
            totalSoldier++;

            Flock flock = this.transform.parent.parent.parent.parent.GetComponent<Flock>();

            copiedSoldier = flock.CreateNewWorker(100, 100.0f / 60.0f);
            copiedSoldier.SetActive(false);
            Invoke("TimerBarHelper", 5f);

            Resources.gold -= 40;

            context.text = "" + Resources.gold;
        }
    }

    public void CreateAnKnight()
    {
        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;

        GameObject text = child.transform.Find("Text").gameObject;
        Text context = text.GetComponent<Text>();

        int currGold = int.Parse(context.text);
        if (currGold >= 50)
        {

            totalAxe++;
            totalSoldier++;
            Flock flock = this.transform.parent.parent.parent.parent.GetComponent<Flock>();

            copiedSoldier = flock.CreateNewSoldier(20f, 10, 1);
            copiedSoldier.SetActive(false);
            Invoke("TimerBarHelper", 5f);

            Resources.gold -= 70;

            context.text = "" + Resources.gold;
        }

    }

    public void CreateAnHourse()
    {
        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;

        GameObject text = child.transform.Find("Text").gameObject;
        Text context = text.GetComponent<Text>();

        int currGold = int.Parse(context.text);
        if (currGold >= 50)
        {

            totalHorse++;
            totalSoldier++;

            Flock flock = this.transform.parent.parent.parent.parent.GetComponent<Flock>();

            copiedSoldier = flock.CreateNewSoldier(20f, 10, 1);
            copiedSoldier.SetActive(false);
            Invoke("TimerBarHelper", 5f);

            Resources.gold -= 60;

            context.text = "" + Resources.gold;
        }

    }

    void TimerBarHelper()
    {
        copiedSoldier.SetActive(true);
    }


}
