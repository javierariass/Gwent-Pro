using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cartas_Unidad : MonoBehaviour
{
    public int atk;
    public int habilidad = 0;
    public string hability;
    public bool Activado = false;
    GameManager Manager;

    private void Start()
    {
       Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(!Activado && GetComponent<General>().invocada)
        {
            StartCoroutine(Activacion_Time());
            Activado = true;
        }
    }
    private void OnMouseEnter()
    {
        GetComponent<General>().descripcion.text += "\n" + "Power: " + atk + "\n\n" + hability;
    }
    public void Activar_habilidad()
    {
        switch (habilidad)
        {
            //Habilidad de los Elfos

            case 1: //Robar una carta
                Manager.mazo1.GetComponent<Mazo>().Robar(1);
                break;
            case 2: //Elimina la carta con mas poder del campo
                GameObject card = null;
                int atk = 0;
                int pos = 0;
                for(int i = 0; i< Manager.Cartas_Campo.Length;i++)
                {
                    if(Manager.Cartas_Campo[i] != null)
                    {
                        if (Manager.Cartas_Campo[i].GetComponent<General>().Type_Card != "Gold")
                        {
                            if (Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk > atk)
                            {
                                card = Manager.Cartas_Campo[i];
                                atk = Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk;
                                pos = i;
                            }
                        }
                    }
                    
                }
                if(card != null)
                {
                    Destroy(card);
                    Manager.Cartas_Campo[pos] = null;
                }
                break;
            case 3: //Invoca una carta clima al azar de la mano
                for(int i = 0; i < Manager.mazo1.GetComponent<Mazo>().Hand.Length;i++)
                {
                    if(Manager.mazo1.GetComponent<Mazo>().Hand[i] != null)
                    {
                        if (Manager.mazo1.GetComponent<Mazo>().Hand[i].GetComponent<General>().Type_Attack == "Wheather")
                        {
                            Manager.Turn_Invoque = false;
                            if (Manager.mazo1.GetComponent<Mazo>().Invocar(Manager.mazo1.GetComponent<Mazo>().Hand[i]))
                            {
                                break;
                            }
                        }
                    }                   
                }
                Manager.Turn_Invoque = true;
                break;

            //Habilidad de los orcos
            case 4: //Multiplica su ataque por la cantidad de cartas igual a ella en el campo
                int contador = 1;
                for (int i = 0; i < Manager.Cartas_Campo.Length; i++)
                {
                    if (Manager.Cartas_Campo[i] != null)
                    {
                        if (Manager.Cartas_Campo[i].GetComponent<General>().Name_Card == GetComponent<General>().Name_Card)
                        {
                            if (Manager.Cartas_Campo[i] != gameObject)
                            {
                                Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk += GetComponent<Cartas_Unidad>().atk;
                                contador++;
                            }
                            
                        }
                    }
                }
                GetComponent<Cartas_Unidad>().atk *= contador;
                break;        
            case 5: //Elimina la carta con menos poder del rival
                GameObject cards = null;
                int atks = 100;
                int poss = 0;
                for (int i = 0; i < Manager.Cartas_Campo.Length; i++)
                {
                    if (Manager.Cartas_Campo[i] != null)
                    {
                        if (Manager.Cartas_Campo[i].GetComponent<General>().Type_Card != "Gold")
                        {
                            if(Manager.Cartas_Campo[i].CompareTag("Elfo"))
                            {
                                if (Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk < atks)
                                {
                                    cards = Manager.Cartas_Campo[i];
                                    atks = Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk;
                                    poss = i;
                                }
                            }                            
                        }
                    }

                }
                if (cards != null)
                {
                    Destroy(cards);
                    Manager.Cartas_Campo[poss] = null;
                }
                break;
            case 6: //Invoca una carta aumento de la mano al azar;
                for (int i = 0; i < Manager.mazo2.GetComponent<Mazo>().Hand.Length; i++)
                {
                    if (Manager.mazo2.GetComponent<Mazo>().Hand[i] != null)
                    {
                        Manager.Turn_Invoque = false;
                        if (Manager.mazo2.GetComponent<Mazo>().Hand[i].GetComponent<General>().Type_Attack == "Increase")
                        {
                            if (Manager.mazo2.GetComponent<Mazo>().Invocar(Manager.mazo2.GetComponent<Mazo>().Hand[i]))
                            {
                                break;
                            }
                        }
                    }                  
                }
                Manager.Turn_Invoque = true;
                break;
        }
    }

    IEnumerator Activacion_Time()
    {
        yield return new WaitForSeconds(1f);
        Activar_habilidad();
    }
}
