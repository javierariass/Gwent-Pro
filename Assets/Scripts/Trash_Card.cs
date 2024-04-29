using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash_Card : MonoBehaviour
{
    public Start_Game game;
    public int num = 0;

    //Funcion para botar cartas
    public void Activar()
    {
        if(GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().turno == 1)
        {
            game.Cambiar1(num, GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().mazo1.GetComponent<Mazo>());

        }
        if (GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().turno == 2)
        {
            game.Cambiar1(num, GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().mazo2.GetComponent<Mazo>());

        }
    }
}
