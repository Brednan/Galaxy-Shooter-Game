using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text LiveCounter;
    [SerializeField]
    private Text ShieldHealth;
    [SerializeField]
    private Text PowerupStatus;
    [SerializeField]
    private Text _GameOver;
    [SerializeField]
    private Text scoreText;

    Player player;

    public int score;


    // Start is called before the first frame update
    void Start()
    {
        Player player = GetComponent<Player>();


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore()
    {
        
    }
    
}