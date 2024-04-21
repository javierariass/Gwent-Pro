using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class General : MonoBehaviour
{
    private RawImage Image_Card;
    public TextMeshProUGUI descripcion;
    public GameObject Mazo,Descripcion_GUI;
    public string Type_Attack,Type_Card,Name_Card;
    public bool invocada,clima_influence,aumento_influence,mover = false;
    public GameObject obj;
    public GameManager gameManager;
   
    // Start is called before the first frame update
    void Start()
    {
        Asignar_Mazo();       
        Image_Card = GameObject.FindGameObjectWithTag("Image_Card").GetComponent<RawImage>(); //Localizar elemento UI para mostrar imagen de la carta
        Descripcion_GUI = GameObject.FindGameObjectWithTag("lore");
        descripcion = GameObject.FindGameObjectWithTag("text_lore").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
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
        bool ya = false;
        if(!invocada)
        {
            invocada = Mazo.GetComponent<Mazo>().Invocar(gameObject);
            ya = true;
        }

        if (invocada && gameManager.Lure != null && Type_Card != "Lure" && !ya && !gameManager.Turn_Invoque)
        {
            if(gameManager.Lure.CompareTag(gameObject.tag) )
            {
                if(Type_Card == "Silver" || Type_Card == "Gold")
                {
                    GameObject card = gameManager.Lure;
                    mover = false;
                    card.transform.position = gameObject.transform.position;
                    card.transform.localScale = gameObject.transform.localScale;
                    card.transform.rotation = gameObject.transform.rotation;
                    gameObject.transform.position = Mazo.GetComponent<Mazo>().Hand_Pos[gameManager.Pos_lure].transform.position;
                    card.transform.parent = null;
                    if(CompareTag("Elfo"))
                    {
                        gameObject.transform.SetParent(gameManager.P1.transform);
                    }
                    else
                    {
                        gameObject.transform.SetParent(gameManager.P2.transform);
                    }
                    invocada = false;
                    gameManager.Turn_Invoque = true;
                    for (int i = 0; i < Mazo.GetComponent<Mazo>().Hand.Length; i++)
                    {
                        if (Mazo.GetComponent<Mazo>().Hand[i] == card)
                        {
                            Mazo.GetComponent<Mazo>().Hand[i] = gameObject;
                        }
                    }
                    for (int i = 0; i < gameManager.Cartas_Campo.Length; i++)
                    {
                        if (gameManager.Cartas_Campo[i] == gameObject)
                        {
                            gameManager.Cartas_Campo[i] = card;
                        }
                        gameManager.Turn_Invoque = true;
                    }
                }
                
            }               
        }
    }

}
