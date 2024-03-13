using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas_Especiales : MonoBehaviour
{
    public string Afectados;
    private GameManager gameManager;
    General datos; 
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        if(gameObject.GetComponent<General>().invocada && gameObject.GetComponent<General>().Type_card == "Wheather")
        {
            Cambiar_Ataque(-1);
        }

        if (gameObject.GetComponent<General>().invocada && gameObject.GetComponent<General>().Type_card == "Increase")
        {
           Cambiar_Ataque(1);
        }
    }

    public void Cambiar_Ataque(int power)
    {
         
        for(int i = 0; i < gameManager.Cartas_Campo.Length; i++)
        {
            
            if (gameManager.Cartas_Campo[i] == null)
            {
                break;
            }
            else
            {
                datos = gameManager.Cartas_Campo[i].GetComponent<General>();
                if (datos.Type_card == Afectados && !datos.clima_influence && gameObject.GetComponent<General>().Type_card == "Wheather")
                {
                    gameManager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk += power;
                    gameManager.Cartas_Campo[i].GetComponent<General>().clima_influence = true;
                }
                
                else if (datos.Type_card == Afectados && !datos.aumento_influence && gameObject.GetComponent<General>().Type_card == "Increase")
                {
                    if(gameObject.CompareTag("Elfo") && datos.gameObject.CompareTag("Elfo"))
                    {
                        gameManager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk += power;
                        gameManager.Cartas_Campo[i].GetComponent<General>().aumento_influence = true;
                    }
                    if (gameObject.CompareTag("orc") && datos.gameObject.CompareTag("orc"))
                    {
                        gameManager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk += power;
                        gameManager.Cartas_Campo[i].GetComponent<General>().aumento_influence = true;
                    }                   
                }
                
            }
            
        }
    }

    public void Eliminar_Clima_influence()
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

    public void Eliminar_Aumento_influence()
    {
        for (int i = 0; i < gameManager.Cartas_Campo.Length; i++)
        {
            if (gameManager.Cartas_Campo[i] != null)
            {
                if (gameManager.Cartas_Campo[i].GetComponent<General>().aumento_influence)
                {
                    gameManager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk -= 1;
                }
                gameManager.Cartas_Campo[i].GetComponent<General>().aumento_influence = false;
            }
        }
    }

    public void Despeje()
    {
        for(int i = 0; i < gameManager.aumentos.Length;i++)
        {
            if (gameManager.aumentos[i] != null)
            {
                Destroy(gameManager.aumentos[i]);
                gameManager.aumentos[i] = null;
            }          
        }
        if(gameManager.clima != null)
        {
            Destroy(gameManager.clima);
            gameManager.clima = null;
        }
    }
}
