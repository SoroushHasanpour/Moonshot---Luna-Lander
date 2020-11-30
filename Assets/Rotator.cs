using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{


    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * -50000);
    }
}
