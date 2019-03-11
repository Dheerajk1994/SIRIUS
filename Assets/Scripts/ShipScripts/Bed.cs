using UnityEngine;

public class Bed : Interactable {
    public GameObject player;
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
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
    }
}
