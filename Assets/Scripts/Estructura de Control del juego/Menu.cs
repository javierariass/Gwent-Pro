using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        //Boton para abir menu de pausa
        if (Input.GetKeyDown(KeyCode.Escape) && CompareTag("Button_Tag"))
        {
            menu.transform.localScale = Vector3.one;
        }
    }

    //Boton Start menu inicio
    public void Iniciar()
    {
        SceneManager.LoadScene(1);
    }

    //Boton de creditos
    public void Creditos()
    {
        SceneManager.LoadScene(2);
    }

    //Boton para las intrucciones
    public void Opciones()
    {
        SceneManager.LoadScene(3);
    }
    //Boton salir
    public void Exit()
    {
        Application.Quit();
    }
    //Boton para regresar a la pantalla de carga

    public void Volver()
    {
        SceneManager.LoadScene(0);
    }

    //Boton para mostrar el final del juego
    public void End_Game()
    {
        SceneManager.LoadScene(4);
    }
    
    //Boton para cerrar menu de pausa
    public void Cerrar_Panel()
    {
        menu.transform.localScale = Vector3.zero;
    }
}
