using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class General : MonoBehaviour
{
    private RawImage Image_Card;
    private TextMeshProUGUI descripcion;
    public GameObject Mazo,UI_Descripcion;
    public string Type_card;
    private bool invocada = false;

    [TextArea(order = 10)] public string Lore;

    // Start is called before the first frame update
    void Start()
    {
        Mazo = GameObject.FindGameObjectWithTag("elven");
        UI_Descripcion = GameObject.FindGameObjectWithTag("UI_Lore");
        Image_Card = GameObject.FindGameObjectWithTag("Image_Card").GetComponent<RawImage>(); //Localizar elemento UI para mostrar imagen de la carta
        descripcion = GameObject.FindGameObjectWithTag("Texto_Card").GetComponent<TextMeshProUGUI>(); //Localizar elemento UI para mostrar descripcion de la carta
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Funcion colocar mouse encima de la carta para ver su descripcion

    private void OnMouseEnter()
    {
        Image_Card.gameObject.transform.localScale = new Vector2(1f, 1f);
        UI_Descripcion.transform.localScale = new Vector2(1f, 1f);
        Image_Card.texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        descripcion.text = Lore;
    }

    private void OnMouseExit()
    {
        Image_Card.gameObject.transform.localScale = new Vector2(0f, 0f);
        UI_Descripcion.gameObject.transform.localScale = new Vector2(0f, 0f);
    }

    //Click para invocar carta
    private void OnMouseDown()
    {
        GameManager manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        if(!invocada)
        {
            if(gameObject.CompareTag("Elfo") && manager.turno == 1)
            {
                invocada = Mazo.GetComponent<Mazo>().Invocar(gameObject);
                if(invocada)//Verificar si la carta fue invocada antes de sumar el poder
                {
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().Power_Elf(gameObject.GetComponent<Cartas_Unidad>().atk);
                }               
            }
        }
    }
}
