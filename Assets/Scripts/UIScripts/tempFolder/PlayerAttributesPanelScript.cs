using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesPanelScript : MonoBehaviour {

    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;

    public Image attributeIcon;
    public Sprite[] attributePanelStatusSprite;

    public UIScript uiScript;

    public void SetPlayerAttributesPanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    public void UpdateHealth(float health)
    {
        healthBar.value = health;
        //if (health <= 0)
        //{
        //    attributeIcon.sprite = attributePanelStatusSprite[0];
        //}
        //else if (health <= 30)
        //{
        //    attributeIcon.sprite = attributePanelStatusSprite[1];
        //}
        //else if (health <= 60)
        //{
        //    attributeIcon.sprite = attributePanelStatusSprite[2];
        //}
        //else if (health <= 90)
        //{
        //    attributeIcon.sprite = attributePanelStatusSprite[3];
        //}
        //else
        //{
        //    attributeIcon.sprite = attributePanelStatusSprite[4];
        //}

    }

    public void UpdateStamina(float stamina)
    {
        staminaBar.value = stamina;
    }

    public void UpdateHunger(float hunger)
    {
        hungerBar.value = hunger;
    }
}
