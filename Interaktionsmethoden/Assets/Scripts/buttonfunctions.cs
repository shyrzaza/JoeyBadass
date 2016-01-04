using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonfunctions : MonoBehaviour {
   public InputField portobj;
   public InputField nameobj;
   public Text highscoreText;

   public void ChangeScene(int sceneindex)
   {
      Application.LoadLevel(sceneindex);

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
         GameObject inputFieldGo = GameObject.FindGameObjectWithTag("highscore");
         highscoreText = inputFieldGo.GetComponent<Text>();
      }
      return highscoreText;
   }
  

   #region UIhandling
   //for toggle
   public void ToggleAdvancedModeChange()
   {
      Manager.getInstance().advancedon = !Manager.getInstance().advancedon;
   }

   //for input field
   public void NameChanged()
   {

      Manager.getInstance().name = getnameobj().text;
   }

   //for input field
   public void PortChanged()
   {
      int x;
      int.TryParse(getportobj().text, out x);
      Manager.getInstance().port = x;
      getportobj().text = x.ToString();
   }

   //for button
   public void EndGame()
   {
      Manager.getInstance().saveHighscore();
      Application.Quit();
   }
   #endregion 

}
