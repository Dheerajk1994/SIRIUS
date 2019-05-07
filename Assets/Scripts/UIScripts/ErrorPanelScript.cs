using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanelScript : MonoBehaviour {

    [SerializeField] private Text errorDescriptionText;
    [SerializeField] private Button okButton;

    public void SetErrorPanel()
    {
        this.gameObject.SetActive(false);
        okButton.onClick.AddListener(OkButtonClicked);
    }
    
    public void SetError(string error)
    {
        errorDescriptionText.text = error;
        this.gameObject.SetActive(true);
    }

    public void OkButtonClicked()
    {
        this.gameObject.SetActive(false);
    }


}
