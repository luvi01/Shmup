using UnityEngine;
using System.Collections;

public class StatePatrulhaPorWaypoints : State, IShooter
{
    public Transform[] waypoints;
    SteerableBehaviour steerable;

    public GameObject bullet;
    public float shootDelay = 1.0f;
    private float _lastShootTimestamp = 0.0f;
    GameManager gm;


    public override void Awake()
    {
        base.Awake();
        // Configure a transição para outro estado aqui.
        steerable = GetComponent<SteerableBehaviour>();
        gm = GameManager.GetInstance();


    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    public void Start()
    {
        waypoints[0].position = transform.position;
        waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
    }

    public override void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME &
             gm.gameState != GameManager.GameState.RESUME) return;

        if (!(Time.time - _lastShootTimestamp < shootDelay))
        {
            _lastShootTimestamp = Time.time;
            Shoot();
        }
        

        if (Vector3.Distance(transform.position, waypoints[1].position) > .1f)
        {
            Vector3 direction = waypoints[1].position - transform.position;
            direction.Normalize();
            steerable.Thrust(direction.x, direction.y);
        }
        else
        {
            waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
        }
    }

}