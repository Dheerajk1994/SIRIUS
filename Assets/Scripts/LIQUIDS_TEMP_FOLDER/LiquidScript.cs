using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LiquidScript : MonoBehaviour {

    protected LiquidManagerScript liquidManagerScript;
    protected Vector2 currentPos;
    protected ushort flowDirection = 0;
    protected ushort tilesTravelled = 0;

    private void Start()
    {
        //createTerrainScript = CreateTerrainScript.instance;
        currentPos = this.transform.position;
        liquidManagerScript = LiquidManagerScript.instance;
        flowDirection = (ushort)UnityEngine.Random.Range(0, 2);
    }

    private void FixedUpdate()
    {
        UpdateLiquidPosition();
    }

    protected abstract void UpdateLiquidPosition();
}
