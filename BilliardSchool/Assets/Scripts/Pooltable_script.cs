using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooltable_script : MonoBehaviour
{
    private Vector3 vonInsideZuGesScale;
    private Vector3 size;
    private float gameFieldHeightPercentage = 0.9584f;    //Wenn z.B. von tableSizes die Höhe des Tisches 150cm beträgt, liegt auf den Y-Wert 95,84% von 150cm, also auf der Höhe von 143,76cm das Spielfeld. Dies zu wissen ist wichtig um die Kugeln drauf zu setzen
    private float[,] tableSizes = new float[2, 3]{{112f, 150f, 224f},{2.0f, 2.0f, 2.0f}};   
    //0 = 8pool ball, 1 = snooker
    //Es geht bei den Table Sizes um die innere Spielflächen und sie sind in cm angegeben. X ist die width und Z die Length
    private MeshRenderer meshRenderer;
    
    public float getGameFieldHeight(){

        meshRenderer = GetComponent<MeshRenderer>();
        return meshRenderer.bounds.size.y*gameFieldHeightPercentage;

    }

    public void setSize(int t){

        meshRenderer = GetComponent<MeshRenderer>();
        vonInsideZuGesScale = new Vector3(1.194458f, 1f, 1.09678f);

        //Standart meshRenderer.bounds.size.y = 34.50839
        //Danach (nach auf scaling) meshRenderer.bounds.size.y = 150

        /*  StandartSize ist (46.99, 34.51, 86.35)
            Size vom InsideTable (39.34, 0.0, 78.73)
            Wir können aber nur die Gesamte(Also die Außenscale) verändern. 
            Wir müssen also zuerst den Scale vom InsideTable zum Gesamten ausrechnen und dann auf die gewünschte Tablegröße Setzen.
            Scale (1.194458, ???, 1.09678)
        */

        size = meshRenderer.bounds.size;                
                                                        
        transform.localScale = new Vector3((tableSizes[t,0] / size.x) * vonInsideZuGesScale.x, (tableSizes[t,1]/size.y) * vonInsideZuGesScale.y, (tableSizes[t,2]/size.z) * vonInsideZuGesScale.z);
        transform.localPosition = new Vector3(0,0,0);
        transform.position = new Vector3(0,0,0);

    }

}
