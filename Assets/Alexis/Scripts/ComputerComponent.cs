using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class ComputerComponent : MonoBehaviour
{
    // Goal: Display the name of the component
    // We would need to grab the name of the object
    // Create a user interface and display that text when the player's mouse is over it
    // it would need a collider I believe.

    #region Private
    private MeshCollider componentCollider;
    private MeshRenderer componentRenderer;

    private string componentName;
    #endregion

    #region Public
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        float randomR = Random.value;
        float randomG = Random.value;
        float randomB = Random.value;
        
        componentCollider = gameObject.AddComponent<MeshCollider>();
        componentName = gameObject.name;
        componentRenderer = gameObject.GetComponent<MeshRenderer>();
    
        // Temporary line (below), to be removed
        componentRenderer.material.color = new Color(randomB, randomG, randomB)/*.black*/;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseEnter() 
    { componentRenderer.material.SetColor("_EmissionColor", new Color(0.16f, 0.16f, 0.16f)); }

    private void OnMouseExit()
    { componentRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f)); }
}