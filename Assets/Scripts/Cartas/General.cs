using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class General : MonoBehaviour
{
    private RawImage Image_Card;
    private TextMeshProUGUI descripcion;
    public GameObject Mazo;
    public string Type_card;
    [TextArea(order = 5)] public string Lore;

    // Start is called before the first frame update
    void Start()
    {
        Mazo = GameObject.FindGameObjectWithTag("elven");
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
        Image_Card.texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        descripcion.text = Lore;
    }


    //Click para invocar carta
    private void OnMouseDown()
    {
        Mazo.GetComponent<Mazo>().Invocar(gameObject);
    }
}
