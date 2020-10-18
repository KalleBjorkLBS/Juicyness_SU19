using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_sound : MonoBehaviour
{

    private AudioSource spawnSound = null;
    
    private void Awake() {
        spawnSound = GetComponent<AudioSource>();
        spawnSound.Play();
    }

}
