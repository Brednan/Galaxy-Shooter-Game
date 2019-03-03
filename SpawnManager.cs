using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameRestart gameRestart;

    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    // Start is called before the first frame update
    void Start()
    {
    StartCoroutine("EnemySpawnRoutine");
    StartCoroutine("PowerUpSpawnRoutine");
    }

    private IEnumerator EnemySpawnRoutine()
    {
        gameRestart = GameObject.Find("GameRestart").GetComponent<GameRestart>();
        Player player = GetComponent<Player>();
        while (gameRestart.gameOver == false)
        {

                yield return new WaitForSeconds(5.0f);
                Instantiate(enemyShipPrefab, new Vector3(Random.Range(-1.787f, 1.614f), 0.5488f), Quaternion.identity);
        }
        
    }
    IEnumerator PowerUpSpawnRoutine()
    {
        gameRestart = GameObject.Find("GameRestart").GetComponent<GameRestart>();

        while (gameRestart.gameOver == false)
        {
            yield return new WaitForSeconds(10.0f);
            int randomPowerup = Random.Range(-1, 2);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-1.913f, 1.77f), Random.Range(0.177f, 0.826f), 0), Quaternion.identity);
        }
    }
}
