using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject purpleShip;
    public GameObject asteroid;
    public GameObject player;
    private float Timer;

    //https://answers.unity.com/questions/772331/spawn-object-in-front-of-player-and-the-way-he-is.html
    // Start is called before the first frame update
    void Start()
    {
        Timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) { return; }
        
        Vector3 playerPosition = player.transform.position;

        playerPosition.y = Random.Range(-10.0f, 10.0f);

        Vector3 playerDirection = player.transform.right;
        Quaternion playerRotation = player.transform.rotation;

        float spawnDistance = 5;

        Vector3 spawnPosition = playerPosition + spawnDistance * playerDirection;
        if (Timer < Time.time)
        {
            if (Random.Range(0.0f, 1.0f) < 0.8f)
            {
                Instantiate(purpleShip, spawnPosition, playerRotation);
                Timer = Time.time + 5;
            }
            else
            {
                Instantiate(asteroid, spawnPosition, playerRotation);
                Timer = Time.time + 1;
            }
        }
        
    }
}
