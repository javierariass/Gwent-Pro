using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas_Clima : MonoBehaviour
{
    public string Afectados;
    private GameManager gameManager;
   
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        if(gameObject.GetComponent<General>().invocada)
        {
            Reducir_Ataque();
        }
    }

    public void Reducir_Ataque()
    {
        for(int i = 0; i < gameManager.Cartas_Campo.Length; i++)
        {
            if (gameManager.Cartas_Campo[i] == null)
            {
                break;
            }
            else if (gameManager.Cartas_Campo[i].GetComponent<General>().Type_card == Afectados && !gameManager.Cartas_Campo[i].GetComponent<General>().clima_influence)
            {
                gameManager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk -= 1;
                gameManager.Cartas_Campo[i].GetComponent<General>().clima_influence = true;
            }           
        }
    }

    public void Elimnar_Clima_influence()
    {
        for (int i = 0; i < gameManager.Cartas_Campo.Length; i++)
        {
            if (gameManager.Cartas_Campo[i] != null)
            {
                if(gameManager.Cartas_Campo[i].GetComponent<General>().clima_influence)
                {
                    gameManager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk += 1;
                }
                gameManager.Cartas_Campo[i].GetComponent<General>().clima_influence = false;               
            }
        }
    }
}
