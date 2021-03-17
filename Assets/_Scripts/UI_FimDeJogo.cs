using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_FimDeJogo : MonoBehaviour
{

    GameManager gm;
    void OnEnable()
    {
        gm = GameManager.GetInstance();
    }
    public void Voltar()
    {
        SceneManager.LoadScene(0);
        gm.ChangeState(GameManager.GameState.MENU);
    }
    
}
