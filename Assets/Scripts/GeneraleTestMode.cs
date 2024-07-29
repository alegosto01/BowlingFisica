using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneraleTestMode : MonoBehaviour
{
    public UIGioco AccUIGioco;
    public Animator AnimSfera;
    public Animator AnimFatherCamera;
    public Animator AnimCamera;
    public GameObject FatherCamera;
    public float[] Parametri;
   
    public GameObject Sfera;
    public Rigidbody RigidBSfera;
    public GameObject MainCamera;
    public Vector3 RotazioneAlSecondo;
    //public PlayMode AccPlayMode;
    public GameObject SferaFather;
    public TimeManager AccTimeManager;
    public bool Prova = false;
    public int Turno = 1;
    public List <int> EsitiTiri;
    public GameObject Birilli;
    public int BirilliCaduti = 0;
    public GameObject BirilloPrefab;
    public Vector3[] PosizioneBirilli = new Vector3[10];
    public GameObject[] BirilloNuovo;
    public List<float> ParametroPosizioneIniziale;
    public List<float> ParametroDirezione;
    public List<float> ParametroAngolazione;
    public List<float> ParametroRotazione;
    public List<float> ParametroForza;
    public List<float> ParametroAltezza;
    public int Index = 0;
    public Button TiroPrefab;
    public Text IndexElencoButtonTXT;
    public GameObject Elenco;
    public int NumeroTiroInElenco = 0;
    public int TiriTotaliElenco;
    public ScrollRect Scrollrect;
   // public List<int> EsitiTiri;
    void Start()
    {
       
        ParametroPosizioneIniziale.AddRange(PlayerPrefsX.GetFloatArray("PosizioneIniziale"));
        ParametroDirezione.AddRange(PlayerPrefsX.GetFloatArray("Direzione"));
        ParametroAngolazione.AddRange(PlayerPrefsX.GetFloatArray("Angolazione"));
        ParametroRotazione.AddRange(PlayerPrefsX.GetFloatArray("Rotazione"));
        ParametroForza.AddRange(PlayerPrefsX.GetFloatArray("Forza"));
        ParametroAltezza.AddRange(PlayerPrefsX.GetFloatArray("Altezza"));
        EsitiTiri.AddRange(PlayerPrefsX.GetIntArray("EsitiTiri"));
        TiriTotaliElenco = PlayerPrefs.GetInt("TiriTotali", 0);
        NumeroTiroInElenco = 0;
        CreaButtonsSalvati();
    }

    // Update is called once per fra



    private void FixedUpdate()
    {
         if (Prova)
            {
                FatherCamera.transform.position = Vector3.Lerp(FatherCamera.transform.position, new Vector3(0, 0, 0), 0.1f);
                FatherCamera.transform.rotation = Quaternion.Lerp(FatherCamera.transform.rotation, new Quaternion(0, 0, 0, 1), 0.1f);
            }
         Quaternion deltaRotation = Quaternion.Euler(RotazioneAlSecondo * Time.deltaTime);
         RigidBSfera.MoveRotation(RigidBSfera.rotation * deltaRotation);

        if (Input.GetKey(KeyCode.D))
        {
            CancellaSalvataggiTestMode();
        }
    }
    public IEnumerator DaPanoramaANormale()
    {
        while (FatherCamera.transform.position != new Vector3(0, 0, 0))
        {
            yield return null;
        }
    }
    public void RipristinoLancio() // rispristina le cose per fare un altro tiro
    {
        //rispristino birilli e palla per un nuovo tiro
        DistruggiBirilli();
        RigidBSfera.constraints = RigidbodyConstraints.FreezeAll;
        Sfera.transform.localPosition = new Vector3(0, 0.13f, -6);
        SferaFather.transform.position = new Vector3(0, 0, 0);
        RotazioneAlSecondo = new Vector3(0, 0, 0);
        RigidBSfera.constraints = RigidbodyConstraints.None;
        FatherCamera.transform.position = new Vector3(0, 0, 0);
        AnimSfera.enabled = true;
        AnimCamera.enabled = true;
        BirilliCaduti = 0;
        CreaBirilli();
        int i;
        for (i = 0; i < 6; i++)
        {
            Parametri[i] = 0;
        }
        int k;
        for (k = 0; k < 6; k++)
        {
            AccUIGioco.InputFieldParametri[k].text = "";
        }
        //AccUI.TV.SetActive(false);
    }
    public void DistruggiBirilli()       // ditrugge i birilli caduti
    {

        foreach (Transform Birillo in Birilli.transform) // ripristina posizione birilli
        {
            /*
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
            }*/
            GameObject.Destroy(Birillo.gameObject);

        }
    }
    public void CreaBirilli()    //crea nuovi 10 birilli
    {
        int b;
        //yield return new WaitForSeconds(0.1f);
        for (b = 0; b < 10; b++)
        {
            BirilloNuovo[b] = Instantiate(BirilloPrefab, PosizioneBirilli[b], new Quaternion(0, 0, 0, 0)) as GameObject;
            BirilloNuovo[b].transform.SetParent(Birilli.transform);
        }
    }
    public void ConteggioBirilli() // Conta i birilli caduti
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
    public void LanciaTestMode()            // funzione di lancio della palla in testmode
    {
        AnimSfera.enabled = false;
        AnimCamera.enabled = false;
        RigidBSfera.constraints = RigidbodyConstraints.None;
        //RigidBSfera.useGravity = false;
        RotazioneAlSecondo = new Vector3(0, 0, Parametri[3]);
        /*foreach (Transform Birillo in Birilli.transform)
        {
            Birillo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }*/
        //Physics.gravity = new Vector3(0, 0, 0);
        Physics.gravity = new Vector3(0, -9.81f, 0);

        RigidBSfera.AddForce(new Vector3(-Mathf.Sin((Parametri[1] * Mathf.PI) / 180),Mathf.Sin(((Parametri[2]) * Mathf.PI) / 180), Mathf.Cos(((Parametri[2]) * Mathf.PI) / 180)) * Parametri[4], ForceMode.Impulse);
        //RigidBSfera.useGravity = true;

        /*foreach (Transform Birillo in Birilli.transform)
        {
            Birillo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }*/
        //yield return new WaitForSeconds(5);
        //GestionePunteggio();
    }
    public void SalvaParametriTiri()              // Salva i parametri del tiro effettuato nei vari PlayerPrefs e il numero di birilli caduti
    {
        ParametroPosizioneIniziale.Add(Parametri[0]);
        ParametroDirezione.Add(Parametri[1]);
        ParametroAngolazione.Add(Parametri[2]);
        ParametroRotazione.Add(Parametri[3]);
        ParametroForza.Add(Parametri[4]);
        ParametroAltezza.Add(Parametri[5]);
        EsitiTiri.Add(BirilliCaduti);
        PlayerPrefsX.SetFloatArray("PosizioneIniziale", ParametroPosizioneIniziale.ToArray());
        PlayerPrefsX.SetFloatArray("Direzione", ParametroDirezione.ToArray());
        PlayerPrefsX.SetFloatArray("Angolazione", ParametroAngolazione.ToArray());
        PlayerPrefsX.SetFloatArray("Rotazione", ParametroRotazione.ToArray());
        PlayerPrefsX.SetFloatArray("Forza", ParametroForza.ToArray());
        PlayerPrefsX.SetFloatArray("Altezza", ParametroAltezza.ToArray());
        PlayerPrefsX.SetIntArray("EsitiTiri", EsitiTiri.ToArray());
        Index++;
    }
    public void CreaButton()                     // Crea un Button ( che se cliccato mostra i parametri di quel tiro) nell elenco della testmode
    {
        foreach (Transform Button in Elenco.transform)
        {
            Button.transform.localPosition += new Vector3(0, 90, 0);
        }
        Button button;
        button = GameObject.Instantiate(TiroPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Button;
       
        button.GetComponent<RectTransform>().SetParent(Elenco.transform);
        NumeroTiroInElenco++;
        TiriTotaliElenco++;
        PlayerPrefs.SetInt("TiriTotali", PlayerPrefs.GetInt("TiriTotali", 0) + 1);

        // àncoro il button a bottom center 
        RectTransform Rect = button.GetComponent<RectTransform>();
        Rect.anchorMin = new Vector3(0.5f, 0);
        Rect.anchorMax = new Vector3(0.5f, 0);
        Rect.pivot = new Vector3(0.5f, 0.5f);
        Rect.anchoredPosition = new Vector3(100, 90, 0);
        //button.transform.localPosition = (100,Elenco.transform.position - (4)

        //Rect.localEulerAngles = new Vector3(100, 90, 0);

        //button.transform.localPosition = new Vector3(100, (TiriTotaliElenco * -90) - 140, 0);
        //Elenco.transform.position += new Vector3(0, 90, 0);
        button.GetComponentInChildren<Text>().text = BirilliCaduti.ToString();
        button.GetComponent<TiroTestMode>().index = TiriTotaliElenco - 1;

        // se ci sono piu di 4 button si deve aumentare l altezza dell elenco e ridimensionare i tasti
        if(TiriTotaliElenco > 4)
        {
            Elenco.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 90);
           // Elenco.transform.position += new Vector3(0,90,0);
        }

        // creo numero di fianco al button
        Text Numero;
        Numero = GameObject.Instantiate(IndexElencoButtonTXT, new Vector3(0,0,0), Quaternion.identity) as Text;
        Numero.GetComponent<RectTransform>().SetParent(button.transform);
        Numero.transform.localPosition = new Vector3(-220,0, 0);
        Numero.text = TiriTotaliElenco.ToString();

    }
    public void CancellaSalvataggiTestMode()  // cancellare tutti i tiri nel elenco
    {

        foreach (Transform Button in Elenco.transform)
        {
            Destroy(Button.gameObject);
            print("Destroyed");
        }

        PlayerPrefs.DeleteAll();
       /* PlayerPrefs.DeleteKey("PosizioneIniziale");
        PlayerPrefs.DeleteKey("Direzione");
        PlayerPrefs.DeleteKey("Angolazione");
        PlayerPrefs.DeleteKey("Rotazione");
        PlayerPrefs.DeleteKey("Forza");
        PlayerPrefs.DeleteKey("Altezza");
        PlayerPrefs.DeleteKey("EsitiTiri");
        PlayerPrefs.DeleteKey("TiriTotali") */

        ParametroPosizioneIniziale.AddRange(PlayerPrefsX.GetFloatArray("PosizioneIniziale"));
        ParametroDirezione.AddRange(PlayerPrefsX.GetFloatArray("Direzione"));
        ParametroAngolazione.AddRange(PlayerPrefsX.GetFloatArray("Angolazione"));
        ParametroRotazione.AddRange(PlayerPrefsX.GetFloatArray("Rotazione"));
        ParametroForza.AddRange(PlayerPrefsX.GetFloatArray("Forza"));
        ParametroAltezza.AddRange(PlayerPrefsX.GetFloatArray("Altezza"));
        EsitiTiri.AddRange(PlayerPrefsX.GetIntArray("EsitiTiri"));
        TiriTotaliElenco = 0;
        Scrollrect.verticalNormalizedPosition = 0;

        Elenco.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(540, 0, 0), Quaternion.identity); // la posizione x è 540 perche se si mette 0 va a -540 non si sa il perche, com'è impostato adesso va a 0.
        Elenco.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 460);
    }
    public void Cancella1SalvataggioTestMode() // cancellare un button nel elenco dei tiri 
    {
        ParametroAltezza.RemoveAt(NumeroTiroInElenco);
        ParametroPosizioneIniziale.RemoveAt(NumeroTiroInElenco);
        ParametroDirezione.RemoveAt(NumeroTiroInElenco);
        ParametroAngolazione.RemoveAt(NumeroTiroInElenco);
        ParametroRotazione.RemoveAt(NumeroTiroInElenco);
        ParametroForza.RemoveAt(NumeroTiroInElenco);
        PlayerPrefsX.SetFloatArray("PosizioneIniziale", ParametroPosizioneIniziale.ToArray());
        PlayerPrefsX.SetFloatArray("Direzione", ParametroDirezione.ToArray());
        PlayerPrefsX.SetFloatArray("Angolazione", ParametroAngolazione.ToArray());
        PlayerPrefsX.SetFloatArray("Rotazione", ParametroRotazione.ToArray());
        PlayerPrefsX.SetFloatArray("Forza", ParametroForza.ToArray());
        PlayerPrefsX.SetFloatArray("Altezza", ParametroAltezza.ToArray());
        Elenco.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 90);
        Elenco.transform.position = new Vector3(540,0,0); // la posizione x è 540 perche se si mette 0 va a -540 non si sa il perche, com'è impostato adesso va a 0.
        TiriTotaliElenco--;
        PlayerPrefs.SetInt("TiriTotali", PlayerPrefs.GetInt("TiriTotali", 0) - 1);

        //Elenco.transform.position -= new Vector3(0, Elenco.transform.position.y, 0);

        foreach (Transform Button in Elenco.transform)
        {
            int index;

            if (Button.GetComponent<TiroTestMode>().index < NumeroTiroInElenco)
            {
                Button.transform.localPosition -= new Vector3(0, 90, 0);
            }
            else if (Button.GetComponent<TiroTestMode>().index == NumeroTiroInElenco)
            {
                Destroy(Button.gameObject);
                print("Destroyed");
            }
            else
            {
                //Button.transform.localPosition += new Vector3(0, 90, 0);
                int.TryParse(Button.transform.GetChild(1).GetComponent<Text>().text, out index);
                Button.transform.GetChild(1).GetComponent<Text>().text = (index - 1).ToString();
                Button.GetComponent<TiroTestMode>().index--;
            }
        }
        if (TiriTotaliElenco >= 5)
        {
            Elenco.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 90);
            //Elenco.transform.position -= new Vector3(0, 90, 0);
        }
        NumeroTiroInElenco--;

        //EsitiTiri.AddRange(PlayerPrefsX.GetIntArray("EsitiTiri"));
    }
    public void CreaButtonsSalvati()
    {
        int i;
        if(TiriTotaliElenco > 0)
        {
            for (i = 0; i < TiriTotaliElenco; i++)
            {
                foreach (Transform Button in Elenco.transform)
                {
                    Button.transform.localPosition += new Vector3(0, 90, 0);
                }
                Button button;
                button = GameObject.Instantiate(TiroPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Button;
                button.GetComponent<RectTransform>().SetParent(Elenco.transform);

                // àncoro il button a bottom center 
                RectTransform Rect = button.GetComponent<RectTransform>();
                Rect.anchorMin = new Vector3(0.5f, 0);
                Rect.anchorMax = new Vector3(0.5f, 0);
                Rect.pivot = new Vector3(0.5f, 0.5f);
                Rect.anchoredPosition = new Vector3(100, 90, 0);
                
                button.GetComponentInChildren<Text>().text = EsitiTiri[NumeroTiroInElenco].ToString();
                button.GetComponent<TiroTestMode>().index = NumeroTiroInElenco;

                // se ci sono piu di 4 button si deve aumentare l altezza dell elenco e ridimensionare i tasti
                if (NumeroTiroInElenco > 3)
                {
                    Elenco.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 90);
                    
                }

                // creo numero di fianco al button
                Text Numero;
                Numero = GameObject.Instantiate(IndexElencoButtonTXT, new Vector3(0, 0, 0), Quaternion.identity) as Text;
                Numero.GetComponent<RectTransform>().SetParent(button.transform);
                Numero.transform.localPosition = new Vector3(-220, 0, 0);
                Numero.text = (NumeroTiroInElenco + 1).ToString();

                NumeroTiroInElenco++;
            }
        }
        
        
    }
}
