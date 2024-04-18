using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public int turno = 1; //1- Jugador 1, 2- Jugador 2
    public int ronda = 0;
    public GameObject P1, P2; //Camara jugadores
    public GameObject mazo1, mazo2;
    public GameObject[] aumentos = new GameObject[6];
    public GameObject[] Climas = new GameObject[3];
    public GameObject[] HandBack1, HandBack2;
    public GameObject[] Cartas_Campo = new GameObject[24];
    private TextMeshProUGUI TextPower;
    private TextMeshProUGUI TextPower2;
    int poder_jugador = 0;
    int poder_enemigo = 0;
    public int cartas_Selected = 0;
    public bool Turn_Invoque, turn1_end, turn2_end = false;
    
    // Start is called before the first frame update
    void Start()
    {
        TextPower = GameObject.FindGameObjectWithTag("Texto_power").GetComponent<TextMeshProUGUI>();
        TextPower2 = GameObject.FindGameObjectWithTag("Texto_power2").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Power();
        Cartas_Back_Check();
    }
    public void Power()
    {
        poder_jugador = 0;
        poder_enemigo = 0;
       for (int i = 0; i < Cartas_Campo.Length;i++)
        {
            if (Cartas_Campo[i] != null)
            {
                if (Cartas_Campo[i].CompareTag("Elfo"))
                {
                    poder_jugador += Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk;
                }
                if (Cartas_Campo[i].CompareTag("orc"))
                {
                    poder_enemigo += Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk;
                }
            }
        }
        if(turno == 1)
        {
            TextPower.text = poder_jugador.ToString();
            TextPower2.text = poder_enemigo.ToString();
        }

        if(turno == 2)
        {
            TextPower2.text = poder_jugador.ToString();
            TextPower.text = poder_enemigo.ToString();
        }
    }

    public string Reset_Power()
    {
        string jugador = "";

        if(poder_jugador > poder_enemigo)
        {
            jugador = "Jugador 1";
        }
        if(poder_jugador < poder_enemigo)
        {
            jugador = "Jugador 2";
        }
        
        if(poder_jugador == poder_enemigo)
        {
            jugador = "Empate";
        }
        poder_enemigo = 0;
        poder_jugador = 0;
        return jugador;
    }

    public void Cartas_Back_Check()
    {
        for (int i = 0; i < mazo1.GetComponent<Mazo>().Hand.Length; i++)
        {
            if (mazo1.GetComponent<Mazo>().Hand[i] == null)
            {
                HandBack2[i].SetActive(false);
            }
            else
            {
                HandBack2[i].SetActive(true);
            }

            if (mazo2.GetComponent<Mazo>().Hand[i] == null)
            {
                HandBack1[i].SetActive(false);
            }
            else
            {
                HandBack1[i].SetActive(true);
            }
        }
    }

}
