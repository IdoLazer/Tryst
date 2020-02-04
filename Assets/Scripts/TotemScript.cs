using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    public float speedBoost = 2;
    public float goDownSpeed = 3;
    public float goDownAmount = 10;
    public float resetTime = 60f;

    [Header("Materials and Children")]
    public MeshRenderer blackColumn;
    public MeshRenderer whiteColumn;
    public Material blackTurnedOffMaterial;
    public Material whiteTurnedOffMaterial;
    [Range(-3, 3)]
    public float minPulsePitch = 1.5f; 
    [Range(-3, 3)]
    public float maxPulsePitch = 0.5f; 
    private AudioSource totemSound;

    private bool isUsed;
    private bool goingDown;
    private bool goingUp;
    private float movingStartTime;
    private Vector3 startPos;
    private Vector3 target;
    private Material whiteMaterial;
    private Material blackMaterial;

    void Start()
    {
        totemSound = gameObject.GetComponent<AudioSource>();
        isUsed = false;
        whiteMaterial = whiteColumn.material;
        blackMaterial = blackColumn.material;
        target = transform.position - transform.up * goDownAmount;
        startPos = transform.position;
    }

    private void Update()
    {
        if (goingDown)
        {
            transform.position = Vector3.Lerp(startPos, target, Time.time - movingStartTime);
        }
        if (goingUp)
        {
            transform.position = Vector3.Lerp(target, startPos, Time.time - movingStartTime);
        }
    }

    // Returns true if totem was activated
    public bool useTotem(){
        if(!isUsed){
            isUsed = true;
            blackColumn.material = blackTurnedOffMaterial;
            whiteColumn.material = whiteTurnedOffMaterial;
            totemSound.pitch =  Random.Range(minPulsePitch, maxPulsePitch);
            totemSound.Play();
            goingUp = false;
            goingDown = true;
            movingStartTime = Time.time;
            StartCoroutine(WaitUntilReset());
            return true;
        }
        return false;
    }

    public IEnumerator WaitUntilReset() 
    {
        yield return new WaitForSeconds(resetTime);
        isUsed = false;
        blackColumn.material = blackMaterial;
        whiteColumn.material = whiteMaterial;
        goingDown = false;
        goingUp = true;
        movingStartTime = Time.time;
    }

}
