using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public Text LivelloCPUTXT;
    public int LivelloCPU = 0;
    public Generale AccGenerale;
    public GeneraleTestMode AccGeneraleTestMode;
   
    

    public void AumentaLivelloCPU()
        
    {
        if (LivelloCPU < 5)
        {
            LivelloCPU++;
            LivelloCPUTXT.text = LivelloCPU.ToString();
        }

    }
    public void DiminuisciLivelloCPU()
    {
        if (LivelloCPU > 0)
        {
            LivelloCPU--;
            LivelloCPUTXT.text = LivelloCPU.ToString();
        }

    }
    public void TestMode()// Button Schermata Principale
    {
        AccGeneraleTestMode.AnimFatherCamera.enabled = false;
        AccGeneraleTestMode.Prova = true;
        AccGeneraleTestMode.enabled = true;
    }
    public void PlayMode() // Button Schermata Principale
    {
        AccGenerale.AnimFatherCamera.enabled = false;
        AccGenerale.Prova = true;
        AccGenerale.enabled = true;
    }
    public void Info()
    {
        Time.timeScale = 0.5f;
    }
    public void EsciInfo()
    {
        Time.timeScale = 1;
    }

}
