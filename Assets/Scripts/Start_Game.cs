using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Game : MonoBehaviour
{
    public Mazo mazo1,mazo2;
    public bool inicio;
    private int cant = 0;
    public RawImage[] image = new RawImage[10];
    public GameObject GameObject;

    //Boton para robar carta al dar click
    private void OnMouseDown()
    {
        if (!inicio)
        {
            mazo1.Robar(10);
            mazo2.Robar(10);
            inicio = true;
            for (int i = 0; i < image.Length; i++)
                {
                    image[i].texture = mazo1.Hand[i].GetComponent<SpriteRenderer>().sprite.texture;
                    image[i].transform.localScale = Vector3.one;
                    GameObject.transform.localScale = Vector3.one;
                }
        }
    }

    //Boton para mostrar lista de cartas para desechar 2
    public void Colocar()
    {
        
        if (GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().turno == 2 && GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().inicio2)
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].texture = mazo2.Hand[i].GetComponent<SpriteRenderer>().sprite.texture;
                image[i].transform.localScale = Vector3.one;
            }
            GameObject.transform.localScale = Vector3.one;
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().Turn_Invoque = true;
            cant = 0;
        }
    }

    //funcion para cambiar carta por otra
    public void Cambiar1(int num, Mazo mazos)
    {
        Destroy(mazos.Hand[num]);
        mazos.Hand[num] = null;
        mazos.Robar(1);
        image[num].texture = null;
        image[num].transform.localScale = Vector3.zero;
        cant++;
        if(cant == 2)
        {
            Cerrar();
        }
    }

    //funcionar Terminar fase de descarte
    public void Cerrar()
    {
        for (int i = 0; i < image.Length; i++)
        {
            if(image[i].texture != null)
            {
                image[i].transform.localScale = Vector3.zero;
                GameObject.transform.localScale = Vector3.zero;
            }
            
        }
        if(GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().turno == 1)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().inicio1 = false;
        }
        if (GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().turno == 2)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().inicio2 = false;
        }
        GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().Turn_Invoque = false;
    }
}
