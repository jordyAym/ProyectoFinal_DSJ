using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeOpciones : MonoBehaviour
{
    // Ya tendr�s m�todos como PlayGame(), Quit(), etc.

    // Este m�todo carga tu GalaxyMapScene:
    public void cargarOpcion(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
