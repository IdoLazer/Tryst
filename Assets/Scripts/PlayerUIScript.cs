using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
   private GameObject TextBox;
   private bool FirstLight = true;
    private bool isAlmostDead = false;
   GameObject UIObj;

    void Start()
    {
        TextBox = transform.GetChild(0).gameObject;
    }

    public void ActivateUI(string name)
    {
        Debug.Log(name);
        switch (name)
        {
          
            case "sentPulse":
                //not placed
                if (!isAlmostDead)
                {
                    getRandomStatmen(0, 3);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));
                }
                break;

            case "didntsendPulse":
                //placed
                if (!isAlmostDead)
                {
                    UIObj = TextBox.transform.GetChild(9).gameObject;
                    UIObj.SetActive(true);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));
                }

                break;

            case "didntMakeTRail":
                //placed
                if (!isAlmostDead)
                {
                    getRandomStatmen(7, 1);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));
                }
                break;

            case "meetOtherTrail":
                //placed
                if (!isAlmostDead)
                {
                    getRandomStatmen(2, 10);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));
                }

                break;
            case "lightout":
                //placed
                if (FirstLight)
                {
                    isAlmostDead = true;
                    FirstLight = false;
                    UIObj = TextBox.transform.GetChild(5).gameObject;
                    UIObj.SetActive(true);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));

                }
                else
                {
                    UIObj = TextBox.transform.GetChild(4).gameObject;
                    UIObj.SetActive(true);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));

                }
                break;
            case "recievespulse":
                //placed
                if (!isAlmostDead)
                {
                    getRandomStatmen(8, 11);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));
                }

                break;
            case "basic":
                //notplaced
                if (!isAlmostDead)
                {
                    getRandomStatmen(6, 12);
                    StartCoroutine(waitTillAnimationIsOver(UIObj));
                }

                break;

        }
    }
    private void getRandomStatmen(int a, int b)
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

    private IEnumerator waitTillAnimationIsOver(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        obj.SetActive(false);

    }
}
