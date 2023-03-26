using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EscapeScreen : MonoBehaviour
{
    public void Play() {
        SceneManager.LoadScene(1); //Wenn Play gedrückt wird, wird auf die nächste Scene gesprungen
    }
    public void Quit() {
        SceneManager.LoadScene(0);
    }
}
