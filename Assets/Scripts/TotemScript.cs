using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    public ParticleSystem usedTotemPartical;
    public int totemCombo = 3;
    public float speedBoost = 2;
    public float lifeBoost = 50;
    private bool isUsed;

    void Start()
    {
        isUsed = false;
    }

    private void Update()
    {
        
    }

    // Returns true if totem was activated
    public bool useTotem(){
        if(!isUsed){
            isUsed = true;
            usedTotemPartical.Play();
            return true;
        }
        return false;
    }
    public void resetTotem(){
        isUsed = false;
        usedTotemPartical.Stop();
    }
}
