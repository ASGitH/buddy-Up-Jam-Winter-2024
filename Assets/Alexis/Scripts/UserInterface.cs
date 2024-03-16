using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;   

public class UserInterface : MonoBehaviour
{
    #region Public
    public GameObject computerComponentUI;
    public GameObject minigameUI;
    public GameObject transparentBackground;
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

    public void InitializeMinigameTimer()
    { minigameUI.transform.GetChild(3).transform.GetChild(1).GetComponent<Image>().fillAmount = 1f; }

    public void SetUserIntefaceActive(GameObject userInterface, bool value)
    { userInterface.SetActive(value); }

    public void UpdateMinigameTimer(int amountOfTime, int currentAmountOfTime)
    { minigameUI.transform.GetChild(3).transform.GetChild(1).GetComponent<Image>().fillAmount = (float)1/currentAmountOfTime * amountOfTime; }
}