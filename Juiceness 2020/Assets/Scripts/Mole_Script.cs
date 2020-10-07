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
            SpawnMole(Random.Range(1,4),Random.Range(1,4));
        }
    }

    public void SpawnMole(int xPosition, int yPosition){
        Instantiate(moleObject,new Vector3(xPosition,yPosition,0),Quaternion.identity);
    }
}
