using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float duration = 10f;
    public bool hasPowerUpInvisible = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other)); //Usage of a Parameter
        }
    }

    IEnumerator Pickup(Collider player)
    {

        hasPowerUpInvisible = true;

        //Disable graphics & colliders of the powerup so it doesn't exist 
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //Wait x amount of seconds
        yield return new WaitForSeconds(duration);
        hasPowerUpInvisible = false;

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
