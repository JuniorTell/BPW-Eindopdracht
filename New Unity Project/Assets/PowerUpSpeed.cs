using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{

    public float multiplier = 1.4f;
    public float duration = 4f;

    public GameObject pickupEffect;

    void OnTriggerEnter (Collider other)
    {
        if   (other.CompareTag("Player") )
        {
            StartCoroutine( Pickup(other) ); //Usage of a Parameter
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply effect to the player
            ////player.transform.localScale *= multiplier;
        //Get a certain script from the "other" object and give it a custom name
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        //Change something specific inside the custom named script
            ////movement.raycastDistance *= multiplier;
        movement.speed *= multiplier;

        //Disable graphics & colliders of the powerup (to counter WaitForSeconds bug)
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //Wait x amount of seconds
        yield return new WaitForSeconds(duration);
        //Reverse effect on player
        movement.speed /= multiplier;

        //Remove powerup object
        Destroy(gameObject);

    }
}
