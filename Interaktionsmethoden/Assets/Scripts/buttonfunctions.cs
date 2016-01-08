using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonfunctions : MonoBehaviour {
   
   public InputField portobj;
   public InputField nameobj;
   public Text highscoreTextobj;
   public Toggle advancedobj;

   public void Start()
   {
      Manager.getInstance().onSceneOne();
   }
   public void ChangeScene(int sceneindex)
   {
      Debug.Log("Changing to scene " + sceneindex);
      Application.LoadLevel(sceneindex);
   }

   public void updateMenu(string name, int port, bool advancedon, string highscoretext)
   {
      Debug.Log("Updating UI in Level " + Application.loadedLevel + ", advanced: " + advancedon);
      nameobj.text = name;
      portobj.text = port.ToString();
      advancedobj.isOn = advancedon; // may not work correctly
      highscoreTextobj.text = highscoretext;
   }

   #region UIhandling
   //for toggle
   public void ToggleAdvancedModeChange()
   {
       Manager.getInstance().advancedon = !Manager.getInstance().advancedon;
   }

   public void ToggleControllerModeChange()
   {
       Manager.getInstance().controller = !Manager.getInstance().controller;
   }

   //for input field
   public void NameChanged()
   {

      Manager.getInstance().name = nameobj.text;
   }

   //for input field
   public void PortChanged()
   {
      int x;
      int.TryParse(portobj.text, out x);
      Manager.getInstance().port = x;
		Debug.Log ("port changed to " + x);
      portobj.text = x.ToString();
   }

   //for button
   public void resetHighscore()
   {
      Manager.getInstance().cleanScoreFile();
      Manager.getInstance().onSceneOne();
   }


   //for button
   public void EndGame()
   {
      Application.Quit();
   }
   #endregion 

}
