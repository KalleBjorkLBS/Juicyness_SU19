using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Post_Process_Script : MonoBehaviour
{

    private Vignette vignetteLayer = null;
    public Mole_Script moleValue;

    public float vignetteValue = 1;
    private float lastLifeValue;

    private bool isDead = false;

    private void Awake() {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
    }

    void Update()
    {   
        float currentLife;
        
        
        
        currentLife = moleValue.life/100;
        
        if(currentLife != lastLifeValue && isDead == false){
            vignetteValue = 0.9f-currentLife;
            lastLifeValue = currentLife;    
        }

        if(currentLife == 0){
            isDead = true;
        }
        
        vignetteLayer.intensity.value = vignetteValue;
    }
}
