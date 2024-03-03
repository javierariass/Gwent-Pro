using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    public GameObject[] mazo = new GameObject[30];
    public GameObject[] Hand_Pos = new GameObject[10];
    public GameObject[] Hand = new GameObject[10];
    private GameObject[] Card_Campo = new GameObject[12];
    private GameObject[] Melee_pos, Range_pos, Asedio_pos = new GameObject[4];
    private GameObject LeaderPos,ClimaPos;
    private GameManager manager;
    public GameObject Leader;
    private int carta_actual_deck = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ClimaPos = GameObject.FindGameObjectWithTag("Clima");
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        Buscar_Lugares();
        Barajear(mazo);
        Robar(10);
    }
    

    //Funcion para detectar posiciones donde poner las cartas
    private void Buscar_Lugares()
    {
        if(gameObject.CompareTag("elven"))
        {
            Melee_pos = GameObject.FindGameObjectsWithTag("Melee_pos");
            Range_pos = GameObject.FindGameObjectsWithTag("Range_pos");
            Asedio_pos = GameObject.FindGameObjectsWithTag("Asedio_pos");
            LeaderPos = GameObject.FindGameObjectWithTag("Leader1");            
        }

        if(gameObject.CompareTag("Cuevita-land"))
        {
            Melee_pos = GameObject.FindGameObjectsWithTag("Melee_pos1");
            Range_pos = GameObject.FindGameObjectsWithTag("Range_pos1");
            Asedio_pos = GameObject.FindGameObjectsWithTag("Asedio_pos1");
            LeaderPos = GameObject.FindGameObjectWithTag("Leader2");
        }
        GameObject.Instantiate(Leader, LeaderPos.transform.position, LeaderPos.transform.rotation);
        Leader.transform.localScale = LeaderPos.transform.localScale;
    }

    //Funcion barajear deck
    static GameObject[] Barajear(GameObject[] mazo)
    {
        GameObject card;

        for(int i = 0; i < mazo.Length; i++)
        {
            int d = Random.Range(0, mazo.Length);
            card = mazo[i];
            mazo[i] = mazo[d];
            mazo[d] = card;
        }

        return mazo;
    }

    //Robar carta del mazo
    public void Robar(int cantidad)
    {
        int j = 0;
        for(int i = 0; i < Hand.Length; i++)
        {           
            //Robar carta y poner en la mano
            if(Hand[i] == null)
            {
                Hand[i] = GameObject.Instantiate(mazo[carta_actual_deck], Hand_Pos[i].transform.position, Hand_Pos[i].transform.rotation);
                if(gameObject.CompareTag("elven"))
                {
                    Hand[i].transform.parent = manager.P1.transform; // Localizar donde ubicar la carta si es elfo
                }

                if (gameObject.CompareTag("Cuevita-land"))
                {
                    Hand[i].transform.parent = manager.P2.transform; // Localizar donde ubicar la carta si es orco
                }
                carta_actual_deck++;
                j += 1;
            }

            
            if(j == cantidad) //Destruir carta por no caber en la mano o haber robado todas las cartas
            {
                break;
            }
        }
    }

    //Invocar carta
    public bool Invocar(GameObject card)
    {
        if (manager.Turn_Invoque == false) //Verificar si este turno ya se invoco una carta
        {
            bool hecho = false;
            for (int i = 0; i < 12; i++)
            {
                //Verificar si queda casilla y si quedan invocar carta cuerpo a cuerpo
                if(i < 4)
                {
                    if (Card_Campo[i] == null && card.GetComponent<General>().Type_card == "Melee")
                    {
                        card.transform.position = Melee_pos[i].transform.position;
                        card.transform.localScale = Melee_pos[i].transform.localScale;
                        Card_Campo[i] = card;
                        hecho = true;
                        break;
                    }
                }
                
                //Verificar si queda casilla y si quedan invocar carta a distancia
                if(i >= 4 && i < 8 )
                {
                    if (Card_Campo[i] == null && card.GetComponent<General>().Type_card == "Range")
                    {
                        card.transform.position = Range_pos[i - 4].transform.position;
                        card.transform.localScale = Range_pos[i - 4].transform.localScale;
                        Card_Campo[i] = card;
                        hecho = true;
                        break;
                    }
                }

                //Verificar si queda casilla y si quedan invocar carta de asedio
                if (i >= 8)
                {
                    if (Card_Campo[i] == null && card.GetComponent<General>().Type_card == "Asedio")
                    {
                        card.transform.position = Asedio_pos[i - 8].transform.position;
                        card.transform.localScale = Asedio_pos[i - 8].transform.localScale;
                        Card_Campo[i] = card;
                        hecho = true;
                        break;
                    }
                }                                
            }

            //Verificar si la carta es clima
            if(card.GetComponent<General>().Type_card == "Wheather")
            {
                if (manager.clima != null)
                {
                    Destroy(manager.clima);
                }
                manager.clima = card;
                manager.clima.GetComponent<Cartas_Clima>().Elimnar_Clima_influence();
                card.transform.SetPositionAndRotation(ClimaPos.transform.position, ClimaPos.transform.rotation);
                card.transform.localScale = ClimaPos.transform.localScale;
                hecho = true;
            }
            //Si la carta fue invocada eliminar de la mano
            if (hecho)
            {
                for (int i = 0; i < Hand.Length; i++)
                {
                    if (Hand[i] == card)
                    {
                        card.transform.parent = null;
                        for(int v = 0; v < manager.Cartas_Campo.Length; v++)
                        {
                            if (manager.Cartas_Campo[v] == null && card.GetComponent<General>().Type_card != "Wheather")
                            {
                                manager.Cartas_Campo[v] = card;
                                break;
                            }
                        }
                        Hand[i] = null;                       
                        manager.Turn_Invoque = true;//Prohibir invocar mas cartas en este turno
                        return true;
                    }
                }
            }
        }       
        return false;
    }    
}
