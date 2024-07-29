using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApriChiudi : MonoBehaviour
{
    public GameObject[] Aprire;
    public GameObject[] Chiudere;
    public bool ChiudereSeStesso = false;

    public void Apri_Chiudi()
    {
        foreach (GameObject Pagina in Aprire)
        {
            Pagina.SetActive(true);
        }
        foreach (GameObject Pagina in Chiudere)
        {
            Pagina.SetActive(false);
        }
        if (ChiudereSeStesso == true)
        {
            this.gameObject.SetActive(false);
        }
    }

}
