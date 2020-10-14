using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_Script : Score_System
{
    [SerializeField]
    GameObject mouseObject = null; //Musen

    [SerializeField]
    GameObject moleObject = null;  //Prefab för moles /Kalle
    public GameObject[] holesObject = new GameObject[9]; //Platser där moles kan spawna /Kalle
    private float[] holesPositionX = new float[99];  //Hålens x position /Kalle
    private float[] holesPositionY = new float[99];  //Hålens y position /Kalle

    //Klass variabler
    private int numberOfMolesAlive = 0; //Hur många moles som får vara vid liv samtidigt /Kalle
    private float timeBetweenSpawn = 1f; //Tid mellan varje spawn event /Kalle
    private float randomPositionY;
    private float randomPositionX;
    private int lastRandom = 999; //Måste vara global för annars överskrivs den varje frame med fel värde /Kalle
    private GameObject newMole;

    private float life = 100;
    
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
           
            int random = Random.Range(0,holesObject.Length);
            if(random == lastRandom){ //Om den valde samma random plats igen så ökar den bara random igen och om random är mer än antalet hål den kan hamna i resetar den till 0(vilket är starten på arrayen)/Kalle
                random++;             //Detta funkar inte riktigt och fattar inte varför /Kalle
                if(random > holesObject.Length){
                    random = 0;
                    randomPositionX = holesPositionX[random]; //väljer en random position i arrayen och rundar av det till en int /Kalle
                    randomPositionY = holesPositionY[random]; //
                }
            }else{ //Om random == en ny sifra än förra så placerar den bara ut den där /Kalle
                randomPositionX = holesPositionX[random]; //
                randomPositionY = holesPositionY[random]; //
            }
            
            newMole = Instantiate(moleObject,new Vector3(randomPositionX,randomPositionY,0),Quaternion.identity); //definerar newMole som ett nytt Instansiatat object /Kalle
            numberOfMolesAlive += 1;

            timeBetweenSpawn = 1f;
            lastRandom = random;
        }

        if(timeBetweenSpawn < -0.001f){

            //TODO Starta despawn animation

            //if mole death animation är över då kan de nedan ske
            Destroy(newMole);
            numberOfMolesAlive -= 1;
            life -= 10f;
        }

        if(Input.GetMouseButtonDown(0)){ 
            if(mousePosition.x + 0.5f >= randomPositionX && mousePosition.x - 0.5f <= randomPositionX && mousePosition.y + 0.5f >= randomPositionY && mousePosition.y - 0.5f <= randomPositionY && numberOfMolesAlive > 0){ //Om musen är ungefär ovanpå en mole så träffar den och förstör den /Kalle
                numberOfMolesAlive -= 1;
                Destroy(newMole);
                MolePoints();
                if(life < 100){
                    life += 10;
                }
                scoreText.text = gameScore.ToString();
                //TODO Kill animation
            }
        }
    }
}
