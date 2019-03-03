using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

        [SerializeField]
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        float boundaryY = 0.7954589f;


        if (transform.position.y > boundaryY)
        {
            Destroy(gameObject);
        }

    }
    private void Move()
    {
        //move up at 10 speed
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
