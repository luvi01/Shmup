using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolantBehavior : SteerableBehaviour, IDamageable, IShooter
{
    float angle = 0;
    public GameObject bullet;
    public float shootDelay = 1.0f;
    private float _lastShootTimestamp = 0.0f;
    private GameManager gm;
    public int experience;


    public void Awake()
    {
        gm = GameManager.GetInstance();
    }

    public void TakeDamage()
    {
        Die();
    }

    public void Die()
    {
        var player = gm.GetActivePlayer();
        player.CurrentScore += experience;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        angle += 0.1f;
        if (angle > 2.0f * Mathf.PI) angle = 0.0f;
        Thrust(0, Mathf.Cos(angle));
        if (Time.time - _lastShootTimestamp < shootDelay) return;
        _lastShootTimestamp = Time.time;
        Shoot();
    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
