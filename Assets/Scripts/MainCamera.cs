using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public GameObject Sfera;
    public float i = 4;
    // Update is called once per frame
    void Update()
    {
        if(Sfera.transform.position.z < 3f)
        {
            if (Mathf.Abs(transform.position.z - Sfera.transform.position.z) > 4f)
            {
                transform.position = new Vector3(0, 1, Sfera.transform.position.z - 4f);

            }
        }
        else if(Sfera.transform.position.z > 12)
        {

        }
        else
        {
            transform.position = new Vector3(0, 1, Sfera.transform.position.z - i);
            if(i < 4f)
            {
                i += Time.unscaledDeltaTime/ 2;
            }
        }
        
    }
}
