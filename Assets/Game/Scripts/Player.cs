using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    //variables are buckets that hold information
    //public or private identify
    //data type (int, float, bool, strings)
    //every variable has a name
    //optional value assignment

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool shieldsActive = false;

    public int lives = 3;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    //fireRate is 0.25f
    //canFire -- has the amount of time between firing passed?
    //time.time

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

	// Use this for initialization
	private void Start () 
    {
        //current pos = new position
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
    }
	
	// Update is called once per frame
	private void Update () 
    {
        Movement();

        //if space key pressed
        //spawn laser at player position

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //if tripleshot is true
        //shoot 3 lasers from those positions in unity
        //else if tripleshot is false
        //proceed with code below

        if (Time.time > _canFire)
        {
            if (canTripleShot == true)
            {
                //Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.13f, 0.45f, 0), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.91f, 0), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, -0.06f, 0), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, -0.06f, 0), Quaternion.identity);
            }
            else
            {
                //spawn laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.up * _speed * 2.0f * verticalInput * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * 2.0f * horizontalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
        }

        if (transform.position.x > 9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, transform.position.z);
        }
    }

    public void Damage()
    {
        //subtract 1 life from the player
        //lives = lives - 1;
        //lives -= 1;
        if (shieldsActive == true)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
        //if lives < 1 (meaning 0)
        //destroy player
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
       
    }

    public void SpeedBoostPowerUp()
    {
        canSpeedBoost = true;
        //_speed *= 2.0f;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void EnableShield()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
}
