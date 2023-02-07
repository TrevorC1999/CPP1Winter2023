using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Pickuptype
    {
        Powerup = 0,
        Life = 1,
        Score = 2
    }
    // Start is called before the first frame update
    public Pickuptype currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController temp = collision.gameObject.GetComponent<PlayerController>();
            switch (currentPickup)
            {
                case Pickuptype.Powerup:
                    temp.StartJumpForceChange();
                    break;
                case Pickuptype.Life:
                    temp.lives++;
                    Debug.Log("You gained a life");
                    
                    break;

                case Pickuptype.Score:
                    temp.score++;
                    Debug.Log("Current Score: " + temp.score);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
