using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject playerInvoPanel;
    public GameObject craftingPanel;
    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;
    public RectTransform fadePanel;
    private Animator fadePanelAnimator;

    public Image attributeIcon;
    public Sprite [] attributePanelStatusSprite;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        playerInvoPanel.gameObject.SetActive(false);
        craftingPanel.gameObject.SetActive(false);
        fadePanelAnimator = fadePanel.GetComponent<Animator>();
    }

    public void TogglePlayerInventory()
    {
        playerInvoPanel.gameObject.SetActive(!playerInvoPanel.gameObject.activeInHierarchy);
        playerInvoPanel.GetComponent<PlayerInventoryPanelScript>().UpdatePlayerInventoryPanel();
    }

    public void ToggleCraftingPanel()
    {
        craftingPanel.gameObject.SetActive(!craftingPanel.gameObject.activeInHierarchy);
        craftingPanel.GetComponent<CraftingPanelScript>().UpdateCraftingPanel();
    }

    public void UpdateHealth(float health)
    {
        healthBar.value = health;
        if(health <= 0)
        {
            attributeIcon.sprite = attributePanelStatusSprite[0];
        }
        else if(health <= 30)
        {
            attributeIcon.sprite = attributePanelStatusSprite[1];
        }
        else if (health <= 60)
        {
            attributeIcon.sprite = attributePanelStatusSprite[2];
        }
        else if (health <= 90)
        {
            attributeIcon.sprite = attributePanelStatusSprite[3];
        }
        else
        {
            attributeIcon.sprite = attributePanelStatusSprite[4];
        }

    }

    public void UpdateStamina(float stamina )
    {
        staminaBar.value = stamina;
    }

    public void UpdateHunger(float hunger)
    {
        hungerBar.value = hunger;
    }

    public void FadeInScene()
    {
        fadePanelAnimator.SetBool("sceneDark", false);
    }

    public void FadeOutScene()
    {
        fadePanelAnimator.SetBool("sceneDark", true);
    }

}
