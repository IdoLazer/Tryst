using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    //public ParticleSystem usedTotemPartical;
    public float speedBoost = 2;
    public float goDownSpeed = 3;
    public float goDownAmount = 10;

    [Header("Materials and Children")]
    public MeshRenderer blackColumn;
    public MeshRenderer whiteColumn;
    public Material blackTurnedOffMaterial;
    public Material whiteTurnedOffMaterial;


    private bool isUsed;
    private bool goingDown;
    private float goingDownStartTime;
    private Vector3 startPos;
    private Vector3 target;

    void Start()
    {
        isUsed = false;
    }

    private void Update()
    {
        if (goingDown)
        {
            transform.position = Vector3.Lerp(startPos, target, Time.time - goingDownStartTime);
        }
    }

    // Returns true if totem was activated
    public bool useTotem(){
        if(!isUsed){
            isUsed = true;
            blackColumn.material = blackTurnedOffMaterial;
            whiteColumn.material = whiteTurnedOffMaterial;
            //usedTotemPartical.Play();
            goingDown = true;
            goingDownStartTime = Time.time;
            target = transform.position - transform.up * goDownAmount;
            startPos = transform.position;
            return true;
        }
        return false;
    }
    //public void resetTotem(){
    //    isUsed = false;
    //    //usedTotemPartical.Stop();
    //}
}
