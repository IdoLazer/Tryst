using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    private GameObject TextBox;
    private GameObject UIObj;

    private bool didSeePulse = false;
    private bool didrecievespulse = false;
    private bool didseeTrail = false;
    private bool didTrail = false;
    bool isPressed = false;

    private float LastTrailTime;
    public float betweenTrail = 20;
    public float TIME_TO_WAIT = 2.5f;
    private bool isActive = false;
    public float BASIC_TIME = 5;
    private float CurrTime;
    void Start()
    {
        TextBox = transform.GetChild(0).gameObject;
        LastTrailTime = Time.time;
        CurrTime = Time.time;
    }

    void FixedUpdate()
    {
        //whenLastMadeTrail();
        if (Time.time - CurrTime > BASIC_TIME)
        {
            CurrTime = Time.time;
        }
    }


    public void ActivateUI(string name)
    {
        if (!isActive)
        {
            isActive = true;
            Debug.Log(name);

            switch (name)
            {
                case "sentPulse":
                    //the player who sent the pulse
                    //placed
                    if (!didSeePulse)
                    {
                        didSeePulse = true;
                        getRandomStatmen2("comefindme", "hopeyourethere");
                        StartCoroutine(waitTillAnimationIsOver(UIObj));
                    }
                    break;

                case "didntMakeTRail":
                    //placed
                    if (!didTrail)
                    {
                        didTrail = true; 
                        getRandomStatmen3("rememberthisplace", "findmywayback", "searchingforme");
                        StartCoroutine(waitTillAnimationIsOver(UIObj));
                    }

                    break;

                case "meetOtherTrail":
                    if (!didseeTrail)
                    {
                        didseeTrail = true;
                        getRandomStatmen2("haveibeenhere", "sendasign");
                        StartCoroutine(waitTillAnimationIsOver(UIObj));
                    }

                    break;

                case "recievespulse":
                    //placed
                    if (!didrecievespulse)
                    {
                        didrecievespulse = true;
                        getRandomStatmen2("searchingforme", "searchingforme");
                        StartCoroutine(waitTillAnimationIsOver(UIObj));

                    }
                    break;

                case "basic":
                    getRandomStatmen3("hopeyourethere", "itslonely", "whenwillwemeet");
                    StartCoroutine(waitTillAnimationIsOver(UIObj));

                    break;

            }
        }
    }
    private void getRandomStatmen2(string a, string b)
    {
        GameObject child1 = null;
        GameObject child2 = null;

        foreach (Transform t in TextBox.transform)
        {
            if (t.name == a)// Do something to child one
            {
                child1 = t.gameObject;
            }
            if (t.name == b)// Do something to child one
            {
                child2 = t.gameObject;
            }

        }

        if (child1 != null && child2 != null)
        {
            if (Random.value < 0.5f)
            {

                UIObj = child1;
                UIObj.SetActive(true);
            }
            else
            {
                UIObj = child2;
                UIObj.SetActive(true);
            }
        }
    }



    private void getRandomStatmen3(string a, string b, string c)
    {
        GameObject child1 = null;
        GameObject child2 = null;
        GameObject child3 = null;


        foreach (Transform t in TextBox.transform)
        {
            if (t.name == a)// Do something to child one
            {
                child1 = t.gameObject;
            }
            if (t.name == b)// Do something to child one
            {
                child2 = t.gameObject;
            }
            if (t.name == c)// Do something to child one
            {
                child3 = t.gameObject;
            }

        }

        if (child1 != null && child2 != null && child3 != null)
        {
            if (Random.value < 0.3f)
            {

                UIObj = child1;
                UIObj.SetActive(true);

            }

            if (Random.value > 0.3f && Random.value < 0.7f)
            {
                UIObj = child2;
                UIObj.SetActive(true);
            }
            else
            {
                UIObj = child3;
                UIObj.SetActive(true);
            }
        }
    }

    private bool whenLastMadeTrail()
    {

        if (name == "playerOne")
        {
            isPressed = KeyJoyController.getTrailPressed_Player1();
            if (isPressed)
            {
                LastTrailTime = Time.time;
            }

        }

        if (name == "playerTwo")
        {
            isPressed = KeyJoyController.getTrailPressed_Player2();
            if (isPressed)
            {
                LastTrailTime = Time.time;
            }
        }

        if (!didTrail)
        {
            if (Time.time - LastTrailTime > betweenTrail && !isActive)
            {
                ActivateUI("didntMakeTRail");
                betweenTrail += betweenTrail;
                LastTrailTime = Time.time;
                isPressed = true;
            }
        }


        return true;
    }

    private IEnumerator waitTillAnimationIsOver(GameObject obj)
    {
        yield return new WaitForSeconds(TIME_TO_WAIT);
        if (UIObj != null)
        {
            UIObj.SetActive(false);
            isActive = false;
        }
        

    }
}
