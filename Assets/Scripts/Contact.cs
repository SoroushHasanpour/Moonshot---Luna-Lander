using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{
    public GameObject Player;
    public GameObject GameController;

    private float xV;
    private float yV;
    private float zV;
    public float criticalValue;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
        }    
    }

    void OnCollisionStay(Collision collision)
    {
        xV = Player.GetComponent<Rigidbody>().velocity.x;
        yV = Player.GetComponent<Rigidbody>().velocity.y;
        zV = Player.GetComponent<Rigidbody>().velocity.z;

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(Player.GetComponent<Rigidbody>().velocity);

            if(-criticalValue < xV && xV < criticalValue)
            {
                if (-criticalValue < yV && yV < criticalValue)
                {
                    if (-criticalValue < zV && zV < criticalValue)
                    {
                        GameController.GetComponent<GameController>().HasLanded();
                    }
                }
            }
        }
    }      
}
