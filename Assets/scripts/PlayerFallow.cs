using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallow : MonoBehaviour
{
    public Transform Player;
    private Vector3 _cameraOffSet;

    [Range(0.0f, 1.0f)]
    public float SmoothFactor = 0.5f;

    void Start()
    {
        _cameraOffSet = transform.position - Player.position;
    }

    void LateUpdate()
    {
        Vector3 newpos = Player.position + _cameraOffSet;
        transform.position = Vector3.Slerp(transform.position, newpos, SmoothFactor);

    }
}
