using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{

    public GameObject player;
    public Player PlayerScript;

    public GameObject swordPrefab;
    private GameObject sword;

    public GameObject katanaPrefab;
    private GameObject katana;

    public GameObject spacegunPrefab;
    private GameObject spacegun;

    public GameObject lavagunPrefab;
    private GameObject lavagun;

    public GameObject gyrogunPrefab;
    private GameObject gyrogun;

    public float rotationZ;
    [SerializeField] private Transform equipmentPosition;

    private bool isAttacking = false;
    private bool isMeleeEquipped = false;

    float rotation = 90;
    float attackSpeed = 1;

    private void Start()
    {
        //player = GameObject.Find("/PlayArea/Sam(Clone)");
        //PlayerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            if (isMeleeEquipped)
            {
                if (PlayerScript.getFacingDirection())
                    transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                else
                    transform.rotation = Quaternion.Euler(0f, 180f, 270f);
            }
            else
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
                    if (rotationZ >= 90 && rotationZ <= 180)
                    {
                        //transform.Rotate(0f, 180, 0f);
                        transform.rotation = Quaternion.Euler(0f, 180f, -rotationZ + 180);
                    }
                    else if (rotationZ <= -90 && rotationZ >= -180)
                    {
                        transform.rotation = Quaternion.Euler(0f, 180f, -rotationZ + 180);
                    }

                }
            }
        }
        else
        {

            if (PlayerScript.getFacingDirection())
            {
                rotation -= (float)(attackSpeed - (2 * Time.deltaTime));
                transform.rotation = Quaternion.Euler(0f, 0f, rotation);

                if (rotation <= -90)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    isAttacking = false;
                    rotation = 0;
                    equipmentPosition.GetComponentInChildren<MeleeWeapon>().endAttack();

                }
            }
            else
            {
                //Debug.Log("rotate facing left");
                //rotation += (float)(attackSpeed - (2 * Time.deltaTime));
                rotation -= (float)(attackSpeed - (2 * Time.deltaTime));
                transform.rotation = Quaternion.Euler(0f, 180f, rotation);
                if (rotation <= -90)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 270f);
                    isAttacking = false;
                    rotation = 0;
                    equipmentPosition.GetComponentInChildren<MeleeWeapon>().endAttack();
                }
            }

        }


    }

    public void MeleeRotate()
    {

        if (!isAttacking)
        {
            isAttacking = true;
            equipmentPosition.GetComponentInChildren<MeleeWeapon>().startAttack();

            if (PlayerScript.getFacingDirection())
            {
                rotation = 0;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                rotation = 0;
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

        }

    }

    public void EquipSword()
    {
        if (sword == null)
        {
            EmptyCurrentHand();
            isMeleeEquipped = true;
            attackSpeed = swordPrefab.GetComponent<MeleeWeapon>().attackSpeed;

            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            sword = Instantiate(swordPrefab, new Vector3(), Quaternion.identity);
            sword.transform.position = equipmentPosition.transform.position;
            sword.transform.SetParent(equipmentPosition);
            //if (!PlayerScript.getFacingDirection())
                //sword.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            
            
        }
    }

    public void EquipKatana()
    {
        if (katana == null)
        {
            EmptyCurrentHand();
            isMeleeEquipped = true;
            attackSpeed = katanaPrefab.GetComponent<MeleeWeapon>().attackSpeed;

            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            katana = Instantiate(katanaPrefab, new Vector3(), Quaternion.identity);
            katana.transform.position = equipmentPosition.position;
            katana.transform.parent = equipmentPosition.transform;
            //if (!PlayerScript.getFacingDirection())
                //katana.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void equipSpacegun()
    {
        if (spacegun == null)
        {
            EmptyCurrentHand();
            isMeleeEquipped = false;
            spacegun = Instantiate(spacegunPrefab, new Vector3(), Quaternion.identity) as GameObject;
            spacegun.transform.parent = equipmentPosition.transform;
            spacegun.transform.position = equipmentPosition.position;
            spacegun.transform.localRotation = Quaternion.Euler(0f, 0f, -60f);
        }  

    }
    public void equipLavagun()
    {
        if (lavagun == null)
        {
            EmptyCurrentHand();
            isMeleeEquipped = false;
            lavagun = Instantiate(lavagunPrefab, new Vector3(), Quaternion.identity) as GameObject;
            lavagun.transform.parent = equipmentPosition.transform;
            lavagun.transform.position = equipmentPosition.position;
            lavagun.transform.localRotation = Quaternion.Euler(0f, 0f, -60f);

        }
    }
    public void equipGyrogun()
    {
        if (gyrogun == null)
        {
            EmptyCurrentHand();
            gyrogun = Instantiate(gyrogunPrefab, new Vector3(), Quaternion.identity) as GameObject;
            gyrogun.transform.parent = equipmentPosition.transform;
            gyrogun.transform.position = equipmentPosition.position;
            gyrogun.transform.localRotation = Quaternion.Euler(0f, 0f, -60f);

        }
    }
    public void EquipItem(GameObject obj)
    {
        EmptyCurrentHand();
        isMeleeEquipped = false;
        obj.transform.parent = equipmentPosition.transform;
        //obj.transform.position = equipmentPosition.position;
        obj.transform.localPosition = new Vector3(0.3f, 0, 0);
        obj.transform.localRotation = Quaternion.Euler(0f, 0f, -103f);
        obj.GetComponent<SpriteRenderer>().sortingOrder = 17;
    }

    private void EmptyCurrentHand()
    {
        if (equipmentPosition.transform.childCount > 0)
        {
            Destroy(equipmentPosition.transform.GetChild(0).gameObject);
        }
    }
}
