using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfera : MonoBehaviour
{
    public Generale AccGenerale;
    public GeneraleTestMode AccGeneraleTestMode;
    public bool CollisioneAvvenuta = false;

    private IEnumerator Lanciato()              // funzione che quando la palla collide con i birilli attiva i metodi di TestMode o PlayMode
    {
        if (CollisioneAvvenuta == false)
        {
            CollisioneAvvenuta = true;
            yield return new WaitForSeconds(4);
            if (AccGenerale.enabled)
            {
                AccGenerale.GestionePunteggio();
            }
            else
            {
                AccGeneraleTestMode.ConteggioBirilli();
                AccGeneraleTestMode.SalvaParametriTiri();
                AccGeneraleTestMode.CreaButton();
                AccGeneraleTestMode.RipristinoLancio();
            }
            CollisioneAvvenuta = false;
        }
    }
    private void FixedUpdate()
    {
        if(this.transform.position.z > 11.5f)
        {
            StartCoroutine(Lanciato());
        }
    }
}
