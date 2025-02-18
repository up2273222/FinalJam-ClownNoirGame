using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public GameObject MainButtons;
   public GameObject SettingsButtons;
   public void ExitGame()
   {
      Application.Quit();
      print("Quit");
   }

   public void PlayGame()
   {
      
      CameraManager.Instance.enabled = true;
      UIManager.Instance.enabled = true;
      GameManager.Instance.enabled = true;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void OpenSettings()
   {
      SettingsButtons.SetActive(true);
      MainButtons.SetActive(false);
   }

   public void CloseSettings()
   {
    SettingsButtons.SetActive(false);
    MainButtons.SetActive(true);
   }


}
