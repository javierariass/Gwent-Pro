using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.transform.localScale = Vector3.one;
        }
    }
    public void Iniciar()
    {
        SceneManager.LoadScene(1);
    }

    public void Creditos()
    {
        SceneManager.LoadScene(2);
    }

    public void Opciones()
    {
        SceneManager.LoadScene(3);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Volver()
    {
        SceneManager.LoadScene(0);
    }

    public void End_Game()
    {
        SceneManager.LoadScene(4);
    }

    public void Cerrar_Panel()
    {
        menu.transform.localScale = Vector3.zero;
    }
}
