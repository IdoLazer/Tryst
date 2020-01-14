using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScript : MonoBehaviour
{

    public Vector3 baseWobbleTime = new Vector3(1f, 1f, 0f);
    public Vector3 movingWobbleTime = new Vector3(3f, 1f, 0f);
    public float wobbleSpeed = 2f;
    public float baseWobbleFreq = 3f;
    public float movingWobbleFreq = 5f;
    public float baseWobbleDistance = 0.2f;
    public float movingWobbleDistance = 0.5f;
    public float accelerationTime = 1f;
    public bool isWobbling = true;

    MeshRenderer meshRender;

    private float wobbleWaveLength;
    public float curWobbleWaveTime = 0;
    private float curWobbleFreq;
    private float curWobbleDist;
    private Vector3 curWobbleTime;
    private float startAccelerationOrDeccelerationTime = 0;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        curWobbleTime = baseWobbleTime;
        curWobbleDist = baseWobbleDistance;
        curWobbleFreq = baseWobbleFreq;
        meshRender = GetComponent<MeshRenderer>();
        meshRender.material.SetFloat("_wobbleSpeed", wobbleSpeed);
        meshRender.material.SetFloat("_wobbleFreq", curWobbleFreq);
        meshRender.material.SetFloat("_wobbleDistance", curWobbleDist);
        meshRender.material.SetVector("_wobTime", curWobbleTime);
    }

    // Update is called once per frame
    void Update()
    {
        wobbleWaveLength = 2 * Mathf.PI / wobbleSpeed;
        curWobbleWaveTime = isWobbling? (curWobbleWaveTime + Time.deltaTime) % wobbleWaveLength : curWobbleWaveTime;
        meshRender.material.SetFloat("_wobControl", curWobbleWaveTime);

        meshRender.material.SetFloat("_wobbleSpeed", wobbleSpeed);
        meshRender.material.SetFloat("_wobbleFreq", curWobbleFreq);
        meshRender.material.SetFloat("_wobbleDistance", curWobbleDist);
        meshRender.material.SetVector("_wobTime", curWobbleTime);

        if (moving)
        {
            Accelerate(true);
        }
        else
        {
            Accelerate(false);
        }

        if (Input.GetButtonDown("Up"))
        {
            StartMoving();
        }
        if (Input.GetButtonUp("Up"))
        {
            StopMoving();
        }
    }

    public void StartMoving()
    {
        if (!moving)
        {
            startAccelerationOrDeccelerationTime = Time.time;
            moving = true;
        }
    }

    public void StopMoving()
    {
        if (moving)
        {
            moving = false;
            startAccelerationOrDeccelerationTime = Time.time;
        }
    }

    private void Accelerate(bool accelerating)
    {
        float t = (Time.time - startAccelerationOrDeccelerationTime) / accelerationTime;
        if (accelerating)
        {
            curWobbleTime = Vector3.Lerp(baseWobbleTime, movingWobbleTime, t);
            curWobbleDist = Mathf.Lerp(baseWobbleDistance, movingWobbleDistance, t);
            curWobbleFreq = Mathf.Lerp(baseWobbleFreq, movingWobbleFreq, t);
        }
        else
        {
            curWobbleTime = Vector3.Lerp(movingWobbleTime, baseWobbleTime, t);
            curWobbleDist = Mathf.Lerp(movingWobbleDistance, baseWobbleDistance, t);
            curWobbleFreq = Mathf.Lerp(movingWobbleFreq, baseWobbleFreq, t);
        }

    }
}
