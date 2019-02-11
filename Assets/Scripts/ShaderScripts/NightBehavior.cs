using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class NightBehavior : MonoBehaviour
{

    public Material material;
    public Color orange = new Color(1.0f, 1.0f, 1.0f, 1f);
    [Range(0,1)]
    public float delta = 0.2f;
    //Globar reference used to store all game data
    public GameObject player;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Vector2 pos = player.transform.position;
        Vector4 v1 = Camera.main.WorldToViewportPoint(pos);

        material.SetVector("_Orange", orange);
        material.SetFloat("_Delta", delta);

        v1.z = 0.5f;
        v1.w = 1;
        material.SetVector("_Player", v1);//come back to this


        Graphics.Blit(source, destination, material);
    }
}
