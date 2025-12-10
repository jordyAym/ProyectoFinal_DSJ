using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject mainMenu;

    public void btnOptions()
    {
        // funcionalidad de presionar botón Opciones
    }

    public void btnAtras()
    {
        // funcionalidad para regresar a Main menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Escena1");
    }

}
