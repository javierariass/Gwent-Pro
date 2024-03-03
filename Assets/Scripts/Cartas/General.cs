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
    public bool invocada,clima_influence = false;

    [TextArea(order = 10)] public string Lore;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.CompareTag("Elfo"))
        {
            Mazo = GameObject.FindGameObjectWithTag("elven");

        }
        if (gameObject.CompareTag("orc"))
        {
            Mazo = GameObject.FindGameObjectWithTag("Cuevita-land");

        }

        UI_Descripcion = GameObject.FindGameObjectWithTag("UI_Lore");
        Image_Card = GameObject.FindGameObjectWithTag("Image_Card").GetComponent<RawImage>(); //Localizar elemento UI para mostrar imagen de la carta
        descripcion = GameObject.FindGameObjectWithTag("Texto_Card").GetComponent<TextMeshProUGUI>(); //Localizar elemento UI para mostrar descripcion de la carta
        
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
            invocada = Mazo.GetComponent<Mazo>().Invocar(gameObject);                                       
        }
    }
}
