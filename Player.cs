using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //public or private identify
    //data type ( int, floats, bool, strings )
    //every variable has a name
    //option value assigned

    public bool hasGameStarted = false;
    
    public GameRestart _gameRestart;

    public int eliminations;
    
    public Text scoreText;
   
    public GameObject _startNewGame;
    
    public Text ShieldHealth;

    public bool shieldActive;

    public GameObject PlayerExplosion;

    public Text LiveCounter;

    public Text PowerupStatus;

    public int shieldCount = 0;

    float enemy;

    public float lives;

    public bool speedPowerUp = false;

    public bool canTripleShot = false;

    public float TripleShotCooldown;

    [SerializeField]
    private GameObject shieldGameObject;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _firstLaserPrefab;
    [SerializeField]
    private GameObject _secondLaserPrefab;
    [SerializeField]
    private GameObject _speedPowerup;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int score;

    

    // Start is called before the first frame update
    void Start()
    {
        _gameRestart = GameObject.Find("GameRestart").GetComponent<GameRestart>();

        _gameRestart.gameOver = true;
        eliminations = 0;

        //current pos = new position
        transform.position = new Vector3(0, -0.799f, 0);

        TripleShotCooldown = Time.time + 4;

        lives = 3;

        shieldCount = 0;

        shieldActive = false;
        PowerupStatus.text = "Triple Shot Status: Not Ready";
        LiveCounter.text = "Lives: " + lives;
        _startNewGame.SetActive(false);
        ShieldHealth.text = "";

    }

    // Update is called once per frame
    void Update()
    {

        while(_gameRestart.gameOver == true)
        {
            _startNewGame.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        LiveCounter.text = "Lives: " + lives;

        PlayerDeath();

        if(lives > 0)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        ShieldOn();

        if (lives == 0)
        {
            _startNewGame.SetActive(true);
        }
        UIStatus();



    }

    private void Shoot()
    {

        //if space key pressed
        //spawn laser at player position


        
            if (Time.time > _canFire)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.204f, 0), Quaternion.identity);
                _canFire = Time.time + _fireRate;
            }

        
            if(Time.time > TripleShotCooldown)
        {
            canTripleShot = true;
        }


            if(canTripleShot == true)
        {
            Instantiate(_firstLaserPrefab, transform.position + new Vector3(-0.19f, -0.1133f, 0), Quaternion.identity);
            Instantiate(_secondLaserPrefab, transform.position + new Vector3(0.199f, -0.102f, 0), Quaternion.identity);
            canTripleShot = false;
            TripleShotCooldown = TripleShotCooldown = Time.time + 4;
        }

    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");


        if (speedPowerUp == true)
        {

            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);

            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else if (speedPowerUp == false)
        {

            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }


        // if player on the y is greater than 0
        // set player position to 0

        float boundaryY = 0.7954589f;
        float boundaryYnegative = -1.108565f;
        float boundaryXnegative = -2.012071f;
        float boundaryX = 1.801926f;

        if (transform.position.y > boundaryY)
        {
            transform.position = new Vector3(transform.position.x, 0.7954589f, transform.position.z);
        }
        else if (transform.position.y < boundaryYnegative)
        {
            transform.position = new Vector3(transform.position.x, boundaryYnegative, transform.position.z);
        }

        if (transform.position.x > boundaryX)
        {
            transform.position = new Vector3(boundaryX, transform.position.y, transform.position.z);

        }
        else if (transform.position.x < boundaryXnegative)
        {
            transform.position = new Vector3(boundaryXnegative, transform.position.y, transform.position.z);
        }
    }

    public void SpeedPowerupOn()
    {
        speedPowerUp = true;
        StartCoroutine(SpeedPowerUpDownRoutine());
    }

    public IEnumerator SpeedPowerUpDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speedPowerUp = false;
   

    }
    private void PlayerDeath()
    {
        if (lives == 0)
        {
            Instantiate(PlayerExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void ShieldOn()
    { 
        if (shieldCount > 0)
        {
            shieldActive = true;
            shieldGameObject.SetActive(true);
            ShieldHealth.text = "Shield Health: " + shieldCount;
        }
        else if(shieldCount < 0 | shieldCount == 0)
        {
            shieldActive = false;
            shieldGameObject.SetActive(false);
            //Debug.Log("shieldOff");
            ShieldHealth.text = "";
        }

    }
    private IEnumerator GameShutdownRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        
    }

    public void UIStatus()
    {

        if (Time.time > TripleShotCooldown)
        {
            //Debug.Log("True");
            PowerupStatus.text = "Triple Shot Status: Ready";
        }
        else
        {
            PowerupStatus.text = "Triple Shot Status: Not Ready";
        }

        LiveCounter.text = "Lives: " + lives;

        if (shieldActive == true)
        {
            ShieldHealth.text = "Shield Health: " + shieldCount;
        }

        if (shieldActive == false)
        {
            ShieldHealth.text = "";
        }
        if (lives < 1)
        {
            _startNewGame.SetActive(true);

            _gameRestart.gameOver = true;         
        }
        else
        {
            _startNewGame.SetActive(false);
            _gameRestart.gameOver = false;
        }
        scoreText.text = "Score: " + eliminations;
    }

}

