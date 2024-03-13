using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cartas_Unidad : MonoBehaviour
{
    public int atk;
    public int habilidad = 0;
    public GameObject Panel_De_Efecto;

    private void Start()
    {
        Panel_De_Efecto = GameObject.FindGameObjectWithTag("Panel_efecto");
    }
    private void Update()
    {
        if (gameObject.GetComponent<General>().invocada)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Panel_De_Efecto.transform.localScale = Vector3.one;
                GetComponent<General>().Desactivar_Descripcion();
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
                break;

            //Eliminar carta con menos poder del rival
            case 4:
                break;

            //Multiplica por n su ataque siendo n la cantidad de cartas iguales a ella en el campo
            case 5:
                break;

            //Limpia la fila del campo (no vacia,propia o ddel rival) con menos unidades
            case 6:
                break;

            //Robar una carta
            case 7:
                break;

            //Calcula el promedio entre todas las cartas del campo (propia o del rival). luego iguala el poder de todas las cartas del campo (propia o del rival) a ese promedio
            case 8:
                break;
        }
    }
}
