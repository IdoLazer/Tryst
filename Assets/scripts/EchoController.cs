using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoController : MonoBehaviour
{
    public const string ECHO_PULSE_PARENT_NAME = "Echoes";

    [SerializeField] GameObject echoPulsePrefab;
    [SerializeField] float timeBetweenPulses = 0.5f;

    private GameObject echoPulsesParent;
    private bool canSendPulse = true;

    void Start()
    {
        CreateEchoesParent();
    }

    private void CreateEchoesParent()
    {
        echoPulsesParent = GameObject.Find(ECHO_PULSE_PARENT_NAME);
        if (!echoPulsesParent)
        {
            echoPulsesParent = new GameObject(ECHO_PULSE_PARENT_NAME);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (canSendPulse)
                StartCoroutine(SendPulse());
        }
    }

    private IEnumerator SendPulse()
    {
        canSendPulse = false;
        GameObject pulse = Instantiate(echoPulsePrefab, transform.position, Quaternion.identity, echoPulsesParent.transform) as GameObject;
        pulse.GetComponent<EchoPulse>().ActivatePulse();
        yield return new WaitForSeconds(timeBetweenPulses);
        canSendPulse = true;
    }
}
