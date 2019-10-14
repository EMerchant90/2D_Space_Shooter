using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    //variable for speed
    [SerializeField]
    private float _enemySpeed = 3.0f;

    public int score = 0;

    private UIManager _uIManager;

    // Use this for initialization
    void Start ()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //when off screen at bottom
        //respawn back on top with a new x position between the bounds of screen
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
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uIManager.UpdateScore();
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage(); 
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
