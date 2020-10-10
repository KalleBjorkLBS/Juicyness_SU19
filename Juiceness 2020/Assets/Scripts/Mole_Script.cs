using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_Script : MonoBehaviour
{
    [SerializeField]
    GameObject mouseObject = null;

    [SerializeField]
    GameObject moleObject = null;  //Prefab för moles /Kalle
    public GameObject[] holesObject = new GameObject[9]; //Platser där moles kan spawna /Kalle
    private float[] holesPositionX = new float[9];  //Hålens x position /Kalle
    private float[] holesPositionY = new float[9];  //Hålens y position /Kalle

    //Klass variabler
    private int numberOfMolesAlive = 0; //Hur många moles som får vara vid liv samtidigt /Kalle
    private float timeBetweenSpawn = 1f; //Tid mellan varje spawn event /Kalle
    private int randomPositionY;
    private int randomPositionX;
    private int lastRandom;
    private GameObject newMole;
    void Awake()
    {
        for (int i = 0; i < Mathf.Min(holesObject.Length, holesPositionX.Length); i++) //Bestämer x och y värden i deras respektive array baserat på hålens position /Kalle
        {
            holesPositionX[i] = holesObject[i].transform.position.x;
            holesPositionY[i] = holesObject[i].transform.position.y;
        }
    }
    
    void Update()
    {   

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; //Just Z axis so its getting the position infront of the camera
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //Gets mouse postion in world space
        mouseObject.transform.position = mousePosition; //Object following the mouse position

 

        
        timeBetweenSpawn -= 1*Time.deltaTime;

        if(timeBetweenSpawn <= 0f && numberOfMolesAlive < 1){
           
            int random = Random.Range(0,holesPositionX.Length);
            if(random != lastRandom){
                random = Random.Range(0,holesPositionX.Length);
                randomPositionX = (int) Mathf.Round(holesPositionX[random]);
                randomPositionY = (int) Mathf.Round(holesPositionY[random]);
            }else{
                randomPositionX = (int) Mathf.Round(holesPositionX[random]);
                randomPositionY = (int) Mathf.Round(holesPositionY[random]);
            }
            
            newMole = Instantiate(moleObject,new Vector3(randomPositionX,randomPositionY,0),Quaternion.identity);
            numberOfMolesAlive += 1;

            timeBetweenSpawn = 1f;
            lastRandom = random;
        }

        if(Input.GetMouseButtonDown(0)){
            if(Mathf.Round(mousePosition.x) == randomPositionX && Mathf.Round(mousePosition.y) == randomPositionY){
                Debug.Log("hit");
                numberOfMolesAlive -= 1;
                Destroy(newMole);
            }
        }

    }

    public void SpawnMole(float xPosition, float yPosition){ //Method som sköter spawning av moles /Kalle 
        Instantiate(moleObject,new Vector3(xPosition,yPosition,0),Quaternion.identity);
    }
}
