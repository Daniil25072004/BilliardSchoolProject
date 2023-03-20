using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueScript : MonoBehaviour
{
    private int test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0,0,0.5f);
    }
}
