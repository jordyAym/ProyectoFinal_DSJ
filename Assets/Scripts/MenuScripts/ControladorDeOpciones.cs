using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeOpciones : MonoBehaviour
{
    // Ya tendrás métodos como PlayGame(), Quit(), etc.

    // Este método carga tu GalaxyMapScene:
    public void cargarOpcion(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
