#if UNITY_EDITOR
using UnityEditor;
using System.Linq;

[InitializeOnLoad]
public static class AutoAddPlanetScenes
{
    static AutoAddPlanetScenes()
    {
        // Busca todas las escenas bajo Assets/Scenes/PlanetScenes
        string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes/PlanetScenes" });
        var planetScenes = guids
            .Select(g => AssetDatabase.GUIDToAssetPath(g))
            .Select(path => new EditorBuildSettingsScene(path, true))
            .ToList();

        // También puedes conservar otras escenas ya en Build Settings si lo deseas:
        var existing = EditorBuildSettings.scenes
            .Where(s => !s.path.StartsWith("Assets/Scenes/PlanetScenes/"))
            .ToList();

        EditorBuildSettings.scenes = existing.Concat(planetScenes).ToArray();
    }
}
#endif
