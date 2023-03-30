using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNetwork_script : MonoBehaviour
{
    
    [SerializeField] GameObject Hitbox1;
    [SerializeField] GameObject Hitbox2;
    [SerializeField] GameObject Hitbox3;
    [SerializeField] GameObject Hitbox4;
    [SerializeField] GameObject Hitbox5;
    [SerializeField] GameObject Hitbox6;

    private Hitbox[] hitbox_scripts = new Hitbox[6];

    // Start is called before the first frame update

    public void registerHitboxCollision(string s, int number){

        GameObject ball = GameObject.Find(s);
        TESTSCRIPT script = ball.GetComponent<TESTSCRIPT>();
        script.test();

    }

    void Start()
    {
        
        hitbox_scripts[0] = Hitbox1.GetComponent<Hitbox>();
        hitbox_scripts[1] = Hitbox2.GetComponent<Hitbox>();
        hitbox_scripts[2] = Hitbox3.GetComponent<Hitbox>();
        hitbox_scripts[3] = Hitbox4.GetComponent<Hitbox>();
        hitbox_scripts[4] = Hitbox5.GetComponent<Hitbox>();
        hitbox_scripts[5] = Hitbox6.GetComponent<Hitbox>();

        for(int n = 0; n < 6; n++){

            hitbox_scripts[n].setNumber(n);

        }
    }

}
