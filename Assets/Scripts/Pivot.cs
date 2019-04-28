using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{

    public GameObject player;
    public Player PlayerScript;

    public GameObject spacegunPrefab;
    public GameObject spacegun;

    public GameObject lavagunPrefab;
    public GameObject lavagun;

      
    private void Start()
    {
        player = GameObject.Find("/PlayArea/Sam(Clone)");
        PlayerScript = player.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (PlayerScript.getFacingDirection())
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
                //transform.Rotate(0f, 180, 0f);
                transform.rotation = Quaternion.Euler(0f, 180f, -rotationZ+180) ;
            }
            else if(rotationZ <= -90 && rotationZ >= -180)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, -rotationZ+180);
            }
            
        }
    }
    public void equipSpacegun()
    {
        if(spacegun == null)
        {
            spacegun = Instantiate(spacegunPrefab, new Vector3(), Quaternion.identity) as GameObject;
            spacegun.transform.parent = GameObject.Find("/PlayArea/Sam(Clone)/RotatingArm(Clone)/arm").transform;
            spacegun.transform.localPosition = new Vector3(0.359f,-0.421f);
            spacegun.transform.localRotation = Quaternion.Euler(.48f, -180, -268);
        }
        
    }
}
