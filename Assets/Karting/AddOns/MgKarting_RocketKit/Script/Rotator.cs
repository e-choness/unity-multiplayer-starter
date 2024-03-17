using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    //exoposed Variable to control fan speed
    public float fanSpeed = 0.1f;

    public bool xAxis = false;
    public bool yAxis = false;
    public bool zAxis = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //change the defined Varible to other axis to change the rotation angle.
        if (xAxis == true)
        { transform.Rotate(fanSpeed, 0, 0); }

        if (yAxis == true)
        { transform.Rotate(0, fanSpeed, 0); }

        if (zAxis == true)
        { transform.Rotate(0, 0, fanSpeed); }
    }
}
