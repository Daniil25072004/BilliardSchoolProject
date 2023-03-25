using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooltableSize : MonoBehaviour
{
    private Vector3 vonInsideZuGesScale;
    private Vector3 size;
    private float[,] tableSizes = new float[2, 3]{{112f, 150f, 224f},{2.0f, 2.0f, 2.0f}};   
    //0 = 8pool ball, 1 = snooker
    //Es geht bei den Table Sizes um die innere Spielflächen und sie sind in cm angegeben. X ist die width und Z die Length
    private MeshRenderer meshRenderer;
    private void Start() {

        vonInsideZuGesScale = new Vector3(1.194458f, 1f, 1.09678f);

        /*  StandartSize ist (46.99, 34.51, 86.35)
            Size vom InsideTable (39.34, 0.0, 78.73)
            Wir können aber nur die Gesamte(Also die Außenscale) verändern. 
            Wir müssen also zuerst den Scale vom InsideTable zum Gesamten ausrechnen und dann auf die gewünschte Tablegröße Setzen.
            Scale (1.194458, ???, 1.09678)
        */

        meshRenderer = GetComponent<MeshRenderer>();
        size = meshRenderer.bounds.size;                
                                                        
        transform.localScale = new Vector3((tableSizes[0,0] / size.x) * vonInsideZuGesScale.x, (tableSizes[0,1]/size.y) * vonInsideZuGesScale.y, (tableSizes[0,2]/size.z) * vonInsideZuGesScale.z);
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
