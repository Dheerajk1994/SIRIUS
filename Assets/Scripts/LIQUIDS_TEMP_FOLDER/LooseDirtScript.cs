using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseDirtScript : LiquidScript
{
    protected override void UpdateLiquidPosition()
    {
        Vector2 newPos = currentPos;
        if (!liquidManagerScript.CheckIfTile((ushort)(currentPos.x), (ushort)(currentPos.y - 1)))//check bot
        {
            newPos = new Vector2((ushort)(currentPos.x), (ushort)currentPos.y - 1);
            tilesTravelled = 0;
        }
        else if(flowDirection == 0)
        {
            if(!liquidManagerScript.CheckIfTile((ushort)(currentPos.x - 1), (ushort)(currentPos.y))
                &&
               !liquidManagerScript.CheckIfTile((ushort)(currentPos.x - 1), (ushort)(currentPos.y - 1)))
            {
                newPos = new Vector2((ushort)(currentPos.x - 1), (ushort)currentPos.y);
            }
            else if (!liquidManagerScript.CheckIfTile((ushort)(currentPos.x + 1), (ushort)(currentPos.y))
                &&
               !liquidManagerScript.CheckIfTile((ushort)(currentPos.x + 1), (ushort)(currentPos.y - 1)))
            {
                newPos = new Vector2((ushort)(currentPos.x + 1), (ushort)currentPos.y);
                flowDirection = 1;
            }
        }
        else
        {
            if (!liquidManagerScript.CheckIfTile((ushort)(currentPos.x + 1), (ushort)(currentPos.y))
                &&
               !liquidManagerScript.CheckIfTile((ushort)(currentPos.x + 1), (ushort)(currentPos.y - 1)))
            {
                newPos = new Vector2((ushort)(currentPos.x + 1), (ushort)currentPos.y);
            }
            else if (!liquidManagerScript.CheckIfTile((ushort)(currentPos.x - 1), (ushort)(currentPos.y))
                &&
               !liquidManagerScript.CheckIfTile((ushort)(currentPos.x - 1), (ushort)(currentPos.y - 1)))
            {
                newPos = new Vector2((ushort)(currentPos.x - 1), (ushort)currentPos.y);
                flowDirection = 0;
            }
        }




        if (currentPos != newPos)
        {
            liquidManagerScript.PlaceTile((ushort)currentPos.x, (ushort)currentPos.y, 0);//set tile at old pos to 0
            currentPos = newPos;
            this.transform.position = currentPos;
            liquidManagerScript.PlaceTile((ushort)currentPos.x, (ushort)currentPos.y, 2);//set tile at new pos to 2
            tilesTravelled++;
            if (tilesTravelled > 50)
            {
                liquidManagerScript.PlaceTile((ushort)currentPos.x, (ushort)currentPos.y, 0);
                Destroy(this.gameObject);
            }
        }
    }
}
