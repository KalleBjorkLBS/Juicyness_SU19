using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_Script : Score_System
{
    [SerializeField]
    GameObject mouseObject = null; //Musen

    [SerializeField]
    GameObject moleObject = null;  //Prefab för moles /Kalle
    public GameObject[] holesObject; //Platser där moles kan spawna /Kalle
    private float[] holesPositionX;  //Hålens x position /Kalle
    private float[] holesPositionY;  //Hålens y position /Kalle

    //Klass variabler
    private int numberOfMolesAlive = 0; //Hur många moles som får vara vid liv samtidigt /Kalle
    private float timeBetweenSpawn = 1f; //Tid mellan varje spawn event /Kalle
    private int randomPositionY;
    private int randomPositionX;
    private int lastRandom = 999; //Måste vara global för annars överskrivs den varje frame med fel värde /Kalle
    private GameObject newMole;
    
    void Awake()
    {
        float[] holesPositionX = new float[holesObject.Length];
        float[] holesPositionY = new float[holesObject.Length];

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
            if(random == lastRandom){ //Om den valde samma random plats igen så ökar den bara random igen och om random är mer än antalet hål den kan hamna i resetar den till 0(vilket är starten på arrayen)/Kalle
                random++;             //Detta funkar inte riktigt och fattar inte varför /Kalle
                if(random > holesPositionX.Length){
                    random = 0;
                    randomPositionX = (int) Mathf.Round(holesPositionX[random]); //väljer en random position i arrayen och rundar av det till en int /Kalle
                    randomPositionY = (int) Mathf.Round(holesPositionY[random]); //
                }
            }else if(random != lastRandom){ //Om random == en ny sifra än förra så placerar den bara ut den där /Kalle
                randomPositionX = (int) Mathf.Round(holesPositionX[random]); //
                randomPositionY = (int) Mathf.Round(holesPositionY[random]); //
            }
            
            newMole = Instantiate(moleObject,new Vector3(randomPositionX,randomPositionY,0),Quaternion.identity); //definerar newMole som ett nytt Instansiatat object /Kalle
            numberOfMolesAlive += 1;

            timeBetweenSpawn = 1f;
            lastRandom = random;
        }

        if(Input.GetMouseButtonDown(0)){ 
            if(Mathf.Round(mousePosition.x) == randomPositionX && Mathf.Round(mousePosition.y) == randomPositionY && numberOfMolesAlive > 0){ //Om musen är ungefär ovanpå en mole så träffar den och förstör den /Kalle
                numberOfMolesAlive -= 1;
                Destroy(newMole);
                MolePoints();
                scoreText.text = gameScore.ToString();
            }
        }

    }
}
