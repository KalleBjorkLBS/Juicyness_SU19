using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private void Update() { //Pausar spelet om man trycker ESC /Kalle
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0;
        }
    }
    public void ResumeOnClick(){ //Metod för en knapp så att man kan resume /Kalle
        Time.timeScale = 1;
    }
}
