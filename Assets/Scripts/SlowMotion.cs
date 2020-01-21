using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    float currentAmount = 0f;
    float maxAmount = 5f;
    private GameManager Gm;

    void Start()
    {
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {

        if (Gm.getState() == GameManager.State.Win)
        {

            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.3f;
            }

            else
            {
                Time.timeScale = 1.0f;
            }

            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }


        if (Time.timeScale == 0.03f)
        {

            currentAmount += Time.deltaTime;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

    }
}
