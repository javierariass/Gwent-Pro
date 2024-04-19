using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Game : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Menu());
    }

   IEnumerator Menu()
    {
        yield return new WaitForSeconds(26);
        SceneManager.LoadScene(0);
    }
}
