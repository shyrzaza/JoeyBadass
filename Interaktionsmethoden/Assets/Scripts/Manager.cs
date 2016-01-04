using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Manager : MonoBehaviour {
   
   private static Manager Instance;

   //ui
   public string name = "";
   public int port = 0;
   public  bool advancedon = false;

   //[SerializeField]
   private List<Highscore> scorelist = new List<Highscore>();

   [Serializable]
   public struct Highscore{
      public string playername;
      public int time;
      public Highscore(string p , int t){
         playername = p;
         time = t;
      }
   };
   
   public static Manager getInstance()
   {
      return Instance;
   }

  
   public void Awake()
   {
      // First we check if there are any other instances conflicting
      if (Instance != null && Instance != this)
      {
         // If that is the case, we destroy other instances
         Destroy(gameObject);
      }

      // Here we save our singleton instance
      Instance = this;

      // Furthermore we make sure that we don't destroy between scenes (this is optional)
      DontDestroyOnLoad(gameObject);
      Manager.getInstance().scoreListToText();
   }

   public void OnLevelWasLoaded()
   {
      if (Application.loadedLevel == 0)
      {
         Debug.Log("Updating score");
         scoreListToText();
         GameObject inputFieldGo = GameObject.FindGameObjectWithTag("port");
         InputField port = inputFieldGo.GetComponent<InputField>();
         port.text = this.port.ToString();
      }
   }

   public void ChangeScene(int sceneindex)
   {
      Application.LoadLevel(sceneindex);

   }

 
   //for other class to get bool
   public bool IsAdvancedOn()
   {
      return advancedon;
   }


   #region Highscore handling
   //adds new Score to list, takes name which was given in main menu
   public void addnewScore(int timeinsec)
   {
      int i=0;
      while(scorelist.Count > i){
         while(timeinsec < scorelist[i].time){
            i++;
         }
      }
      scorelist.Insert(i, new Highscore(name, timeinsec));
   }

   //updates highscore list only in main menu!!!!!
   private void scoreListToText()
   {
      string s = "";
      foreach (Highscore h in scorelist){
         s += (h.playername + ": " + h.time.ToString() + "\n");
      }
      GameObject inputFieldGo = GameObject.FindGameObjectWithTag("highscore");
      Text highscoreText = inputFieldGo.GetComponent<Text>();
      highscoreText.text = s;
   }

   //
   public void saveHighscore()
   {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Create(Application.persistentDataPath + "/highscore.dat");
      bf.Serialize(file, scorelist);
      file.Close();
   }

   //loads highscore if existant
   public void loadHighscore()
   {
      if (File.Exists(Application.persistentDataPath + "/highscore.dat"))
      {
         BinaryFormatter bf = new BinaryFormatter();
         FileStream file = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.Open);
         scorelist = (List<Highscore>) bf.Deserialize(file);
         file.Close();
      }
   }

   #endregion
}