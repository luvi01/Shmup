using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : SteerableBehaviour, IDamageable
{
    public int experience;
    GameManager gm;

    public void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void TakeDamage()
    {
        Die();
    }

    public void Die()
    {
        gm = GameManager.GetInstance();
        var player = gm.GetActivePlayer();
        gm.pontos += experience;
        Debug.Log("AAA");
        Debug.Log(gm.pontos);
        player.CurrentScore += experience;
        Destroy(gameObject);

    }
    float angle = 0;

    private void FixedUpdate()
    {
        if (gm.gameState != GameManager.GameState.GAME &
             gm.gameState != GameManager.GameState.RESUME) return;

        angle += 0.1f;

        Mathf.Clamp(angle, 0.0f, 2.0f * Mathf.PI);
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);

        Thrust(x, y);

    }
}
