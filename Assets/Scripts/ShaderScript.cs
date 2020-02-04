using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScript : MonoBehaviour
{
    public bool isWobbling = true;
    public float curWobbleWaveTime = 0;
    public Transform aura;

    [Header("Default Settings")]
    public Vector3 baseWobbleTime = new Vector3(1f, 1f, 0f);
    public float wobbleSpeed = 2f;
    public float baseWobbleFreq = 3f;
    public float baseWobbleDistance = 0.2f;
    public float maxFresnelPower = 2f;
    public float minAuraScale = 10f;
    public float maxAuraScale = 18f;

    [Header("Forward/Backward Movement Settings")]
    public Vector3 movingWobbleTime = new Vector3(3f, 1f, 0f);
    public float movingWobbleFreq = 5f;
    public float movingWobbleDistance = 0.5f;
    public float accelerationTime = 1f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 6f;
    [Range(0, 2 * Mathf.PI)] public float leftTurnPoint = 0;
    [Range(0, 2 * Mathf.PI)] public float rightTurnPoint = Mathf.PI;
    [Range(0, 2 * Mathf.PI)] public float rotationHoldVariation = 0.6f;
    public bool canSnap = false;

    MeshRenderer meshRender;

    private float wobbleWaveLength;
    private float curWobbleFreq;
    private float curWobbleDist;
    private Vector3 curWobbleTime;
    private float maxWobbleFreqAchieved;
    private float maxWobbleDistAchieved;
    private Vector3 maxWobbleTimeAchieved;
    private float minWobbleFreqAchieved;
    private float minWobbleDistAchieved;
    private Vector3 minWobbleTimeAchieved;
    private float wobbleWavePosBeforeRotation;
    private float startAccelerationOrDeccelerationTime = 0;
    private float startRotationTime = 0;
    private bool moving = false;
    private int rotating = 0;
    private PlayerScript playerScript;
    private float startingFresnelPower;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        curWobbleTime = baseWobbleTime;
        curWobbleDist = baseWobbleDistance;
        curWobbleFreq = baseWobbleFreq;
        meshRender = GetComponent<MeshRenderer>();
        meshRender.material.SetFloat("_wobbleSpeed", wobbleSpeed);
        meshRender.material.SetFloat("_wobbleFreq", curWobbleFreq);
        meshRender.material.SetFloat("_wobbleDistance", curWobbleDist);
        meshRender.material.SetVector("_wobTime", curWobbleTime);
        minWobbleFreqAchieved = curWobbleFreq;
        minWobbleDistAchieved = curWobbleDist;
        minWobbleTimeAchieved = curWobbleTime;
        startingFresnelPower = meshRender.material.GetFloat("_FresnelPower");
    }

    // Update is called once per frame
    void Update()
    {
        wobbleWaveLength = 2 * Mathf.PI / wobbleSpeed;
        curWobbleWaveTime = isWobbling ? (curWobbleWaveTime + Time.deltaTime) % wobbleWaveLength : curWobbleWaveTime;
        meshRender.material.SetFloat("_wobControl", curWobbleWaveTime);

        meshRender.material.SetFloat("_FresnelPower", Mathf.Lerp(startingFresnelPower, startingFresnelPower * maxFresnelPower, playerScript.ValForShader()));
        float auraScale = Mathf.Lerp(minAuraScale, maxAuraScale, playerScript.ValForShader());
        //aura.localScale = new Vector3(auraScale, aura.localScale.y, auraScale);

        meshRender.material.SetFloat("_wobbleSpeed", wobbleSpeed);
        meshRender.material.SetFloat("_wobbleFreq", curWobbleFreq);
        meshRender.material.SetFloat("_wobbleDistance", curWobbleDist);
        meshRender.material.SetVector("_wobTime", curWobbleTime);
        if (rotating != 0)
        {
            Rotate(rotating);
        }
        if (moving)
        {
            Accelerate(true);
        }
        else
        {
            Accelerate(false);
        }
    }

    public void StopRotating()
    {
        isWobbling = true;
        rotating = 0;
    }

    public void StartRotating(int dir)
    {
        if (rotating != dir)
        {
            wobbleWavePosBeforeRotation = curWobbleWaveTime;
            rotating = dir;
            startRotationTime = Time.time;
        }
    }

    private void Rotate(int dir)
    {
        isWobbling = false;
        float target = (dir > 0 ? rightTurnPoint : leftTurnPoint) / wobbleSpeed;
        if (Mathf.Abs(wobbleWavePosBeforeRotation - target) < rotationHoldVariation)
        {
            if (canSnap)
            {
                curWobbleWaveTime = target;
            }
            return;
        }
        if (wobbleWavePosBeforeRotation > target)
        {
            target += Mathf.PI;
        }
        curWobbleWaveTime = Mathf.Lerp(wobbleWavePosBeforeRotation, target, (Time.time - startRotationTime) * rotationSpeed);
    }

    public void StartMoving()
    {
        if (!moving)
        {
            minWobbleFreqAchieved = curWobbleFreq;
            minWobbleDistAchieved = curWobbleDist;
            minWobbleTimeAchieved = curWobbleTime;
            startAccelerationOrDeccelerationTime = Time.time;
            moving = true;
        }
    }

    public void StopMoving()
    {
        if (moving)
        {
            maxWobbleDistAchieved = curWobbleDist;
            maxWobbleFreqAchieved = curWobbleFreq;
            maxWobbleTimeAchieved = curWobbleTime;
            moving = false;
            startAccelerationOrDeccelerationTime = Time.time;
        }
    }

    private void Accelerate(bool accelerating)
    {
        float t = (Time.time - startAccelerationOrDeccelerationTime) / accelerationTime;
        if (accelerating)
        {
            curWobbleTime = Vector3.Lerp(minWobbleTimeAchieved, movingWobbleTime, t);
            curWobbleDist = Mathf.Lerp(minWobbleDistAchieved, movingWobbleDistance, t);
            curWobbleFreq = Mathf.Lerp(minWobbleFreqAchieved, movingWobbleFreq, t);
        }
        else
        {
            curWobbleTime = Vector3.Lerp(maxWobbleTimeAchieved, baseWobbleTime, t);
            curWobbleDist = Mathf.Lerp(maxWobbleDistAchieved, baseWobbleDistance, t);
            curWobbleFreq = Mathf.Lerp(maxWobbleFreqAchieved, baseWobbleFreq, t);
        }

    }
}