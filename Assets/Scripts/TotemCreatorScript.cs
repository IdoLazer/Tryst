using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemCreatorScript : MonoBehaviour
{
    public const string TOTEMS_PARENT_NAME = "TotemsContainer";
    public const int NUM_OF_TOTEMS = 100;
    private GameObject[] totemsBank;
    [SerializeField] GameObject TotemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        totemsBank = new GameObject[NUM_OF_TOTEMS];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
