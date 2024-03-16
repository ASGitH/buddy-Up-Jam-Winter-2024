using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ComputerComponent : MonoBehaviour
{
    #region Private
    private Animator animator;

    private BoxCollider componentBoxCollider;

    private MeshCollider componentCollider;
    private MeshRenderer componentRenderer;

    private Minigame minigameComponent;

    private string componentName;
    #endregion

    #region Public
    public bool canBeHighlighted;
    public bool canHover;
    public bool expandCollider;
    public bool hasMinigame, isInMinigame;
    public bool highlightOverwrite;

    public UserInterface userInterfaceGameObject;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        componentName = gameObject.name;
        componentRenderer = gameObject.GetComponent<MeshRenderer>();
        componentRenderer.material.EnableKeyword("_EMISSION");

        // Temporary line (below), to be removed
        // componentRenderer.material.color = randomColor;
    
        if(canHover)
        { animator = GetComponent<Animator>(); }

        if (!expandCollider)
        { componentCollider = gameObject.AddComponent<MeshCollider>(); }
        else
        { 
            componentBoxCollider = gameObject.AddComponent<BoxCollider>();

            componentBoxCollider.size *= 2;
        }

        if(hasMinigame)
        { minigameComponent = transform.GetChild(0).GetComponent<Minigame>(); }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (hasMinigame)
        {
            if (canHover)
            { animator.SetBool("isInFocus", false); }

            if (userInterfaceGameObject != null)
            {
                userInterfaceGameObject.DisplayComputerComponentName("Computer Component");
                userInterfaceGameObject.SetUserIntefaceActive(userInterfaceGameObject.computerComponentUI, false);
            }

            isInMinigame = true;

            transform.GetChild(0).GetComponent<Minigame>().SetMinigame(componentName); 
        }
    }

    private void OnMouseEnter()
    {
        if ((canBeHighlighted && !isInMinigame) || highlightOverwrite)
        { componentRenderer.material.SetColor("_EmissionColor", new Color(0.32f, 0.32f, 0.32f)); }

        if(!isInMinigame)
        {
            if (canHover)
            { animator.SetBool("isInFocus", true); }

            if (userInterfaceGameObject != null)
            {
                userInterfaceGameObject.DisplayComputerComponentName(componentName);
                userInterfaceGameObject.SetUserIntefaceActive(userInterfaceGameObject.computerComponentUI, true);
            }
        }
    }

    private void OnMouseExit()
    {
        if ((canBeHighlighted && !isInMinigame) || highlightOverwrite)
        { componentRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f)); }

        if(!isInMinigame)
        {
            if (canHover)
            { animator.SetBool("isInFocus", false); }

            if (userInterfaceGameObject != null)
            {
                userInterfaceGameObject.DisplayComputerComponentName("Computer Component");
                userInterfaceGameObject.SetUserIntefaceActive(userInterfaceGameObject.computerComponentUI, false);
            }
        }
    }
}