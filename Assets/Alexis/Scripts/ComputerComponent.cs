using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ComputerComponent : MonoBehaviour
{
    // Goal: Making the Computer Hover when Highlighted by Cursor
    // - Have computer quickly rise up when cursor over said object
    // - Have the computer hover down/up as long as cursor is over said object
    // - When not over computer, float back down into position

    #region Private
    private MeshCollider componentCollider;
    private MeshRenderer componentRenderer;

    private string componentName;

    private Transform originalTransform;
    #endregion

    #region Public
    public bool canBeHighlighted;
    public bool canHover;

    public UserInterface userInterfaceGameObject;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        componentCollider = gameObject.AddComponent<MeshCollider>();
        componentName = gameObject.name;
        componentRenderer = gameObject.GetComponent<MeshRenderer>();

        componentCollider = gameObject.AddComponent<MeshCollider>();
        componentName = gameObject.name;
        componentRenderer = gameObject.GetComponent<MeshRenderer>();
        // Temporary line (below), to be removed
        componentRenderer.material.color = randomColor;

        originalTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseEnter()
    {

        if (canBeHighlighted)
        { componentRenderer.material.SetColor("_EmissionColor", new Color(0.16f, 0.16f, 0.16f)); }

        userInterfaceGameObject.DisplayComputerComponentName(componentName);
        userInterfaceGameObject.SetUserIntefaceActive(userInterfaceGameObject.computerComponentUI, true);
    }

    private void OnMouseExit()
    {
        if (canBeHighlighted)
        { componentRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f)); }

        if (canHover)
        {

        }

        userInterfaceGameObject.DisplayComputerComponentName("Computer Component");
        userInterfaceGameObject.SetUserIntefaceActive(userInterfaceGameObject.computerComponentUI, false);
    }

    private void OnMouseOver()
    {
        if (canHover)
        {
            transform.position += Vector3.Lerp(transform.position, Vector3.up * 10f,Time.deltaTime);
            //if (Mathf.Abs(originalTransform.position.y - transform.position.y) > 10f)
            //{ transform.position += Vector3.down * Time.deltaTime; }
            //else if (Mathf.Abs(originalTransform.position.y - transform.position.y) > 10f)
            //{ transform.position += Vector3.up * Time.deltaTime; }
        }
    }
}