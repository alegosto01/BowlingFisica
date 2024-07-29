using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float VelocitàTempo = 0.05f;
    public float DurataRallentamento = 2;
    public bool RallentamentoAttivo = false;
    // Update is called once per frame
    void Update()
    {
        if (RallentamentoAttivo)
        {
            RallentaTempo();
        }
       
    }
    public void RallentaTempo()
    {
        if (Time.timeScale > VelocitàTempo)
        {
            Time.timeScale -= 1f / DurataRallentamento * Time.deltaTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else
        {
            RallentamentoAttivo = false;
        }
    }
    public void VelocizzaTempo()
    {
        if (Time.timeScale < 1)
        {
            //Time.timeScale += 1f / DurataRallentamento * Time.unscaledDeltaTime;
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else
        {
            RallentamentoAttivo = true;
        }
    }
}
