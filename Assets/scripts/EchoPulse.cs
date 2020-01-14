using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoPulse : MonoBehaviour
{
    public float maxScale = 100f;
    public float speed = 10f;

    private bool activated = false;
    private float startTime;

    public void ActivatePulse()
    {
        transform.localScale = Vector3.zero;
        activated = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (activated)
        {
            if (transform.localScale.x >= maxScale)
            {
                Destroy(gameObject);
                return;
            }
            float fractionOfFinalSize = ((Time.time - startTime) * speed) / maxScale;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * maxScale, fractionOfFinalSize);
        }
    }
}
