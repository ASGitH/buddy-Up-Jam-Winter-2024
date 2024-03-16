using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Computer : MonoBehaviour
{
    // If interacted with wouold position the computer in the middle, 

    // In the user interface script, would enable a transparent background

    #region Private
    private Animator animator;

    private bool isInFocus = false;

    private Transform initialCameraTransform, initialTransform;
    #endregion

    #region Public
    // public bool hasCaseInstalled
    public bool hasCPUBeenInstalled, hasGPUBeenInstalled, hasHardDriveBeenInstalled, hasMotherboardBeenInstalled, hasPSUBeenInstalled, hasRAMBeenInstalled;

    public UserInterface userInterfaceGameObject;
    #endregion

    // Start is called before the first frame update
    void Start() 
    {
        animator = GetComponent<Animator>();

        initialCameraTransform = Camera.main.transform;
        initialTransform = transform;
    }

    private void OnMouseDown()
    {
        if(!isInFocus)
        {
            animator.enabled = false;

            Camera.main.transform.Rotate(new Vector3(-22.5f, 0f, 0f));

            isInFocus = true;

            transform.position = new Vector3(0f, 2f, -1f);
            transform.Rotate(0f, -45f, 0f);
        }
    }

    private void OnMouseDrag()
    {
        if(isInFocus) 
        { transform.Rotate(new Vector3(0f, -Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"))); }
    }

    public void SetComputerComponentInPlace(GameObject computerComponent, string component)
    {
        switch (component)
        {
            case "Motherboard":
                if(!hasMotherboardBeenInstalled)
                {
                    hasMotherboardBeenInstalled = true;

                    transform.GetChild(2).gameObject.SetActive(true);
                }
                break;
        }

        computerComponent.SetActive(false);
    }
}