using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameRestart gameRestart;

    [SerializeField]
    private int powerupID; // 1 = speed boost, 2 = shields

    [SerializeField]
    private float _speed = 0.5f;

    private float boundaryYnegative = -1.108565f;
    // Update is called once per frame
    void Update()
    {
        gameRestart = GameObject.Find("GameRestart").GetComponent<GameRestart>();

        while(gameRestart.gameOver == true)
        {
            return;
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < boundaryYnegative)
        {
            Destroy(this.gameObject);
        }    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (powerupID == 0)
                {
                    //enable speed boost
                    player.SpeedPowerupOn();
                }
                else if (powerupID == 1)
                {
                    // enable shield
                    player.shieldCount += 3;
                    player.ShieldOn();
                    
                }
            }
            Destroy(this.gameObject);
        }
    }
    private void Destroy()
    {
        float boundaryY = 0.7954589f;
        float boundaryYnegative = -1.108565f;
        float boundaryXnegative = -2.012071f;
        float boundaryX = 1.801926f;

        if (transform.position.y > boundaryY)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.y < boundaryYnegative)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.x > boundaryX)
        {
            Destroy(this.gameObject);
        }

        else if (transform.position.x < boundaryXnegative)
        {
            Destroy(this.gameObject);
        }
    }

}

