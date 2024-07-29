using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TiroTestMode : MonoBehaviour
{
    public UIGioco AccUIGioco;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        AccUIGioco = GameObject.Find("UIGioco").GetComponent<UIGioco>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ParametriTiroTestMode()
    {
        foreach (Transform ButtonTiro in AccUIGioco.ElencoButtonTiriTestmode.transform)
        {
            ButtonTiro.GetComponent<Image>().color = Color.white;
            ButtonTiro.GetComponentInChildren<Text>().color = Color.white;
        }
        //AccUIGioco.ParametriTiriTestMode((Mathf.RoundToInt((gameObject.transform.localPosition.y + 140) / 90) * -1) - 1);
        gameObject.GetComponent<Image>().color = Color.red;
        gameObject.GetComponentInChildren<Text>().color = Color.red;
        AccUIGioco.ParametriTiriTestMode(index);

        AccUIGioco.AccGeneraleTestmode.NumeroTiroInElenco = index;
        //AccUIGioco.AccGeneraleTestmode.NumeroTiroInElenco = Mathf.RoundToInt((gameObject.transform.localPosition.y + 140) / 90) * -1 - 1;

    }

}
