using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas_Leader : MonoBehaviour
{
    private bool activated = false;
    public string Habilidad;
    public int num;
    private void OnMouseEnter()
    {
        GetComponent<General>().descripcion.text += "\n" + Habilidad;
    }
    private void OnMouseDown()
    {
        if(num == 1)
        {

        }

        if(num == 2)
        {

        }
    }
}
