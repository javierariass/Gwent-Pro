using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cartas_Unidad : MonoBehaviour
{
    public int atk;
    public int habilidad = 0;
    public GameObject Panel_De_Efecto,Boton_Efecto;
    GameManager Manager;

    private void Start()
    {
        Panel_De_Efecto = GameObject.FindGameObjectWithTag("Panel_efecto");
        Boton_Efecto = GameObject.FindGameObjectWithTag("Boton_Efecto");
        Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }
    private void OnMouseDown()
    {
        if (gameObject.GetComponent<General>().invocada && !Manager.Turn_Invoque && habilidad != 0)
        {
            if(gameObject.CompareTag("Elfo") && Manager.turno == 1 || gameObject.CompareTag("orc") && Manager.turno == 2)
            {
                Panel_De_Efecto.GetComponent<Animator>().SetBool("Activate", true); ;
                Boton_Efecto.GetComponent<Boton_Efecto>().Carta = GetComponent<Cartas_Unidad>();
            }         
        }
    }
    public void activar_habilidad()
    {
        switch (habilidad)
        {
            //Poner aumento en fila
            case 1:
                break;

            //Poner una carta clima
            case 2:
                break;

            //Eliminar la carta con mas poder del campo (propio o rival)
            case 3:
                int s = 0;
                while(Manager.Cartas_Campo[s].GetComponent<General>().Type_Card == "Gold" && Manager.Cartas_Campo[s] != null)
                {
                    s++;
                }
                Cartas_Unidad unidad = Manager.Cartas_Campo[s].GetComponent<Cartas_Unidad>();
                
                for(int i = 0; i < Manager.Cartas_Campo.Length; i++)
                {
                    if (Manager.Cartas_Campo[i] != null)
                    {
                        Cartas_Unidad uni = Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>();
                        if (unidad.atk < uni.atk && uni.gameObject.GetComponent<General>().Type_Card != "Gold")
                        {
                            unidad = uni;
                            s = i;
                        }
                    }
                    
                }
                Destroy(Manager.Cartas_Campo[s]);
                break;

            //Eliminar carta con menos poder del rival
            case 4:
                int d = 0;
                while (Manager.Cartas_Campo[d].GetComponent<General>().Type_Card == "Gold" && !Manager.Cartas_Campo[d].CompareTag("Elfo"))
                {
                    d++;
                }

                for (int i = 0; i < Manager.Cartas_Campo.Length; i++)
                {
                    if(Manager.Cartas_Campo[i] != null)
                    {
                        if (Manager.Cartas_Campo[i].CompareTag("Elfo"))
                        {
                            if (Manager.Cartas_Campo[i].GetComponent<Cartas_Unidad>().atk < Manager.Cartas_Campo[d].GetComponent<Cartas_Unidad>().atk)
                            {
                                d = i;
                            }
                        }
                    }
                   
                }
                Destroy(Manager.Cartas_Campo[d]);
                break;

            //Multiplica por n su ataque siendo n la cantidad de cartas iguales a ella en el campo
            case 5:
                int contador = 0;
                for (int i = 0; i < Manager.Cartas_Campo.Length; i++)
                {
                    if (Manager.Cartas_Campo[i] != null)
                    {
                        if (Manager.Cartas_Campo[i].CompareTag("Elfo"))
                        {
                            contador++;
                        }                     
                    }
                }
                atk *= contador;
                break;

            //Limpia la fila del campo (no vacia,propia o del rival) con menos unidades
            case 6:
               
                break;

            //Robar una carta
            case 7:
                GetComponent<General>().Mazo.GetComponent<Mazo>().Robar(1);
                break;

            //Calcula el promedio entre todas las cartas del campo (propia o del rival). luego iguala el poder de todas las cartas del campo (propia o del rival) a ese promedio
            case 8:
                break;
        }
    }
}
