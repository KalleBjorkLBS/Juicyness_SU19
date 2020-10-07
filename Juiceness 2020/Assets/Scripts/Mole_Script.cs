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
        float timeBetweenSpawn = 1f;
        timeBetweenSpawn -= 10*Time.deltaTime;
        Debug.Log(timeBetweenSpawn);

        if(timeBetweenSpawn <= 0f){
            SpawnMole(Random.Range(1,4),Random.Range(1,4));
            timeBetweenSpawn = 0f;
        }

    }

    public void SpawnMole(int xPosition, int yPosition){ //Method som sköter spawning av moles //Kalle 
        Instantiate(moleObject,new Vector3(xPosition,yPosition,0),Quaternion.identity);
    }
}
