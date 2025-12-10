using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetUISelector : MonoBehaviour
{
    public void LoadPlanet(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}