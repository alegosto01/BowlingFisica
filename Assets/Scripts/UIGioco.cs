using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIGioco : MonoBehaviour
{   
   // public GameObject TV;
   // public Text TVText;
    public GameObject Sfera;
    public Text[] ParametriTiroPlayMode;
    public GameObject ParametriTiroFather;
    public GeneraleTestMode AccGeneraleTestmode;
    public Generale AccGenerale;
    public InputField[] InputFieldParametri;
    public Text[] ParametriTiroTestMode;
    public GameObject ElencoButtonTiriTestmode;
    
    private void Start()
    {
       
        
    }
    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0) && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.y > Screen.height / 4)
        {
            if (AccGeneraleTestmode.enabled)
            {
                foreach (InputField input in InputFieldParametri)
                {
                    input.gameObject.SetActive(true);
                }
                foreach (Text Parametro in ParametriTiroTestMode)
                {
                    Parametro.gameObject.SetActive(false);
                }
                foreach (Transform ButtonTiro in ElencoButtonTiriTestmode.transform)
                {
                    ButtonTiro.GetComponent<Image>().color = Color.white;
                    ButtonTiro.GetComponentInChildren<Text>().color = Color.white;
                }
            }
            else
            {
                ParametriTiroFather.SetActive(false);

            }

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 && Input.GetTouch(0).position.y > Screen.height / 4)
        {
            if (AccGeneraleTestmode.enabled)
            {
                foreach (InputField input in InputFieldParametri)
                {
                    input.gameObject.SetActive(true);
                }
                foreach (Text Parametro in ParametriTiroTestMode)
                {
                    Parametro.gameObject.SetActive(false);
                }
                foreach (Transform ButtonTiro in ElencoButtonTiriTestmode.transform)
                {
                    ButtonTiro.GetComponent<Image>().color = Color.white;
                    ButtonTiro.GetComponentInChildren<Text>().color = Color.white;
                }
            }
            else
            {
                ParametriTiroFather.SetActive(true);
            }
            /*
            
            int i;
            print(ElencoButtonTiriTestmode.transform.childCount);
            for(i = 0;i < ElencoButtonTiriTestmode.transform.childCount;i++)
            {
                ElencoButtonTiriTestmode.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                ElencoButtonTiriTestmode.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;

            }*/
        }


    }
   
    public void LanciaTestMode()
    {
        AccGeneraleTestmode.LanciaTestMode();


    }
    public void Restart()
    {
        SceneManager.LoadScene("Bowling");
    }
    public void ParametroTiro(int Numero) // quando clicchi il button di un tiro in playmode
    {
        ParametriTiroFather.SetActive(false);
        
        ParametriTiroFather.SetActive(true);
        ParametriTiroPlayMode[0].text = "POSIZIONE INIZIALE = " + AccGenerale.ParametroPosizioneIniziale[Numero].ToString("F2") + " m";
        ParametriTiroPlayMode[1].text = "DIREZIONE = " + AccGenerale.ParametroDirezione[Numero].ToString("F0") + "°";
        ParametriTiroPlayMode[2].text = "ANGOLAZIONE = " + AccGenerale.ParametroAngolazione[Numero].ToString("F0") + "°";
        ParametriTiroPlayMode[3].text = "ROTAZIONE = " + AccGenerale.ParametroRotazione[Numero].ToString("F0") + "°";
        ParametriTiroPlayMode[4].text = "FORZA = " + AccGenerale.ParametroForza[Numero].ToString("F0") + "°";
        ParametriTiroPlayMode[5].text = "ALTEZZA = " + AccGenerale.ParametroAltezza[Numero].ToString("F2") + " m";
    }

    // Tutti le 6 funzioni sotto assegnano i valori digitati dall utente nel array Parametri[]
    public void InputParametroPosIniziale(int i) 
    {
        float FloatAusilio;
        float.TryParse(InputFieldParametri[i].text, out FloatAusilio);
        //AccGeneraleTestmode.ParametroPosizioneIniziale.Add(FloatAusilio);
        AccGeneraleTestmode.Parametri[0] = FloatAusilio;
        AccGeneraleTestmode.SferaFather.transform.position = new Vector3(0,AccGeneraleTestmode.transform.position.y,0) + new Vector3(FloatAusilio,0 , 0);

    }
    public void InputParametroDirezione(int i)
    {
        float FloatAusilio;
        float.TryParse(InputFieldParametri[i].text, out FloatAusilio);
        //AccGeneraleTestmode.ParametroDirezione.Add(FloatAusilio);
        AccGeneraleTestmode.Parametri[1] = FloatAusilio;

    }
    public void InputParametroAngolazione(int i)
    {
        float FloatAusilio;
        float.TryParse(InputFieldParametri[i].text, out FloatAusilio);
        //AccGeneraleTestmode.ParametroAngolazione.Insert(AccGeneraleTestmode.NumeroTiroInElenco, FloatAusilio);
        AccGeneraleTestmode.Parametri[2] = FloatAusilio;

    }
    public void InputParametroRotazione(int i)
    {
        float FloatAusilio;
        float.TryParse(InputFieldParametri[i].text, out FloatAusilio);
        //AccGeneraleTestmode.ParametroRotazione.Add(FloatAusilio);
        AccGeneraleTestmode.Parametri[3] = FloatAusilio;

    }
    public void InputParametroForza(int i)
    {
        float FloatAusilio;
        float.TryParse(InputFieldParametri[i].text, out FloatAusilio);
        //AccGeneraleTestmode.ParametroForza.Add(FloatAusilio);
        AccGeneraleTestmode.Parametri[4] = FloatAusilio;

    }
    public void InputParametroAltezza(int i)
    {
        float FloatAusilio;
        float.TryParse(InputFieldParametri[i].text, out FloatAusilio);
        //AccGeneraleTestmode.ParametroAltezza.Add(FloatAusilio);
        AccGeneraleTestmode.Parametri[5] = FloatAusilio;
        AccGeneraleTestmode.SferaFather.transform.position = new Vector3(AccGeneraleTestmode.Sfera.transform.position.x,0,0) + new Vector3(0,FloatAusilio,0);

    }

   
    public void ParametriTiriTestMode(int index) // Funzione che si attiva quando clicchi un button di un tiro nell elenco dei tiri in testmode
    {

        if (InputFieldParametri[0].gameObject.activeSelf)
        {
            foreach (InputField input in InputFieldParametri)
            {
                input.gameObject.SetActive(false);
            }
            foreach (Text Parametro in ParametriTiroTestMode)
            {
                Parametro.gameObject.SetActive(true);
            }
        }
        ParametriTiroTestMode[0].text = PlayerPrefsX.GetFloatArray("PosizioneIniziale")[index].ToString();
        ParametriTiroTestMode[1].text = PlayerPrefsX.GetFloatArray("Direzione")[index].ToString();
        ParametriTiroTestMode[2].text = PlayerPrefsX.GetFloatArray("Angolazione")[index].ToString();
        ParametriTiroTestMode[3].text = PlayerPrefsX.GetFloatArray("Rotazione")[index].ToString();
        ParametriTiroTestMode[4].text = PlayerPrefsX.GetFloatArray("Forza")[index].ToString();
        ParametriTiroTestMode[5].text = PlayerPrefsX.GetFloatArray("Altezza")[index].ToString();
    }
    public void CancellaSalvataggiTestMode()
    {
        AccGeneraleTestmode.CancellaSalvataggiTestMode();
    }
    public void Cancella1SalvataggioTestMode()
    {
        AccGeneraleTestmode.Cancella1SalvataggioTestMode();
    }
}





    /* motodi prima di usare il padre per la telecamera
     * 
     * 
    public IEnumerator ResettaParametriAnimSfera(int Tempo)
    {
        yield return new WaitForSeconds(Tempo);
        AnimMainCamera.SetBool("PosizioneIniziale", false);
        AnimMainCamera.SetBool("Altezza", false);
    }
    public IEnumerator Resetta1ParametroAnimSfera(float Tempo, string Parametro, bool Valore)
    {
        yield return new WaitForSeconds(Tempo);
        AnimSfera.SetBool(Parametro, Valore);
    }
    public void ImpostaValoriAnimCameraRotazione()
    {
        AnimCameraRotazione.ClearCurves();

        AnimationCurve CurveX;
        AnimationCurve CurveY;
        AnimationCurve CurveZ;
        AnimationCurve CurveXRot;
        AnimationCurve CurveYRot;
        AnimationCurve CurveZRot;
        AnimationCurve CurveWRot;

        Vector3 PosizioneIniziale = new Vector3(MainCamera.transform.position.x, 1, -7.5f);
        Vector3 PosizioneFinale = new Vector3(Sfera.transform.position.x, 0.1f, -7.2f);
        CurveX = new AnimationCurve();
        CurveY = new AnimationCurve();
        CurveZ = new AnimationCurve();

        CurveX.AddKey(0, MainCamera.transform.position.x);
        CurveX.AddKey(1, Sfera.transform.position.x);
        CurveY.AddKey(0, PosizioneIniziale.y);
        CurveY.AddKey(1, PosizioneFinale.y);
        CurveZ.AddKey(0, PosizioneIniziale.z);
        CurveZ.AddKey(1, PosizioneFinale.z);

        AnimCameraRotazione.SetCurve("", typeof(Transform), "localPosition.x", CurveX);
        AnimCameraRotazione.SetCurve("", typeof(Transform), "localPosition.y", CurveY);
        AnimCameraRotazione.SetCurve("", typeof(Transform), "localPosition.z", CurveZ);


        Quaternion RotazioneFinale = Quaternion.Euler(0.1f, 0, 0);
        CurveXRot = new AnimationCurve();
        CurveYRot = new AnimationCurve();
        CurveZRot = new AnimationCurve();
        CurveWRot = new AnimationCurve();

        CurveXRot.AddKey(0, MainCamera.transform.rotation.x);
        CurveXRot.AddKey(1, RotazioneFinale.x);
        CurveYRot.AddKey(0, MainCamera.transform.rotation.y);
        CurveYRot.AddKey(1, RotazioneFinale.y);
        CurveZRot.AddKey(0, MainCamera.transform.rotation.z);
        CurveZRot.AddKey(1, RotazioneFinale.z);
        CurveWRot.AddKey(0, MainCamera.transform.rotation.w);
        CurveWRot.AddKey(1, RotazioneFinale.w);

        AnimCameraRotazione.SetCurve("", typeof(Transform), "localRotation.x", CurveXRot);
        AnimCameraRotazione.SetCurve("", typeof(Transform), "localRotation.y", CurveYRot);
        AnimCameraRotazione.SetCurve("", typeof(Transform), "localRotation.z", CurveZRot);
        AnimCameraRotazione.SetCurve("", typeof(Transform), "localRotation.w", CurveWRot);
    }
   
    public void ImpostaValoriAnimSferaAltezza() // si usa anche per RotazioneRev
    {
        //AnimCameraTorna.ClearCurves();

        AnimationCurve CurveX;
        AnimationCurve CurveY;
        AnimationCurve CurveZ;

        Vector3 PosizioneIniziale = new Vector3(Sfera.transform.position.x, Sfera.transform.position.y, Sfera.transform.position.z);
        Vector3 PosizioneFinale = new Vector3(Sfera.transform.position.x, 1, -6);

        CurveX = new AnimationCurve();
        CurveY = new AnimationCurve();
        CurveZ = new AnimationCurve();

        CurveX.AddKey(0, PosizioneIniziale.x);
        CurveX.AddKey(1, PosizioneFinale.x);
        CurveX.AddKey(2, PosizioneIniziale.x);

        CurveY.AddKey(0, PosizioneIniziale.y);
        CurveY.AddKey(1, PosizioneFinale.y);
        CurveY.AddKey(2, PosizioneIniziale.y);

        CurveZ.AddKey(0, PosizioneIniziale.z);
        CurveZ.AddKey(1, PosizioneFinale.z);
        CurveZ.AddKey(2, PosizioneIniziale.z);


        AnimSferaAltezza.SetCurve("", typeof(Transform), "localPosition.x", CurveX);
        AnimSferaAltezza.SetCurve("", typeof(Transform), "localPosition.y", CurveY);
        AnimSferaAltezza.SetCurve("", typeof(Transform), "localPosition.z", CurveZ);

    }
    public void ImpostaValoriAnimSferaFerma() // si usa anche per RotazioneRev
    {
        //AnimCameraTorna.ClearCurves();

        AnimationCurve CurveX;
        AnimationCurve CurveY;
        AnimationCurve CurveZ;

        Vector3 PosizioneIniziale = new Vector3(Sfera.transform.position.x, Sfera.transform.position.y, Sfera.transform.position.z);

        CurveX = new AnimationCurve();
        CurveY = new AnimationCurve();
        CurveZ = new AnimationCurve();

        CurveX.AddKey(0, PosizioneIniziale.x);
        CurveX.AddKey(1, PosizioneIniziale.x);

        CurveY.AddKey(0, PosizioneIniziale.y);
        CurveY.AddKey(1, PosizioneIniziale.y);

        CurveZ.AddKey(0, PosizioneIniziale.z);
        CurveZ.AddKey(1, PosizioneIniziale.z);


        AnimSferaFerma.SetCurve("", typeof(Transform), "localPosition.x", CurveX);
        AnimSferaFerma.SetCurve("", typeof(Transform), "localPosition.y", CurveY);
        AnimSferaFerma.SetCurve("", typeof(Transform), "localPosition.z", CurveZ);

    }*/


