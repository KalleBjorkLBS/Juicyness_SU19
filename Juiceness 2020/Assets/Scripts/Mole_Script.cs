using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_Script : MonoBehaviour
{
    [SerializeField]
    GameObject moleObject = null; 

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            SpawnMole(0,0);
        }
    }

    public void SpawnMole(int xPosition, int yPosition){
        Instantiate(moleObject,new Vector3(xPosition,yPosition,0),Quaternion.identity);
    }
}
