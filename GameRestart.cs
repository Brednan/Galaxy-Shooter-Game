using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameRestart : MonoBehaviour
{
    public Player player;

    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {

            //player = GameObject.Find("Player").GetComponent<Player>();

            
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("gameover");
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameOver = false;
            Time.timeScale = 1;
        }
    }

}
