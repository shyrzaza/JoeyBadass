using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Manager : MonoBehaviour {
   private InputField portobj;
   private InputField nameobj;
   private Text highscoreText;
   private static Manager Instance;
   private bool advancedon = false;
   private string name = "";
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

   private InputField getnameobj()
   {
      if (nameobj == null)
      {
         GameObject inputFieldGo = GameObject.FindGameObjectWithTag("name");
         nameobj = inputFieldGo.GetComponent<InputField>();
      }
      return nameobj;
   }

   private InputField getportobj()
   {
      if (portobj == null)
      {
         GameObject inputFieldGo = GameObject.FindGameObjectWithTag("port");
         portobj = inputFieldGo.GetComponent<InputField>();
      }
      return portobj;
   }

   private Text getscoretext()
   {
      if (highscoreText == null)
      {
         Debug.Log("level when searching" +Application.loadedLevel);
         GameObject inputFieldGo = GameObject.FindGameObjectWithTag("highscore");
         Debug.Log(inputFieldGo);
         highscoreText = inputFieldGo.GetComponent<Text>();
      }
      return highscoreText;
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
   }


   public void ChangeScene(int sceneindex)
   {
      Application.LoadLevel(sceneindex);

   }

   #region UIhandling
   //for toggle
   public void ToggleAdvancedModeChange()
   {
      advancedon = !advancedon;
   }

   //for input field
   public void NameChanged()
   {

      name = getnameobj().text;
   }

   //for input field
   public void PortChanged()
   {
      int x ;
      int.TryParse(getportobj().text, out x);
      getportobj().text = x.ToString();      
   }

   //for button
   public void EndGame()
   {
      saveHighscore();
      Application.Quit();
   }
   #endregion 

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

   //updates highscore list
   public void scoreListToText()
   {
      Debug.Log(Application.loadedLevel);
      string s = "";
      foreach (Highscore h in scorelist){
         s += (h.playername + ": " + h.time.ToString() + "\n");
      }
      Debug.Log(Application.loadedLevel);
      Debug.Log(highscoreText);
      getscoretext().text = s;
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