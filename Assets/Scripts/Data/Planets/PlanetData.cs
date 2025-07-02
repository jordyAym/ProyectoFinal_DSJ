using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet/PlanetData")]
public class PlanetData : ScriptableObject
{
    public string planetName;
    public float gravity;
    public Material skybox;
    public Color fogColor;
    public AudioClip ambientAudio;
}
