using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_Script : MonoBehaviour
{
    [SerializeField]
    GameObject mouseObject = null; //Musen

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
    private int lastRandom; //Måste vara global för annars överskrivs den varje frame med fel värde /Kalle
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
            if(random == lastRandom){ //Om den valde samma random plats igen så gör den om random uträkningen, jag vet att detta inte löser det fullständingt men det minskar chansen avsevärt /Kalle
                random = Random.Range(0,holesPositionX.Length);
                randomPositionX = (int) Mathf.Round(holesPositionX[random]); //väljer en random position i arrayen och rundar av det till en int /Kalle
                randomPositionY = (int) Mathf.Round(holesPositionY[random]); //
            }else{                                                           //
                randomPositionX = (int) Mathf.Round(holesPositionX[random]); //
                randomPositionY = (int) Mathf.Round(holesPositionY[random]); //
            }
            
            newMole = Instantiate(moleObject,new Vector3(randomPositionX,randomPositionY,0),Quaternion.identity); //definerar newMole som ett nytt Instansiatat object /Kalle
            numberOfMolesAlive += 1;

            timeBetweenSpawn = 1f;
            lastRandom = random;
        }

        if(Input.GetMouseButtonDown(0)){ 
            if(Mathf.Round(mousePosition.x) == randomPositionX && Mathf.Round(mousePosition.y) == randomPositionY){ //Om musen är ungefär ovanpå en mole så träffar den och förstör den /Kalle
                numberOfMolesAlive -= 1;
                Destroy(newMole);
            }
        }

    }
}
