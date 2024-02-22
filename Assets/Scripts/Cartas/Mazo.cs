using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    public GameObject[] mazo = new GameObject[30];
    public GameObject[] Hand_Pos = new GameObject[10];
    private GameObject[] Hand = new GameObject[10];
    private GameObject[] Melee = new GameObject[4];
    private GameObject[] Range = new GameObject[4];
    private GameObject[] Asedio = new GameObject[4];
    private GameObject[] Melee_pos, Range_pos, Asedio_pos = new GameObject[4];

    private int carta_actual_deck = 0;
    // Start is called before the first frame update
    void Start()
    {
        Melee_pos = GameObject.FindGameObjectsWithTag("Melee_pos");
        Range_pos = GameObject.FindGameObjectsWithTag("Range_pos");
        Asedio_pos = GameObject.FindGameObjectsWithTag("Asedio_pos");
        Barajear(mazo);
        robar(10);
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
    public void robar(int cantidad)
    {
        for(int i = 0; i < cantidad; i++)
        {
            //Robar carta y poner en la mano
            if(Hand[i] == null)
            {
                Hand[i] = GameObject.Instantiate(mazo[carta_actual_deck], Hand_Pos[i].transform.position, Hand_Pos[i].transform.rotation);
                carta_actual_deck++;
            }
            
            //Destruir carta por no caber en la mano
            else
            {
                //Destroy
                break;
            }
        }
    }

    //Invocar carta
    public void Invocar(GameObject card)
    {
        bool hecho = false;
        for (int i = 0; i < 4; i++)
        {
            //Verificar si queda casilla y si quedan invocar carta cuerpo a cuerpo
            if(Melee[i] == null && card.GetComponent<General>().Type_card == "Melee")
            {
                card.transform.position = Melee_pos[i].transform.position;
                card.transform.localScale = Melee_pos[i].transform.localScale;
                Melee[i] = card;
                hecho = true;
                break;
            }

            //Verificar si queda casilla y si quedan invocar carta a distancia
            if (Range[i] == null && card.GetComponent<General>().Type_card == "Range")
            {
                card.transform.position = Range_pos[i].transform.position;
                card.transform.localScale = Range_pos[i].transform.localScale;
                Range[i] = card;
                hecho = true;
                break;
            }

            //Verificar si queda casilla y si quedan invocar carta de asedio
            if (Asedio[i] == null && card.GetComponent<General>().Type_card == "Asedio")
            {
                card.transform.position = Asedio_pos[i].transform.position;
                card.transform.localScale = Asedio_pos[i].transform.localScale;
                Asedio[i] = card;
                hecho=true;
                break;
            }
        }

        if(hecho)
        {
            for(int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == card)
                {
                    Hand[i] = null;
                }
            }
        }
    }
}
