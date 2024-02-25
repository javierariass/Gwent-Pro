using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_by_Turn : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {

    }

    //Funcion cambio de turno
    public void Turn_Switch()
    {
        if(gameObject.GetComponent<GameManager>().turno == 1)
        {
            gameObject.GetComponent<GameManager>().P1.SetActive(false); //Desactivar Camara jugador 1
            gameObject.GetComponent<GameManager>().P2.SetActive(true);  //Activar Camara jugador 2
            gameObject.GetComponent<GameManager>().turno = 2;
        }

        else
        {
           gameObject.GetComponent<GameManager>().P2.SetActive(false); //Desactivar Camara jugador 2
           gameObject.GetComponent<GameManager>().P1.SetActive(true);  //Activar Camara jugador 1
            gameObject.GetComponent<GameManager>().turno = 1;
        }
        gameObject.GetComponent<GameManager>().Power_Elf(0);
        gameObject.GetComponent<GameManager>().Turn_Invoque = false; //Permitir invocar carta
    }
}
