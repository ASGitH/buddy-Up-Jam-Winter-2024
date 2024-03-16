using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    // If interacted with wouold position the computer in the middle, 

    // In the user interface script, would enable a transparent background

    #region Private
    private Animator animator;

    //private bool isInPosition = false;

    private Transform initialCameraTransform, initialTransform;
    #endregion

    #region Public
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
        animator.enabled = false;

        Camera.main.transform.Rotate(new Vector3(-22.5f, 0f, 0f));

        transform.position = new Vector3(0f, 2f, 0f);
        transform.Rotate(0f, -45f, 0f);

        userInterfaceGameObject.SetUserIntefaceActive(userInterfaceGameObject.transparentBackground, true);
    }
}