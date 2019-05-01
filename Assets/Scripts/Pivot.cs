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

    public float rotationZ;
    [SerializeField] private Transform equipmentPosition;

    private void Start()
    {
        //player = GameObject.Find("/PlayArea/Sam(Clone)");
        PlayerScript = player.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
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
            EmptyCurrentHand();
            spacegun = Instantiate(spacegunPrefab, new Vector3(), Quaternion.identity) as GameObject;
            spacegun.transform.parent = equipmentPosition.transform;
            spacegun.transform.position = equipmentPosition.position;
            spacegun.transform.localRotation = Quaternion.Euler(0f, 0f, -60f);
        }  
    }
    public void equipLavagun()
    {
        if(lavagun == null)
        {
            EmptyCurrentHand();
            lavagun = Instantiate(lavagunPrefab, new Vector3(), Quaternion.identity) as GameObject;
            lavagun.transform.parent = equipmentPosition.transform;
            lavagun.transform.position = equipmentPosition.position;
            lavagun.transform.localRotation = Quaternion.Euler(0f, 0f, -60f);
        }
    }

    public void EquipItem(GameObject obj)
    {
        EmptyCurrentHand();
        obj.transform.parent = equipmentPosition.transform;
        obj.transform.position = equipmentPosition.position;
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        obj.GetComponent<SpriteRenderer>().sortingLayerName = "frontTileLayer";

        //obj.transform.SetParent()
    }

    private void EmptyCurrentHand()
    {
        if (equipmentPosition.transform.childCount > 0)
        {
            Destroy(equipmentPosition.transform.GetChild(0).gameObject);
        }
    }
}
