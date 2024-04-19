using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turn_by_Turn : MonoBehaviour
{   
    private GameManager gameManager;
    public GameObject Continue;
    public TextMeshProUGUI Ganador;
    private bool ronda = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    //Funcion cambio de turno
    public void Turn_Switch()
    {
        if (!ronda)
        {
            Verificar_Ronda();
            if (gameManager.turno == 1 && !gameManager.turn2_end)
            {
                gameManager.P1.SetActive(false); //Desactivar Camara jugador 1
                gameManager.P2.SetActive(true);  //Activar Camara jugador 2

                if (!gameManager.turn2_end)
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
                gameManager.Lure = null;
            }
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
        Ganador.enabled = true;
        Ganador.text = gameManager.Reset_Power();
        gameManager.Turn_Invoque = true;
        ronda = true;
        if(!End_Game())
        {
            StartCoroutine(Cambiar_Ronda());
            Vaciar_Campo();
            gameManager.mazo1.GetComponent<Mazo>().Robar(2);
            gameManager.mazo2.GetComponent<Mazo>().Robar(2);
        }
        
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

        for(int i = 0; i < gameManager.Climas.Length;i++)
        {
            if (gameManager.Climas[i] != null)
            {
                gameManager.Climas[i].GetComponent<Cartas_Especiales>().Eliminar_Clima_influence();
                Destroy(gameManager.Climas[i]);
            }
        }
        
    }

    public bool End_Game()
    {
        if (gameManager.ronda1 == 2)
        {
            Ganador.text = "Las fuerzas del bien han ganado la guerra";
            Continue.SetActive(true);
            return true;
        }
        if (gameManager.ronda2 == 2)
        {
            Ganador.text = "Las fuerzas del mal han ganado la guerra";
            Continue.SetActive(true);

            return true;
        }

        return false;
    }
    IEnumerator Cambiar_Ronda()
    {
        yield return new WaitForSeconds(3);
        Ganador.enabled = false;
        gameManager.Turn_Invoque = false;
        ronda = false;
    }
}
