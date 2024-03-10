using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    // Create a script here that would be attached to tthe canvas
    // Would reference the display ttext and below we would have simple functions
    // that would enable or disable the textt, tthis would be called in our computer componentt script
    #region Public
    public GameObject computerComponentUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayComputerComponentName(string name)
    { computerComponentUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name; }

    public void SetUserIntefaceActive(GameObject userInterface, bool value)
    { userInterface.SetActive(value); }
}