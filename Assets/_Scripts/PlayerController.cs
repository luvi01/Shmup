using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    private int lifes;
    Animator animator;
    public GameObject bullet;
    public Transform arma01;
    public float shootDelay = 1.0f;
    public AudioClip shootSFX;
    private GameManager gm;



    private float _lastShootTimestamp = 0.0f;


    public void Start()
    {
        gm = GameManager.GetInstance();

        animator = GetComponent<Animator>();
        gm.vidas = 10;
    }

    public void Shoot()
    {
        if (Time.time - _lastShootTimestamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);

        _lastShootTimestamp = Time.time;
        Instantiate(bullet, arma01.position, Quaternion.identity);
    }

    public void TakeDamage()
    {
        gm.vidas--;
        if (gm.vidas <= 0) Die();
    }

    public void Die()
    {
        gm.ChangeState(GameManager.GameState.ENDGAME);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (gm.gameState != GameManager.GameState.GAME &
             gm.gameState != GameManager.GameState.RESUME) return;

        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }

        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");
        Thrust(xInput, yInput);

        if (yInput != 0 || xInput != 0)
        {
            animator.SetFloat("Velocity", 1.0f);
        }
        else
        {
            animator.SetFloat("Velocity", 0.0f);
        }
        if (Input.GetAxisRaw("Jump") != 0)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }



}
