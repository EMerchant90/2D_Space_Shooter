using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = tripleshot, 1 = speedboost, 2 = shields

	// Use this for initialization

	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Movement();

	}

    private void Movement()
    {
        if (transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 6.0f, transform.position.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided with: " + other.name);

        if (other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                //turn the triple shot bool to true
                //if powerup id == 0
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    //enable speed boost here
                    player.SpeedBoostPowerUp();
                }
                else if (powerupID == 2)
                {
                    player.EnableShield();
                }

            }


            //destroy our selves
            Destroy(this.gameObject);

            //handle to the component
        }
    }
}
