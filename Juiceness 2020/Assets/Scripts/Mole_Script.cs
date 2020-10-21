using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class Mole_Script : Score_System
{
    [SerializeField]
    GameObject mouseObject = null; //Reticle för musen
    
    [SerializeField]
    AudioSource gunSound = null; //Ljud komponenten för pistolen

    [SerializeField]
    PostProcessProfile postProcessProfile = null;

    [SerializeField]
    Text lifeText = null;

    #region Holes vars
    [SerializeField]
    GameObject moleObject = null;  //Prefab för moles /Kalle
    public GameObject[] holesObject = new GameObject[9]; //Platser där moles kan spawna /Kalle
    private float[] holesPositionX = new float[99];  //Hålens x position /Kalle
    private float[] holesPositionY = new float[99];  //Hålens y position /Kalle
    #endregion

    #region Klass variabler
    private int numberOfMolesAlive = 0; //Hur många moles som får vara vid liv samtidigt /Kalle
    private float timeBetweenSpawn = 1f; //Tid mellan varje spawn event /Kalle
    private float randomPositionY;
    private float randomPositionX;
    private int lastRandom = 999; //Måste vara global för annars överskrivs den varje frame med fel värde /Kalle
    private GameObject newMole;
    private Animator moleAnims = null;
    private Vignette vignetteLayer = null;

    private AudioSource popEffect = null;
    public float life;
    
    #endregion
    void Awake()
    {
        Cursor.visible = false; //Gör musen osynlig
        popEffect = GetComponent<AudioSource>(); 
        life = 100;
        lifeText.text = "Life:" + life;
        postProcessProfile = FindObjectOfType<PostProcessProfile>();
        postProcessProfile.TryGetSettings(out vignetteLayer);

        for (int i = 0; i < Mathf.Min(holesObject.Length, holesPositionX.Length); i++) //Bestämer x och y värden i deras respektive array baserat på hålens position /Kalle
        {
            holesPositionX[i] = holesObject[i].transform.position.x;
            holesPositionY[i] = holesObject[i].transform.position.y;
        }
    }
    
    void Update()
    {
        moleAnims = FindObjectOfType<Animator>();
        
        Vector3 mousePosition = Input.mousePosition;                                                        //Allt detta är för att
        mousePosition.z = 10f; //Just Z axis so its getting the position infront of the camera              //spara musens position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //Gets mouse postion in world space  //för att sedan ändra transformen
        mouseObject.transform.position = mousePosition; //Object following the mouse position               //på ett gameobject så att den följer musen 

        timeBetweenSpawn -= 1*Time.deltaTime;

        if(timeBetweenSpawn <= 0f && numberOfMolesAlive < 1){
           
            int random = Random.Range(0,holesObject.Length);
            if(random == lastRandom){ //Om den valde samma random plats igen så ökar den bara random igen och om random är mer än antalet hål den kan hamna i resetar den till 0(vilket är starten på arrayen)/Kalle
                random++;             //Detta funkar inte riktigt och fattar inte varför /Kalle
                if(random > holesObject.Length){
                    random = 0;
                    randomPositionX = holesPositionX[random]; //väljer en random position i arrayen och rundar av det till en int /Kalle
                    randomPositionY = holesPositionY[random];
                }
            }else{ //Om random == en ny sifra än förra så placerar den bara ut den där /Kalle
                randomPositionX = holesPositionX[random];
                randomPositionY = holesPositionY[random];
            }
            
            newMole = Instantiate(moleObject,new Vector3(randomPositionX,randomPositionY + 0.3f,0),Quaternion.identity); //definerar newMole som ett nytt Instansiatat object /Kalle
            numberOfMolesAlive += 1;                                                                                     // Den placerar också den newMole på dess förbestämda random position /Kalle

            timeBetweenSpawn = 1f;
            lastRandom = random;
        }

        if(timeBetweenSpawn < -0.001f){ //Om mole lever för länge /Kalle
            moleAnims.SetBool("Has_Despawned", true); //Startar en animation /Kalle
            if(moleAnims.GetCurrentAnimatorStateInfo(0).IsName("Moles_Despawn_Anim") && moleAnims.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){ //När animation är klar 
                Destroy(newMole);
                numberOfMolesAlive -= 1;
                life -= 10f; //Spelaren tar skada /Kalle
               // vignetteLayer.intensity.value = life/10;
                lifeText.text = "Life: " + life;
            }
        }

        if(Input.GetMouseButtonDown(0)){ 
            gunSound.Play();
            if(mousePosition.x + 0.5f >= randomPositionX && mousePosition.x - 0.5f <= randomPositionX && mousePosition.y + 0.5f >= randomPositionY && mousePosition.y - 0.5f <= randomPositionY && numberOfMolesAlive > 0){ 
                //Om musen är ungefär ovanpå en mole så träffar den och förstör den /Kalle
                scoreText.text = gameScore.ToString();
                moleAnims.SetBool("Has_Been_Shot", true);
                popEffect.Play();
            }
        }

        if(moleAnims != null){ //I och med att jag letar efter den nya molen varje frame så finns den inte hela tiden så enbart om den har en animator riktad till sig så kan den utföra koden nedan /Kalle
            if(moleAnims.GetCurrentAnimatorStateInfo(0).IsName("Mole_Death_Anim") && moleAnims.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){ //När Mole_Death_anim är slut så får den gå vidare /Kalle 
                numberOfMolesAlive -= 1;
                Destroy(newMole);
                MolePoints();
                if(life < 100){
                    life += 5;
                    lifeText.text = "Life: "+ life;
                //    vignetteLayer.intensity.value = life/10;
                }
            }
        } //Frågar man den om den ej var null så får man errors /Kalle
    }
}
