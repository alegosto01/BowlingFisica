using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generale : MonoBehaviour
{
    public UIGioco AccUIGioco;
    public Animator AnimFrecciaForza;
    public Animator AnimFrecciaDirezione;
    public Animator AnimFrecciaAngolo;
    public Animator AnimMainCamera;
    public Animator AnimSfera;
    public Animator AnimFrecciaRotazione;
    public Animator AnimFatherCamera;
    public AnimationClip AnimCameraTorna;
    public GameObject FatherCamera;
    public GameObject FrecciaForza;
    public GameObject FrecciaDirezione;
    public GameObject FrecciaAngolo;
    public GameObject FrecceRotazione;
    public GameObject FrecciaIndicatoreRotazione;
    public GameObject IndicatoreForza;
    public float[] Parametri;
    public Text[] ParametriLiveTXT;
    public float TempoAttesa = 0;
    public int i = -1;
    public GameObject Sfera;
    public Rigidbody RigidBSfera;
    public GameObject MainCamera;
    public Vector3 RotazioneAlSecondo;
    //public PlayMode AccPlayMode;
    public GameObject SferaFather;
    public GameObject MainCameraFather;
    public TimeManager AccTimeManager;
    public bool Prova = false;
    public int FasiRaddoppio;
    public int[] PrimoTiro;
    public int[] SecondoTiro;
    public int[] Totali;
    public Text[] PrimoTiroTXT;
    public Text[] SecondoTiroTXT;
    public Text[] TotaliTXT;
    public bool PrimoLancio = true;
    public int Turno = 1;
    public List<int> LanciStrike = new List<int>();
    public List<int> LanciSpeare = new List<int>();
    public GameObject Birilli;
    public int BirilliCaduti = 0;
    public GameObject BirilloPrefab;
    public Vector3[] PosizioneBirilli = new Vector3[10];
    public GameObject[] BirilloNuovo;
    public float[] ParametroPosizioneIniziale;
    public float[] ParametroDirezione;
    public float[] ParametroAngolazione;
    public float[] ParametroRotazione;
    public float[] ParametroForza;
    public float[] ParametroAltezza;
    public int CaselleUtilizzate;
    public int TiriEffettuati;
    void Start()
    {
        
    }

    // Update is called once per frame



    private void FixedUpdate()

    {
        ParametriLive();


        if (TempoAttesa > 0)
        
        {
           TempoAttesa = TempoAttesa - Time.deltaTime;
        }
            else
            {
                SceltaParametri();
            }
            Quaternion deltaRotation = Quaternion.Euler(RotazioneAlSecondo * Time.deltaTime);
            RigidBSfera.MoveRotation(RigidBSfera.rotation * deltaRotation);
            /*if(Sfera.transform.position.z > 3f)    SLOW MOTION
            {
                AccTimeManager.RallentaTempo();
            }
            if(Sfera.transform.position.z > 12)
            {
                AccTimeManager.VelocizzaTempo();
            }*/
            if (Prova)
            {
                FatherCamera.transform.position = Vector3.Lerp(FatherCamera.transform.position, new Vector3(0, 0, 0), 0.1f);
                FatherCamera.transform.rotation = Quaternion.Lerp(FatherCamera.transform.rotation, new Quaternion(0, 0, 0, 1), 0.1f);
            }
        
        else
        {   
            if (Prova)
            {
                FatherCamera.transform.position = Vector3.Lerp(FatherCamera.transform.position, new Vector3(0, 0, 0), 0.1f);
                FatherCamera.transform.rotation = Quaternion.Lerp(FatherCamera.transform.rotation, new Quaternion(0, 0, 0, 1), 0.1f);
            }
        }
    }
    public void ParametriLive()
    {
        if (ParametriLiveTXT[0].text != " ")
        {
            ParametriLiveTXT[0].text = Sfera.transform.localPosition.x.ToString("F2") + " m";
        }
        if (ParametriLiveTXT[1].text != " ")
        {
            ParametriLiveTXT[1].text = (FrecciaDirezione.transform.localRotation.z * 100).ToString("F0") + "°";
        }
        if (ParametriLiveTXT[2].text != " ")
        {
            ParametriLiveTXT[2].text = (FrecciaAngolo.transform.localRotation.z * 100).ToString("F0") + "°";
        }
        if (ParametriLiveTXT[3].text != " ")
        {
            ParametriLiveTXT[3].text = (FrecciaIndicatoreRotazione.transform.localRotation.z * 100).ToString("F0") + "°";
        }
        if (ParametriLiveTXT[4].text != " ")
        {
            ParametriLiveTXT[4].text = (Mathf.Abs(FrecciaForza.transform.localRotation.z) * 100).ToString("F0") + " N";
        }
        if (ParametriLiveTXT[5].text != " ")
        {
            ParametriLiveTXT[5].text = Sfera.transform.localPosition.y.ToString("F2") + " m";
        }
    }
    public void SceltaPrecedente()
    {
        if (i == 2)
        {
            i--;
            FrecciaAngolo.SetActive(false);
            FrecciaDirezione.SetActive(true);
            AnimFrecciaDirezione.enabled = true;
            Parametri[i] = 0;
            //ParametriText[1].text = "DIREZIONE";
        }
        else if (i == 1)
        {
            i--;
            FrecciaDirezione.SetActive(false);
            FrecciaForza.SetActive(true);
            AnimFrecciaForza.enabled = true;
            IndicatoreForza.SetActive(true);
            Parametri[i] = 0;
           // ParametriText[0].text = "FORZA";
        }
    }
    public void FermaPosizionePalla()
    {
        //if (Input.GetMouseButton(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)
        //if (Input.GetKeyDown(KeyCode.A))
        {
            SferaFather.transform.position = new Vector3(Sfera.transform.localPosition.x, 0, 0);
            MainCameraFather.transform.position = new Vector3(Sfera.transform.localPosition.x, 0, 0);
            AnimSfera.SetBool("PosizioneIniziale", false);
            Parametri[0] = Sfera.transform.position.x;
           // ParametriText[0].text = ParametriText[0].text + " = " + Parametri[0].ToString();
            i++;
            TempoAttesa = 0.5f;
            FrecciaDirezione.SetActive(true);
            AnimMainCamera.SetBool("Direzione", true);
            ParametriLiveTXT[0].text = " ";
            ParametriLiveTXT[1].text = 0.ToString();
            ParametroPosizioneIniziale[CaselleUtilizzate] = Parametri[0];

        }
    }
    public void FermaFrecciaDirezione()
    {
        //if (Input.GetMouseButton(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)
        //if (Input.GetKeyDown(KeyCode.A))
        {
            AnimFrecciaDirezione.enabled = false;
            Parametri[1] = Mathf.RoundToInt(FrecciaDirezione.transform.localRotation.z * 100);
            //ParametriText[1].text = ParametriText[1].text + " = " + Mathf.RoundToInt(FrecciaDirezione.transform.localRotation.z * 100).ToString();
            i++;
            TempoAttesa = 0.5f;
            AnimMainCamera.SetBool("Angolazione", true);
            FrecciaDirezione.SetActive(false);
            FrecciaAngolo.SetActive(true);
            ParametriLiveTXT[1].text = " ";
            ParametriLiveTXT[2].text = 0.ToString();
            ParametroDirezione[CaselleUtilizzate] = Parametri[1];

        }
    }
    public void FermaFrecciaAngolo()
    {
        //if (Input.GetMouseButton(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)
        // if (Input.GetKeyDown(KeyCode.A))
        {

           // ParametriText[2].text = ParametriText[2].text + " = " + Mathf.Abs(Mathf.RoundToInt(FrecciaAngolo.transform.localRotation.z * 100)).ToString();
            i++;
            TempoAttesa = 0.5f;
            print(FrecciaAngolo.transform.rotation.z * 100);
            Parametri[2] = Mathf.RoundToInt(FrecciaAngolo.transform.rotation.z * 100);
            //StartCoroutine( OttieniValoreRotazioneAngolo());
            FrecceRotazione.SetActive(true);
            AnimMainCamera.SetBool("Rotazione", true);
            FrecciaAngolo.SetActive(false);
            ParametriLiveTXT[2].text = " ";
            ParametriLiveTXT[3].text = 0.ToString();
            ParametroAngolazione[CaselleUtilizzate] = Parametri[2];

        }
    }
    public void FermaFrecciaRotazione()
    {
        //if (Input.GetMouseButton(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)
        // if (Input.GetKeyDown(KeyCode.A))
        {
            AnimFrecciaRotazione.enabled = false;
            Parametri[3] = FrecciaIndicatoreRotazione.transform.localRotation.z;
            //ParametriText[3].text = ParametriText[3].text + " = " + (Parametri[3] * 100).ToString();
            i++;
            TempoAttesa = 0.5f;
            FrecceRotazione.SetActive(false);
            AnimMainCamera.SetBool("Forza", true);
            FrecciaForza.SetActive(true);
            IndicatoreForza.SetActive(true);
            ParametriLiveTXT[3].text = " ";
            ParametriLiveTXT[4].text = 0.ToString();
            ParametroRotazione[CaselleUtilizzate] = Parametri[3];

        }
    }
    public void FermaFrecciaForza()
    {
        // if (Input.GetMouseButton(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)
        // if (Input.GetKeyDown(KeyCode.A))
        {
            AnimFrecciaForza.enabled = false;
            Parametri[4] = 90 - Mathf.Abs(Mathf.RoundToInt(FrecciaForza.transform.localRotation.z * 100));
           // ParametriText[4].text = ParametriText[4].text + " = " + (90 - Mathf.Abs(Mathf.RoundToInt(FrecciaForza.transform.localRotation.z * 100))).ToString();
            i++;
            TempoAttesa = 0.5f;
            FrecciaForza.SetActive(false);
            IndicatoreForza.SetActive(false);
            AnimMainCamera.SetBool("Altezza", true);
            AnimSfera.SetBool("Altezza", true);
            print("FermaForza");
            //RigidBSfera.useGravity = false;
            ParametriLiveTXT[4].text = " ";
            ParametriLiveTXT[5].text = 0.ToString();
            ParametroForza[CaselleUtilizzate] = Parametri[4];

        }
    }
    public void FermaFrecciaAltezza()
    {
        // if (Input.GetMouseButton(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)
        // if (Input.GetKeyDown(KeyCode.A))
        {

            AnimFrecciaRotazione.enabled = false;
            Parametri[5] = Sfera.transform.position.y;

            AnimSfera.enabled = false;
            //SferaFather.transform.position = new Vector3(SferaFather.transform.position.x,Sfera.transform.localPosition.y, 0);
            RigidBSfera.constraints = RigidbodyConstraints.FreezePositionY;
            //SferaFather.transform.position = new Vector3(0, 0, 0);
            //ParametriText[5].text = ParametriText[5].text + " = " + Parametri[5].ToString();
            i++;
            TempoAttesa = 0.5f;
            FrecceRotazione.SetActive(false);
            print("FermaAltezza");
            AnimSfera.SetBool("Altezza", false);
            AnimMainCamera.SetBool("ProntoAlLancio", true);
            ParametriLiveTXT[5].text = " ";
            ParametroAltezza[CaselleUtilizzate] = Parametri[5];

        }
    }
    public void SceltaParametri()
    {
        if (i == -2 && (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true)) 

        {
            if (AnimFatherCamera.enabled == false)
            {
                AnimFatherCamera.enabled = false;
                i++;
                TempoAttesa = 0.5f;
            }
           




            //Prova = true;                          L HO PROVATO A TOGLIERE XK MI SA CHE NON SERVE
            //StartCoroutine(Resetta1ParametroAnimCamera("Panorama", false));
            //StartCoroutine(Resetta1ParametroAnimCamera("Comincia", true));
            //ImpostaValoriAnimCameraTorna();
            //StartCoroutine(ResettaParametriAnimCamera(1));
        }
        if (i == -1 && (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true) )
        {
            //Resetta1ParametroAnimCamera(0.00001f, "PosizioneIniziale", true);
            Prova = false;
            AnimMainCamera.SetBool("PosizioneIniziale", true);
            AnimSfera.SetBool("PosizioneIniziale", true);

            i++;
            TempoAttesa = 0.5f;
            ParametriLiveTXT[0].text = 0.ToString();
        }
        else if (i == 0)
        {
            FermaPosizionePalla();
           

        }
        else if (i == 1)
        {
            FermaFrecciaDirezione();
            
        }
        else if (i == 2)
        {
            FermaFrecciaAngolo();
            
        }
        else if (i == 3)
        {
            FermaFrecciaRotazione();
          
        }
        else if (i == 4)
        {
            FermaFrecciaForza();
           
        }
        else if (i == 5)
        {
            FermaFrecciaAltezza();
            AnimMainCamera.SetBool("Direzione", false);
            AnimMainCamera.SetBool("PosizioneIniziale", false);
            AnimMainCamera.SetBool("Angolazione", false);
            AnimMainCamera.SetBool("Rotazione", false);
            AnimMainCamera.SetBool("Forza", false);
            AnimMainCamera.SetBool("Altezza", false);
            AnimMainCamera.SetBool("Panorama", false);
        }
        else if (i == 6 && (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true))
        {
            StartCoroutine(LanciaEnum());
            i++;
        }
        else if (i == 7 && (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height / 2 || Input.GetKeyDown(KeyCode.A) == true))
        {
            //
        }

    }

    public void Lancia()
    {
        StartCoroutine(LanciaEnum());

    }
    public IEnumerator LanciaEnum()
    {
        CaselleUtilizzate++;
        TiriEffettuati++;
        //yield return new WaitForSeconds(2);
        AnimMainCamera.SetBool("ProntoAlLancio", false);
        AnimMainCamera.enabled = false;
        RigidBSfera.constraints = RigidbodyConstraints.None;
        //RigidBSfera.useGravity = true;
        RotazioneAlSecondo = new Vector3(0, 0, FrecciaIndicatoreRotazione.transform.localRotation.z * 100);
        //print((new Vector3(-Mathf.Sin((Parametri[1] * Mathf.PI) / 180), Mathf.Sin(((Parametri[2]) * Mathf.PI) / 180), Mathf.Cos(((Parametri[2]) * Mathf.PI) / 180)) * Parametri[4], ForceMode.VelocityChange));
        Physics.gravity = new Vector3(0, -9.81f, 0);
        RigidBSfera.AddForce(new Vector3(-Mathf.Sin((Parametri[1] * Mathf.PI) / 180), Mathf.Sin(((Parametri[2]) * Mathf.PI) / 180), Mathf.Cos(((Parametri[2]) * Mathf.PI) / 180)) * Parametri[4] , ForceMode.Impulse);

        yield return null;
        //yield return new WaitForSeconds(5);
        //GestionePunteggio();
    }
    public IEnumerator ResettaParametriAnimCamera(float Tempo)
    {
        yield return new WaitForSeconds(Tempo);
        AnimMainCamera.SetBool("Direzione", false);
        AnimMainCamera.SetBool("PosizioneIniziale", false);
        AnimMainCamera.SetBool("Angolazione", false);
        AnimMainCamera.SetBool("Rotazione", false);
        AnimMainCamera.SetBool("Forza", false);
        AnimMainCamera.SetBool("Altezza", false);
        AnimMainCamera.SetBool("Panorama", false);
        AnimMainCamera.SetBool("ProntoAlLancio", false);

    }
    public IEnumerator Resetta1ParametroAnimCamera(string Parametro, bool Valore)
    {
        float i = 0;
        while (i < 0.01f)
        {
            i += Time.deltaTime;
            yield return null;
        }
        AnimMainCamera.SetBool(Parametro, Valore);
        print("Ciao");
    }
    public void ImpostaValoriAnimCameraTorna() // si usa anche per RotazioneRev
    {
        //AnimCameraTorna.ClearCurves();

        AnimationCurve CurveX;
        AnimationCurve CurveY;
        AnimationCurve CurveZ;
        AnimationCurve CurveXRot;
        AnimationCurve CurveYRot;
        AnimationCurve CurveZRot;
        AnimationCurve CurveWRot;

        Vector3 PosizioneIniziale = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
        Vector3 PosizioneFinale = new Vector3(0, 1, -7.5f);

        CurveX = new AnimationCurve();
        CurveY = new AnimationCurve();
        CurveZ = new AnimationCurve();

        CurveX.AddKey(0, MainCamera.transform.position.x);
        CurveX.AddKey(1, Sfera.transform.position.x);
        CurveY.AddKey(0, PosizioneIniziale.y);
        CurveY.AddKey(1, PosizioneFinale.y);
        CurveZ.AddKey(0, PosizioneIniziale.z);
        CurveZ.AddKey(1, PosizioneFinale.z);

        AnimCameraTorna.SetCurve("", typeof(Transform), "localPosition.x", CurveX);
        AnimCameraTorna.SetCurve("", typeof(Transform), "localPosition.y", CurveY);
        AnimCameraTorna.SetCurve("", typeof(Transform), "localPosition.z", CurveZ);


        Quaternion RotazioneFinale = Quaternion.Euler(8, 0, 0);

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

        AnimCameraTorna.SetCurve("", typeof(Transform), "localRotation.x", CurveXRot);
        AnimCameraTorna.SetCurve("", typeof(Transform), "localRotation.y", CurveYRot);
        AnimCameraTorna.SetCurve("", typeof(Transform), "localRotation.z", CurveZRot);
        AnimCameraTorna.SetCurve("", typeof(Transform), "localRotation.w", CurveWRot);
       
    }
    public IEnumerator OttieniValoreRotazioneAngolo()
    {
        yield return new WaitForSeconds(0.3f);
        Parametri[2] = Mathf.RoundToInt(FrecciaAngolo.transform.rotation.z * 100);
        FrecciaAngolo.SetActive(false);
    }
    public IEnumerator DaPanoramaANormale()
    {
        while(FatherCamera.transform.position != new Vector3(0,0,0))
        {
            yield return null;
        }
    }
    public void GestionePunteggio()
    {
        ConteggioBirilli();
        
       
        
        int f;
        for (f = 0; f < 2; f++) // aggiunge al punteggio totale degli strike precendenti i birilli caduti in questo turno 
        { 
            if (LanciStrike.Count > f)
            {
                LanciStrike.RemoveAt(f);
                Totali[Turno - (f + 2)] += BirilliCaduti;
                LanciStrike.Insert(f, Turno - (f + 2));
                TotaliTXT[Turno - (f + 2)].text = Totali[Turno - (f + 2)].ToString();
            }
           
        }

        if (LanciSpeare.Count == 1) // aggiunge al punteggio totale degli speare precendenti i birilli caduti in questo turno 
        {
            LanciSpeare.RemoveAt(0);
            Totali[Turno - 2] += BirilliCaduti;
            //LanciStrike.Insert(0, Totali[Turno - 2]);
            TotaliTXT[Turno - 2].text = Totali[Turno - 2].ToString();
        }


        if (PrimoLancio == true)
        {
            PrimoTiro[Turno - 1] = BirilliCaduti;
            PrimoTiroTXT[Turno - 1].text = PrimoTiro[Turno - 1].ToString();
          /*  if (FasiRaddoppio > 2)
            {
                PrimoTiro[Turno - 1] *= 3;
            }
            else if (FasiRaddoppio > 0)
            {
                PrimoTiro[Turno - 1] *= 2;
            }
            if (BirilliCaduti == 10)
            {
                FasiRaddoppio = FasiRaddoppio + 2;
            }*/

        }
        if(PrimoLancio == false)
        {
            SecondoTiro[Turno - 1] = BirilliCaduti;
            SecondoTiroTXT[Turno - 1].text = SecondoTiro[Turno - 1].ToString();
           /* if (FasiRaddoppio > 2)
            {
                PrimoTiro[Turno - 1] *= 3;
            }
            else if (FasiRaddoppio > 0)
            {
                SecondoTiro[Turno - 1] *= 2;
            }
            if (PrimoTiro[Turno - 1] + SecondoTiro[Turno - 1] == 10)
            {
                FasiRaddoppio = FasiRaddoppio + 1;
            }*/
        }
        if(PrimoTiro[Turno - 1] == 10) // strike
        {
            LanciStrike.Insert(0, Turno - 1);
            Totali[Turno - 1] = Totali[Turno - 1] + BirilliCaduti;
            TotaliTXT[Turno - 1].text = Totali[Turno - 1].ToString();

            CaselleUtilizzate++;
        }
        else if(PrimoTiro[Turno - 1] < 10 && PrimoTiro[Turno - 1] + SecondoTiro[Turno - 1] != 10) // tiro normale nè speare nè strike
        {
            Totali[Turno - 1] = PrimoTiro[Turno - 1] + SecondoTiro[Turno - 1];
            TotaliTXT[Turno - 1].text = Totali[Turno - 1].ToString();
            PrimoLancio = false;
        }
        else if(PrimoTiro[Turno - 1] < 10 && PrimoTiro[Turno - 1] + SecondoTiro[Turno - 1] == 10) // speare
        {
            LanciSpeare.Insert(0, Turno - 1);
            Totali[Turno - 1] = Totali[Turno - 1] + BirilliCaduti;
            TotaliTXT[Turno - 1].text = Totali[Turno - 1].ToString();
        }

        /*
        foreach (float LancioStrike in LanciStrike)
        {
            if(LancioStrike == Turno - 2)
            {
                LanciStrike.RemoveAt(LanciStrike.Count - 1);
            }
        }*/
        
        if (LanciStrike.Count > 0 && LanciStrike[LanciStrike.Count - 1] == TiriEffettuati - 3) // rimuovere l ultimo elemento della lista degli strike se è rimasto nella lista gia 2 turni
        {
            LanciStrike.RemoveAt(LanciStrike.Count - 1);
        }
        if (LanciSpeare.Count > 0 && LanciSpeare[LanciSpeare.Count - 1] == Turno)// rimuovere l ultimo elemento della lista degli speare se è rimesto nella lista gia 1 turno
        {
            LanciSpeare.RemoveAt(LanciSpeare.Count - 1);
        }
        PlayerPrefsX.SetIntArray("PrimoTiro", PrimoTiro);
        PlayerPrefsX.SetIntArray("SecondoTiro", SecondoTiro);
        PlayerPrefsX.SetIntArray("Totali", Totali);
        if (SecondoTiroTXT[Turno - 1].text != "") 
        {
            PrimoLancio = true;
        }
        
       
        // rispristino birilli e palla per un nuovo tiro
        DistruggiBirilli();
        if (SecondoTiroTXT[Turno - 1].text != "" || PrimoTiro[Turno - 1] == 10) 
        {
            StartCoroutine(CreaBirilli());
        }
        if (PrimoTiroTXT[Turno - 1].text != "" && SecondoTiroTXT[Turno - 1].text != "" || PrimoTiro[Turno - 1] == 10)
        {
            Turno++;
        }


        RigidBSfera.constraints = RigidbodyConstraints.FreezeAll;
        Sfera.transform.localPosition = new Vector3(0, 0.13f, -6);
        Sfera.transform.localRotation = new Quaternion(80, 180, 90, Sfera.transform.localRotation.w);

        SferaFather.transform.position = new Vector3(0, 0, 0);
        RotazioneAlSecondo = new Vector3(0, 0, 0);
        RigidBSfera.constraints = RigidbodyConstraints.None;
        MainCameraFather.transform.position = new Vector3(0, 0, 0);
        AnimSfera.enabled = true;
        AnimMainCamera.enabled = true;
        
        i = -1;
        BirilliCaduti = 0;
        AnimFrecciaAngolo.enabled = true;
        AnimFrecciaDirezione.enabled = true;
        AnimFrecciaForza.enabled = true;
        AnimFrecciaRotazione.enabled = true;
        //AccUI.TV.SetActive(false);
    }
    public void DistruggiBirilli()
    {
        
        foreach (Transform Birillo in Birilli.transform) // ripristina posizione birilli
        {
            if (BirilliCaduti == 10 || SecondoTiroTXT[Turno - 1].text != "")
            {
                GameObject.Destroy(Birillo.gameObject);
            }

            else if (BirilliCaduti != 10)
            {
                if (Birillo.transform.localRotation.x > 0.01 || Birillo.transform.localRotation.y > 0.01 || Birillo.transform.localRotation.z > 0.01 || Birillo.transform.localRotation.x < -0.01 || Birillo.transform.localRotation.y < -0.01 || Birillo.transform.localRotation.z < -0.01)
                {
                    GameObject.Destroy(Birillo.gameObject);
                }
            }
            
        }
    }
    public IEnumerator CreaBirilli()
    {
        int b;
        yield return new WaitForSeconds(0.1f);
        for (b = 0; b < 10; b++)// istanzia nuovi birilli
        {
            BirilloNuovo[b] = Instantiate(BirilloPrefab, PosizioneBirilli[b], new Quaternion(0,0,0,0)) as GameObject;
            BirilloNuovo[b].transform.SetParent(Birilli.transform);
        }
    }
    public void ConteggioBirilli()
    {
        foreach (Transform Birillo in Birilli.transform)
        {
            if (Birillo.transform.localRotation.x > 0.01 || Birillo.transform.localRotation.y > 0.01 || Birillo.transform.localRotation.z > 0.01 || Birillo.transform.localRotation.x < -0.01 || Birillo.transform.localRotation.y < -0.01 || Birillo.transform.localRotation.z < -0.01)
            {
                BirilliCaduti++;
            }
        }
       //AccUI.TVText.text = BirilliCaduti.ToString();
    }
    
    public void CompilaArrayPlayerPrefs(int[] array, int Valore1, int Valore2, int Valore3)
    {
        print(array.Length);
        array[array.Length - 3] = Valore1;
        array[array.Length - 2] = Valore2;
        array[array.Length - 1] = Valore3;
    }// compila l' array dei playerprefs slot
  
}
