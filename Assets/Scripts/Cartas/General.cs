using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class General : MonoBehaviour
{
    private RawImage Image_Card;
    private TextMeshProUGUI descripcion;
    [TextArea(order = 5)] public string Lore;
    // Start is called before the first frame update
    void Start()
    {
        Image_Card = GameObject.FindGameObjectWithTag("Image_Card").GetComponent<RawImage>();
        descripcion = GameObject.FindGameObjectWithTag("Texto_Card").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Image_Card.texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        descripcion.text = Lore;
    }
}
