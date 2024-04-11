using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    public GameObject[] mazo = new GameObject[30];
    public GameObject[] Hand_Pos = new GameObject[10];
    public GameObject[] Hand = new GameObject[10];
    private GameObject[] Card_Campo = new GameObject[12];
    private GameObject[] Melee_pos, Range_pos, Asedio_pos = new GameObject[4];
    private GameObject LeaderPos,ClimaPos,Clearence_pos,Special_melee_pos,Special_range_pos;
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
            Clearence_pos = GameObject.FindGameObjectWithTag("Despeje_pos1");
            Special_melee_pos = GameObject.FindGameObjectWithTag("Special1");
            Special_range_pos = GameObject.FindGameObjectWithTag("Special2");
        }

        if(gameObject.CompareTag("Cuevita-land"))
        {
            Melee_pos = GameObject.FindGameObjectsWithTag("Melee_pos1");
            Range_pos = GameObject.FindGameObjectsWithTag("Range_pos1");
            Asedio_pos = GameObject.FindGameObjectsWithTag("Asedio_pos1");
            LeaderPos = GameObject.FindGameObjectWithTag("Leader2");
            Clearence_pos = GameObject.FindGameObjectWithTag("Despeje_pos2");
            Special_melee_pos = GameObject.FindGameObjectWithTag("Special3");
            Special_range_pos = GameObject.FindGameObjectWithTag("Special4");
        }
        GameObject.Instantiate(Leader, LeaderPos.transform.position, LeaderPos.transform.rotation);
        Leader.transform.localScale = LeaderPos.transform.localScale;
    }

    //Funcion barajear deck
    static void Barajear(GameObject[] mazo)
    {
        GameObject card;

        for(int i = 0; i < mazo.Length; i++)
        {
            int d = Random.Range(0, mazo.Length);
            card = mazo[i];
            mazo[i] = mazo[d];
            mazo[d] = card;
        }
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
                    if (Card_Campo[i] == null && card.GetComponent<General>().Type_Attack == "Melee")
                    {
                        card.GetComponent<General>().mover = true;
                        card.GetComponent<General>().obj = Melee_pos[i];
                        card.transform.rotation = Melee_pos[i].transform.rotation;
                        Card_Campo[i] = card;
                        hecho = true;
                        break;
                    }
                }
                
                //Verificar si queda casilla y si quedan invocar carta a distancia
                if(i >= 4 && i < 8 )
                {
                    if (Card_Campo[i] == null && card.GetComponent<General>().Type_Attack == "Range")
                    {
                        card.GetComponent<General>().mover = true;
                        card.GetComponent<General>().obj = Range_pos[i - 4];
                        card.transform.rotation = Range_pos[i - 4].transform.rotation;
                        Card_Campo[i] = card;
                        hecho = true;
                        break;
                    }
                }

                //Verificar si queda casilla y si quedan invocar carta de asedio
                if (i >= 8)
                {
                    if (Card_Campo[i] == null && card.GetComponent<General>().Type_Attack == "Asedio")
                    {
                        card.GetComponent<General>().mover = true;
                        card.GetComponent<General>().obj = Asedio_pos[i - 8];
                        card.transform.rotation = Asedio_pos[i - 8].transform.rotation;
                        Card_Campo[i] = card;
                        hecho = true;
                        break;
                    }
                }                                
            }

            //Verificar si la carta es clima
            if(card.GetComponent<General>().Type_Attack == "Wheather")
            {
                if (manager.clima != null)
                {
                    Destroy(manager.clima);
                }
                manager.clima = card;
                manager.clima.GetComponent<Cartas_Especiales>().Eliminar_Clima_influence();
                card.transform.SetPositionAndRotation(ClimaPos.transform.position, ClimaPos.transform.rotation);
                card.transform.localScale = ClimaPos.transform.localScale;
                hecho = true;
            }

            if(card.GetComponent<General>().Type_Attack == "Clearence")
            {
                card.transform.SetPositionAndRotation(Clearence_pos.transform.position, Clearence_pos.transform.rotation);
                card.transform.localScale = Clearence_pos.transform.localScale;
                card.GetComponent<Cartas_Especiales>().Despeje();
                Destroy(card, 3);//Destruir carta despues de usar el efecto
            }

            //Verificar si la carta es de incremento
            if(card.GetComponent<General>().Type_Attack == "Increase")
            {
                //Verificar tipo de aumento
                if(card.GetComponent<Cartas_Especiales>().Afectados == "Melee")
                {
                    if (gameObject.CompareTag("elven") && manager.aumentos[0] == null || gameObject.CompareTag("Cuevita-land") && manager.aumentos[2] == null)
                    {
                        card.transform.SetPositionAndRotation(Special_melee_pos.transform.position, Special_melee_pos.transform.rotation);
                        card.transform.localScale = Special_melee_pos.transform.localScale;
                        hecho = true;
                        if (gameObject.CompareTag("Elfo"))
                        {
                            manager.aumentos[0] = card;
                        }
                        else
                        {
                            manager.aumentos[2] = card;
                        }
                    }
                    
                }

                if (card.GetComponent<Cartas_Especiales>().Afectados == "Range")
                {
                    if (gameObject.CompareTag("elven") && manager.aumentos[1] == null || gameObject.CompareTag("Cuevita-land") && manager.aumentos[3] == null)
                    {
                        card.transform.SetPositionAndRotation(Special_range_pos.transform.position, Special_range_pos.transform.rotation);
                        card.transform.localScale = Special_range_pos.transform.localScale;
                        hecho = true;
                        if (gameObject.CompareTag("elven"))
                        {
                            manager.aumentos[1] = card;
                        }
                        else
                        {
                            manager.aumentos[3] = card;
                        }
                    }

                }             
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
                            if (manager.Cartas_Campo[v] == null && card.GetComponent<General>().Type_Attack != "Wheather" && card.GetComponent<General>().Type_Attack != "Increase")
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
