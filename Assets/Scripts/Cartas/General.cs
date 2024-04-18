using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class General : MonoBehaviour
{
    private RawImage Image_Card;
    public TextMeshProUGUI descripcion;
    public GameObject Mazo,Descripcion_GUI;
    public string Type_Attack,Type_Card,Name_Card;
    public bool invocada,clima_influence,aumento_influence,mover = false;
    public GameObject obj;
 
   
    // Start is called before the first frame update
    void Start()
    {
        Asignar_Mazo();       
        Image_Card = GameObject.FindGameObjectWithTag("Image_Card").GetComponent<RawImage>(); //Localizar elemento UI para mostrar imagen de la carta
        Descripcion_GUI = GameObject.FindGameObjectWithTag("lore");
        descripcion = GameObject.FindGameObjectWithTag("text_lore").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(mover)
        {
            transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, 6 * Time.deltaTime);           
        }
    }
    //Asignar a que deck pertenece la carta
    private void Asignar_Mazo()
    {
        if (gameObject.CompareTag("Elfo"))
        {
            Mazo = GameObject.FindGameObjectWithTag("Lothlorien");

        }
        if (gameObject.CompareTag("orc"))
        {
            Mazo = GameObject.FindGameObjectWithTag("Gorthul");

        }
    }


    //Funcion colocar mouse encima de la carta para ver su descripcion

    private void OnMouseEnter()
    {
        Image_Card.transform.localScale = new Vector2(1f, 1f);      
        Image_Card.texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        descripcion.transform.localScale = new Vector2(1f, 1f);
        Descripcion_GUI.transform.localScale = new Vector2(1f, 1f);
        descripcion.text = "Name: " + Name_Card + "\n" + "Faccion: " + Mazo.tag + "\n" + "Type Card: " + Type_Card + "\n" + "Type: " + Type_Attack;
      
    }

    private void OnMouseExit()
    {
        Desactivar_Descripcion();
    }

    public void Desactivar_Descripcion()
    {
       Image_Card.transform.localScale = new Vector2(0f, 0f);
        Descripcion_GUI.transform.localScale = new Vector2(0f, 0f);
      
    }

    //Click para invocar carta
    private void OnMouseDown()
    {       
        if(!invocada)
        {
            invocada = Mazo.GetComponent<Mazo>().Invocar(gameObject);                                       
        }
    }
}
