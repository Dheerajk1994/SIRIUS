using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{


    public GameObject sam;
    CharacterController2D CharacterController2DScript;
    bool facingRight;
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        CharacterController2DScript = sam.GetComponent<CharacterController2D>();
        facingRight = CharacterController2DScript.getDirectionFacing();
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Debug.Log(rotationZ);
        //Debug.Log(script.getDirectionFacing());
        //transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
 
        if (facingRight)
        {
            if (rotationZ <= 90 && rotationZ >= -90)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            }

        }
        else
        {
            if (rotationZ >= 90 && rotationZ <=180)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 180);
            }
            else if(rotationZ <= -90 && rotationZ >= -180)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 180);
            }
            
        }
    }
}
