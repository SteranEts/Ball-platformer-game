using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Obstacle") { 
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time, 3));
        }
        else if (gameObject.name == "Obstacle (1)")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time, 2));
        }
        else if (gameObject.name == "Obstacle (2)")
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time,1), transform.position.y, transform.position.z);
        }
        else if (gameObject.name == "Obstacle (3)")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time, 5));
        }
        else if (gameObject.name == "Obstacle (6)")
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time, 4), transform.position.y, transform.position.z);
        }
    }
}
