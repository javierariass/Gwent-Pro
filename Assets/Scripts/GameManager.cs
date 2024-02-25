using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public int turno = 1; //1- Jugador 1, 2- Jugador 2
    public int ronda = 0;
    public GameObject P1, P2; //Camara jugadores
    private TextMeshProUGUI TextPower;
    private TextMeshProUGUI TextPower2;
    int poder_jugador = 0;
    int poder_enemigo = 0;
    public bool Turn_Invoque = false;

    // Start is called before the first frame update
    void Start()
    {
        TextPower = GameObject.FindGameObjectWithTag("Texto_power").GetComponent<TextMeshProUGUI>();
        TextPower2 = GameObject.FindGameObjectWithTag("Texto_power2").GetComponent<TextMeshProUGUI>();
    }

    

    public void Power_Elf(int atk)
    {
        poder_jugador += atk;
        if(turno == 1)
        {
            TextPower.text = poder_jugador.ToString();
            TextPower2.text = poder_enemigo.ToString();
        }

        else
        {
            TextPower2.text = poder_jugador.ToString();
            TextPower.text = poder_enemigo.ToString();
        }
    }
}
