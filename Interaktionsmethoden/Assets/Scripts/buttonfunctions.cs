using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonfunctions : MonoBehaviour {
   
   public InputField portobj;
   public InputField nameobj;
   public Text highscoreTextobj;
   public Toggle advancedobj;
   public Toggle controllerobj;
   public GameObject Info;

   public void Start()
   {
      Manager.getInstance().onSceneOne();
   }
   public void ChangeScene(int sceneindex)
   {
      Debug.Log("Changing to scene " + sceneindex);
      Application.LoadLevel(sceneindex);
   }

   public void updateMenu(string name, int port, bool controlleron, bool advancedon, string highscoretext)
   {
      Debug.Log("Updating UI in Level " + Application.loadedLevel + ", advanced: " + advancedon);
      nameobj.text = name;
      portobj.text = port.ToString();
      controllerobj.isOn = controlleron;
      advancedobj.isOn = advancedon; 
      highscoreTextobj.text = highscoretext;
      ToggleControllerModeChange();
      
   }

   #region UIhandling
   //for toggle
   public void ToggleAdvancedModeChange()
   {
       Manager.getInstance().advancedon = advancedobj.isOn;

   }

   public void ToggleControllerModeChange()
   {     
       Manager.getInstance().controller = controllerobj.isOn;
       //if controller is switched of
       if (!controllerobj.isOn)
       {
          advancedobj.gameObject.SetActive(false);
          advancedobj.isOn = false;
          Manager.getInstance().advancedon = false;
       }
       else
       {
          advancedobj.gameObject.SetActive(true);
       }
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

   public void OnInfoPress()
   {
      Info.SetActive(true);
   }

   public void OnQuitInfo()
   {
      Debug.Log("Quit info");
      Info.SetActive(false);
   }
}
