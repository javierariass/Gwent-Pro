using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton_Efecto : MonoBehaviour
{
    public Cartas_Unidad Carta;

    public void Activar()
    {
        Carta.Activar_habilidad();
        Cerrar_Panel();

    }

    public void Abrir_Panel()
    {
        GetComponentInParent<Animator>().SetBool("Activate", true);
    }
    public void Cerrar_Panel()
    {
        GetComponentInParent<Animator>().SetBool("Activate", false);
    }
}
