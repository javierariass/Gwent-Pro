using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Function : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }
    public void Activate()
    {
        GetComponent<Animator>().SetBool("Activate", true);       
    }

    public void Desactivate()
    {
        GetComponent<Animator>().SetBool("Activate", false);
        gameManager.gameObject.GetComponent<Turn_by_Turn>().Turn_Switch();
    }
}
