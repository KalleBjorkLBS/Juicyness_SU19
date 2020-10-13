using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Save_Script : MonoBehaviour
{
    public int[] currentHighScoresInt;
    public string[] currentHighScoresString;
    public void SaveScore(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/HighScores.save");
        Save save = new Save();
        
        for (int i = 0; i < currentHighScoresInt.Length; i++)
        {
            save.highScoresInt[i] = currentHighScoresInt[i];
            save.highScoreString[i] =  currentHighScoresString[i]; 
        }

        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Highscore saved");
    }

    public void LoadGame(){
        if(File.Exists(Application.persistentDataPath + "/HighScores.save")){

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/HighScore.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < currentHighScoresInt.Length; i++)
            {
                currentHighScoresInt[i] = save.highScoresInt[i];
                currentHighScoresString[i] = save.highScoreString[i];
            }
        }
    }

    private void Awake() {
        LoadGame();
    }


}
