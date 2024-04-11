using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_by_Turn : MonoBehaviour
{   
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    //Funcion cambio de turno
    public void Turn_Switch()
    {
        Verificar_Ronda();
        if(gameManager.turno == 1 && !gameManager.turn2_end)
        {
            gameManager.P1.SetActive(false); //Desactivar Camara jugador 1
            gameManager.P2.SetActive(true);  //Activar Camara jugador 2
            
            if(!gameManager.turn2_end)
            {
                gameManager.turno = 2;
            }
        }

        else if (!gameManager.turn1_end)
        {
            gameManager.P2.SetActive(false); //Desactivar Camara jugador 2
            gameManager.P1.SetActive(true);  //Activar Camara jugador 1
            if (!gameManager.turn1_end)
            {
                gameManager.turno = 1;
            }
        }
        if (gameManager.turn1_end && gameManager.turn2_end)
        {
            End_Round();
        }

        else
        {
            gameManager.Turn_Invoque = false; //Permitir invocar carta
        }
        
    }

    public void Verificar_Ronda()
    {
        if (gameManager.turno == 1 && !gameManager.Turn_Invoque)
        {
            gameManager.turn1_end = true;
        }

        if (gameManager.turno == 2 && !gameManager.Turn_Invoque)
        {
            gameManager.turn2_end = true;
        }
    }
    public void End_Round()
    {
        gameManager.turn1_end = false;
        gameManager.turn2_end = false;
        Debug.Log("Gana el jugador " + gameManager.Reset_Power());
        Vaciar_Campo();
        gameManager.mazo1.GetComponent<Mazo>().Robar(2);
        gameManager.mazo2.GetComponent<Mazo>().Robar(2);
    }

    public void Vaciar_Campo()
    {

        for (int j = 0; j < gameManager.Cartas_Campo.Length; j++)
        {
            if (gameManager.Cartas_Campo[j] != null)
            {
                Destroy(gameManager.Cartas_Campo[j]);
            }
        }
        for(int j = 0; j < gameManager.aumentos.Length;j++)
        {
            if (gameManager.aumentos[j] != null)
            {
                gameManager.aumentos[j].GetComponent<Cartas_Especiales>().Eliminar_Aumento_influence();
                Destroy(gameManager.aumentos[j]);
            }
        }

        if(gameManager.clima != null)
        {
            gameManager.clima.GetComponent<Cartas_Especiales>().Eliminar_Clima_influence();
            Destroy(gameManager.clima);
        }
    }
}
