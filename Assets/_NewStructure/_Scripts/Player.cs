using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterFinal 
{
    public GameManagerScript gameManagerScript;
    public UIScript uiScript;
    public InputManagerScript inputManagerScript;

    //public GameObject rotatingArmPrefab;
    //public GameObject rotatingArmObject;
    public GameObject equippedItem;

    [SerializeField]private GameObject rotatingArm;
    private Pivot rotatingArmScript;


    private static Player instance;
    //there should be only one player
    // void Awake(){
    //     if(instance == null){
    //         instance = this;
    //     }
    //     else if(instance != this){
    //         Destroy(this);
    //     }
    // }

    public void SetPlayerScript(GameManagerScript gmScript, UIScript uScript, InputManagerScript inputScript)
    {
        gameManagerScript = gmScript;
        uiScript = uScript;
        inputManagerScript = inputScript;
    }

    public static Player Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public Rigidbody2D MyRigidbody { get; set; }


    public bool Jump        { get; set; }
    public bool OnGround    { get; set; }

    public override bool IsDead
    {
        get
        {
            return currentHealth <= 0.0;
        }
    }

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    /*
    private bool isGrounded;
    private bool jump;
    private bool jumpAttack;
    */

	// Use this for initialization
	protected override void Start () 
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
        rotatingArmScript = rotatingArm.GetComponent<Pivot>();


    }

    protected override void Update()
    {
        base.Update();
        HandleInput();
        //generateRotatingArm();

    }

    // Update is called once per frame
    void FixedUpdate () 
    {
        float horizontal = Input.GetAxis("Horizontal");
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        //HandleAttacks();
        HandleLayers();
        Flip(horizontal);
        //ResetValues();
	}

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
            //jump = true;
        }
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        //myAnimator.ResetTrigger("jump");
                        //myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            this.ChangeDirection();
        }
    }

    private void HandleMovement(float horizontal) 
    {
        //Debug.Log(movementSpeed);
        if (MyRigidbody.velocity.y <0)
        {
            MyAnimator.SetBool("land", true); 
        }
        if (!Attack && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y); 
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            //MyRigidbody.AddForce(new Vector2(0, jumpForce)); 
            MyRigidbody.AddForce(Vector2.up * jumpForce); 
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("Player.TakeDamage: not implemented");
    } 
    
    public void HandleEquip()
    {
        if (equippedItem != null)
            Destroy(equippedItem.gameObject);


       switch(inputManagerScript.hotbarPanel.GetEquippedSlot())
        {
            case 0:
                MyAnimator.SetBool("rotatingArm", false);
                //clearArm();
                rotatingArm.gameObject.SetActive(false);
                Debug.Log("Nothing equipped");
                break;
            case 800:
                rotatingArm.gameObject.SetActive(true);
                rotatingArm.GetComponent<Pivot>().EquipSword();
                equippedItem = Instantiate(rotatingArmScript.swordPrefab);
                Debug.Log("Sword equipped");
                break;
            case 801:
                rotatingArm.gameObject.SetActive(true);
                rotatingArm.GetComponent<Pivot>().EquipKatana();
                equippedItem = Instantiate(rotatingArmScript.katanaPrefab);
                Debug.Log("Katana equipped");
                break;
            case 900:
                //GenerateRotatingArm();
                rotatingArm.gameObject.SetActive(true);
                rotatingArm.GetComponent<Pivot>().equipSpacegun();
                Debug.Log("Spacegun equipped");
                break;
            case 901:
                //GenerateRotatingArm();
                rotatingArm.gameObject.SetActive(true);
                rotatingArm.GetComponent<Pivot>().equipLavagun();
                Debug.Log("Lavagun equipped");
                break;
            default:
                GameObject obj = new GameObject();
                obj.AddComponent<SpriteRenderer>().sprite = InventorySpritesScript.instance.GetSprite(2);
                rotatingArm.gameObject.SetActive(true);
                rotatingArm.GetComponent<Pivot>().EquipItem(obj);
                break;

        }   
    }

    public void MeleeAttack()
    {
        rotatingArmScript.MeleeRotate(equippedItem.GetComponent<WeaponClass>().attackSpeed);
    }

    //public void GenerateRotatingArm()
    //{
    //    if (GameObject.Find("/PlayArea/Sam(Clone)/RotatingArm(Clone)") == null)
    //    {
    //        rotatingArmObject = Instantiate(rotatingArmPrefab, new Vector3(), Quaternion.identity) as GameObject;
    //        rotatingArmObject.transform.parent = transform;
    //        rotatingArmObject.transform.localPosition = new Vector3(-0.197f, -0.43f);
    //        MyAnimator.SetBool("rotatingArm", true);
    //    }
    //}

    //public Pivot RotatingArm()
    //{
    //    return rotatingArmObject.GetComponent<Pivot>();
    //}

    //public void clearArm()
    //{
    //    GameObject[] holder = 
    //    if (rotatingArmObject != null)
    //    {
    //        Destroy(rotatingArmObject);
    //    }
        
    //}

}
