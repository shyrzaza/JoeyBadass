using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Manager : MonoBehaviour
{

   private static Manager instance;

   //ui
   public string name = "";
   public int port = 0;
   public bool advancedon = false;
   public string highscoretext = "";
   public bool controller = false;

   //[SerializeField]
   private List<Highscore> scorelist = new List<Highscore>();

   [Serializable]
   public struct Highscore
   {
      public string playername;
      public float time;
      public Highscore(string p, float t)
      {
         playername = p;
         time = t;
      }
   };


   public static Manager getInstance()
   {
      if (instance == null)
      {
         Debug.Log("Creating Manager");
         GameObject manager = new GameObject("[GameManager]");
         instance = manager.AddComponent<Manager>();
         manager.AddComponent<AudioSource>();
         manager.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Pixelland");
         manager.GetComponent<AudioSource>().loop = true;
         manager.GetComponent<AudioSource>().Play();
         DontDestroyOnLoad(manager);
      }
      return instance;
   }


   //if scene one has just been opened
   public void onSceneOne()
   {
      if (Application.loadedLevel == 0)
      {
         Debug.Log("Loading score");
         loadHighscore();
         scoreListToText();
         GameObject.FindObjectOfType<buttonfunctions>().updateMenu(name, port, controller,advancedon, highscoretext);
      }
   }

   //for other class to get bool
   public bool IsAdvancedOn()
   {
      return advancedon;
   }


   #region Highscore handling
   //adds new Score to list, takes name which was given in main menu
   public void addnewScore(float timeinsec)
   {
      Debug.Log("Adding new score");
      int i = 0;
      while (scorelist.Count > i)
      {
         if (timeinsec < scorelist[i].time)
         {
            i++;
         }
         else
         {
            break;
         }
      }
     
      scorelist.Insert(i, new Highscore(name, timeinsec));
   }

   //updates highscoretext from list
   private void scoreListToText()
   {
      string s = "";
      foreach (Highscore h in scorelist)
      {
         s += (h.playername + ": " + h.time.ToString("#.##") + " seconds\n");
      }
      highscoretext = s;
   }

   //
   public void saveHighscore()
   {
      Debug.Log("Saving highscore...");
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
         Debug.Log("Loading highscore...");
         BinaryFormatter bf = new BinaryFormatter();
         try
         {
            FileStream file = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.Open);
            scorelist = (List<Highscore>)bf.Deserialize(file);
            file.Close();
         }
         catch (Exception e)
         {
            Debug.Log("An error occured while loading the highscore. File could not be read.");
         }
      }
   }

   public void cleanScoreFile()
   {
      File.Delete(Application.persistentDataPath + "/highscore.dat");
      scorelist.Clear();
   }

  /* public void saveToPrefs()
   {
      Debug.Log("saved");
      PlayerPrefs.SetInt("highscorev" + PlayerPrefs.GetInt("count") + 1, scorelist[0].time);
      PlayerPrefs.SetString("highscoren" + PlayerPrefs.GetInt("count") + 1, scorelist[0].playername);
      PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);
   }
   */
   #endregion
}