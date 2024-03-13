using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Game : MonoBehaviour
{

    public RawImage[] cartas = new RawImage[10];
    public Mazo mazo;

    private void Start()
    {
        Mostrar_Cartas();
    }
    public void Mostrar_Cartas()
    {
        for(int i = 0; i < cartas.Length; i++)
        {
            cartas[i].texture = mazo.Hand[i].GetComponent<SpriteRenderer>().sprite.texture;
        }    
    }
    public void Cambiar_cartas()
    {
        for(int i = 0; i < cartas.Length; i++)
        {
            if(cartas[i].gameObject.GetComponent<Selected_card>().selected)
            {
                mazo.Hand[i] = null;
                mazo.Robar(1);
            }
        }

        gameObject.SetActive(false);

    }
    
   
}
