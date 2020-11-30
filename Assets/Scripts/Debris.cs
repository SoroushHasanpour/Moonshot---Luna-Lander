using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public bool isDebris;   

    // Start is called before the first frame update
    void Start()
    {
        if (isDebris)
        {
            Vector3 randScale = new Vector3(Random.Range(-25, 50), Random.Range(-25, 50), Random.Range(-25, 50));
            transform.localScale += randScale;
        }
        

        float spinForce = Random.Range(750, 1500);
        float directionForce = Random.Range(750, 1500);

        Vector3 spinDir = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)).normalized;
        Vector3 forceDir = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)).normalized;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(spinDir*spinForce);
        rb.AddForce(forceDir * directionForce);
    }
}
