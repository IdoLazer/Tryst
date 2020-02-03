using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    private GameObject TextBox;
    GameObject UIObj;
    private bool didSeePulse = false;
    private bool didrecievespulse = false;
    private bool didseeTrail = false;

    private float LastTrailTime;
    public float betweenTrail = 15;
    public float TIME_TO_WAIT = 2.5f;
    private bool isActive = false;
    public float BASIC_TIME = 60;
    private float CurrTime;
    void Start()
    {
        TextBox = transform.GetChild(0).gameObject;
        LastTrailTime = Time.time;
        CurrTime = Time.time;
    }

    void FixedUpdate()
    {
        whenLastMadeTrail();
        if (Time.time - CurrTime > BASIC_TIME)
        {
            CurrTime = Time.time;
        }
    }


    public void ActivateUI(string name)
    {
        Debug.Log(name);
        if (!isActive)
        {
            isActive = true;
            switch (name)
            {
                case "sentPulse":
                    //the player who sent the pulse
                    //placed
                    if (!didSeePulse)
                    {
                        didSeePulse = true;
                        getRandomStatmen2(0, 3);
                        StartCoroutine(waitTillAnimationIsOver(UIObj));
                    }
                    break;

                case "didntMakeTRail":
                    //placed
                    getRandomStatmen3(5, 1, 7);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));

                    break;

                case "meetOtherTrail":
                    if (!didseeTrail)
                    {
                        didseeTrail = true;
                        getRandomStatmen2(2, 8);
                        StartCoroutine(waitTillAnimationIsOver(UIObj));
                    }

                    break;

                case "recievespulse":
                    //placed
                    if (!didrecievespulse)
                    {
                        didrecievespulse = true;
                        UIObj = TextBox.transform.GetChild(6).gameObject;
                        UIObj.SetActive(true);
                        StartCoroutine(waitTillAnimationIsOver(UIObj));
                    }
                    break;

                case "basic":
                    getRandomStatmen3(4, 9, 10);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));

                    break;

            }
        }
    }
    private void getRandomStatmen2(int a, int b)
    {

        if (Random.value < 0.5f)
        {
            UIObj = TextBox.transform.GetChild(a).gameObject;
            UIObj.SetActive(true);
        }
        else
        {
            UIObj = TextBox.transform.GetChild(b).gameObject;
            UIObj.SetActive(true);
        }
    }



    private void getRandomStatmen3(int a, int b, int c)
    {

        if (Random.value < 0.3f)
        {
            UIObj = TextBox.transform.GetChild(a).gameObject;
            UIObj.SetActive(true);
        }

        if (Random.value > 0.3f && Random.value < 0.7f)
        {
            UIObj = TextBox.transform.GetChild(a).gameObject;
            UIObj.SetActive(true);
        }
        else
        {
            UIObj = TextBox.transform.GetChild(c).gameObject;
            UIObj.SetActive(true);
        }
    }

    private bool whenLastMadeTrail()
    {
        bool isPressed = false;

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

        if (!isPressed)
        {
            if (Time.time - LastTrailTime > betweenTrail)
            {
                ActivateUI("didntMakeTRail");
                betweenTrail += betweenTrail;
                LastTrailTime = Time.time;
            }
        }


        return true;
    }

    private IEnumerator waitTillAnimationIsOver(GameObject obj)
    {
        yield return new WaitForSeconds(TIME_TO_WAIT);
        obj.SetActive(false);
        isActive = false;

    }
}
