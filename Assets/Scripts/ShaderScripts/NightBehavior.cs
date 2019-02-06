using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class NightBehavior : MonoBehaviour
{

    public Material material;

    //Globar reference used to store all game data
    public GameObject player;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Vector2 pos = player.transform.position;
        Vector4 v1 = Camera.main.WorldToViewportPoint(pos);
        Vector4 orange = new Vector4(1.0f, 1.0f, 1.0f, 1f);
        material.SetVector("_Orange", orange);

        v1.z = 0.5f;
        v1.w = 1;
        material.SetVector("_Player", v1);//come back to this


        Graphics.Blit(source, destination, material);
    }
}
