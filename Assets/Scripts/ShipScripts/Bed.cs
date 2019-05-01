using UnityEngine;

public class Bed : Interactable {

    public PlayerScript playerScript;

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        playerScript.healState = true;
        Debug.Log("ZZZ");
    }

    public void Start()
    {
        
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerScript = player.GetComponent<PlayerScript>();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            isInteracting = false;
            playerScript.healState = false;
        }
    }
}
