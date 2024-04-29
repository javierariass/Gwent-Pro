using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas_Leader : MonoBehaviour
{
    private bool activated = false;
    public string Habilidad;
    public int num;
    public GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    //Funcion para mostrar habilidad del lider en la descripcion del UI
    private void OnMouseEnter()
    {
        GetComponent<General>().descripcion.text += "\n" + Habilidad;
    }
    //Funcion para activar habilidad del lider
    private void OnMouseDown()
    {
        //Lider Elfico
        if (num == 1 && manager.turno == 1)
        {
            if (!activated)
            {
                for (int i = 0; i < manager.aumentos.Length; i++)
                {
                    if (manager.aumentos[i] != null)
                    {
                        manager.aumentos[i].GetComponent<Cartas_Especiales>().Eliminar_Aumento_influence();
                        Destroy(manager.aumentos[i]);
                        manager.aumentos[i] = null;
                    }
                }
                activated = true;
            }
        }

        //Lider Orco

        if (num == 2 && manager.turno == 2 && !manager.Turn_Invoque)
        {
            if (!activated)
            {
                manager.mazo2.GetComponent<Mazo>().Robar(1);
                activated = true;
            }
        }
        manager.Turn_Invoque = true;
    }
}
