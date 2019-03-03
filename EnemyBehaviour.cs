using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameRestart gameRestart;
    private AudioSource audioSource;

    [SerializeField]
    private float speed = 0.5f;

    
    public Player player;

    [SerializeField]
    private GameObject EnemyExplosion;

    float x;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();
       
    }

    // Update is called once per frame
    void Update()
    {
        gameRestart = GameObject.Find("GameRestart").GetComponent<GameRestart>();

        while (gameRestart.gameOver == true)
        {

            return;
        }

        x = Random.Range(-1.787f, 1.614f);

        transform.Translate(Vector3.down * speed * Time.deltaTime);

        float boundaryYnegative = -1.108565f;

        if(transform.position.y < boundaryYnegative)
        {
            transform.position = new Vector3(x, 0.725f, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {

            if (player != null)
            {
                player.eliminations += 1;
                Destroy(this.gameObject);
                Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
                if(player.shieldActive == true)
                {
                    player.lives -= 0;
                    player.shieldCount -= 1;
                    StartCoroutine("EnemyExplosionDestroy");
                }
                else if(player.shieldActive == false)
                {
                    player.lives -= 1;
                    player.shieldCount -= 0;
                    StartCoroutine("EnemyExplosionDestroy");
                }
            }
        }
        else if(other.tag == "Laser")
        {
            if(player != null)
            {
                player.eliminations += 1;               
            }
                            Destroy(other.gameObject);
                Destroy(this.gameObject);
                Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
            StartCoroutine("EnemyExplosionDestroy");
        }
    }
     IEnumerator EnemyExplosionDestroy()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        Destroy(EnemyExplosion);
        //Debug.Log("destroy");
    }
}
