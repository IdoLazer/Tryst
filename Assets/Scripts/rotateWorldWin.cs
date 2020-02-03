using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWorldWin : MonoBehaviour
{
    private GameObject centerofplanet;

    void Start()
    {
    }

    void Update()
    {

    }

    public Transform getWinTextcontainerOne()
    {
        return Win1.transform.Find("Win1");
    }

    public Transform getWinTextcontainerTwo()
    {
        return Win1.transform.Find("Win2");
    }

}
