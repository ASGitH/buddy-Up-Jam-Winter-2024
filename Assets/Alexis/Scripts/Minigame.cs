using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    #region Private
    private bool hasMinigameEnded, hasMinigameStarted;
    private bool hasTimerStopped;

    private float minigameTimerIntToFloat;

    private GameObject minigameCamera;

    private int amountOfScrews;
    private int initialMinigameTimer, minigameTimer;

    private List<int> screwList;
    private List<Vector3> screwListPositions;

    private Ray ray;
    private RaycastHit hit;

    private string minigame = "";

    private UserInterface userInterface;
    #endregion

    #region Public
    public bool hasFailedMinigame, hasPassedMinigame;

    public GameObject cameraGO;
    public GameObject screws;
    public GameObject userInterfaceGO;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        minigameCamera = transform.GetChild(0).gameObject;

        userInterface = userInterfaceGO.GetComponent<UserInterface>();
    }

    // Update is called once per frame
    void Update()
    { PlayMinigame(minigame); }

    private void MinigameInitialization()
    {
        cameraGO.SetActive(false);

        hasFailedMinigame = false;
        hasMinigameEnded = false;
        hasMinigameStarted = false;
        hasPassedMinigame = false;
        hasTimerStopped = false;

        initialMinigameTimer = 0;

        minigameCamera.SetActive(true);

        minigameTimer = 0;
        minigameTimerIntToFloat = 0f;
    }

    private void MotherboardMinigame()
    {
        if (!hasMinigameStarted)
        {
            MinigameInitialization();

            int amountOfScrewsToRemove;

            amountOfScrews = Random.Range(1, screws.transform.childCount / 4);
            amountOfScrewsToRemove = screws.transform.childCount - amountOfScrews;

            screwList = new List<int>();
            screwListPositions = new List<Vector3>();

            for (int counter = 0; counter < screws.transform.childCount; counter++)
            { screwList.Add(counter); }

            for (int counterOne = 0; counterOne < amountOfScrewsToRemove; counterOne++)
            {
                int selectedScrew = Random.Range(0, screwList.Count);

                screwList.RemoveAt(selectedScrew);
            }

            for (int counterTwo = 0; counterTwo < screwList.Count; counterTwo++)
            { 
                screws.transform.GetChild(screwList[counterTwo]).gameObject.SetActive(true);

                screwListPositions.Add(screws.transform.GetChild(screwList[counterTwo]).transform.position);
            }

            hasMinigameStarted = true;
            hasTimerStopped = false;

            minigameTimer = (amountOfScrews > screws.transform.childCount / 2) ? 16 : 8;
            minigameTimerIntToFloat = minigameTimer;

            initialMinigameTimer = minigameTimer;

            userInterface.InitializeMinigameTimer();
        }
        else if(!hasFailedMinigame && !hasPassedMinigame)
        {
            if(!hasTimerStopped)
            {
                if(amountOfScrews == 0)
                { hasPassedMinigame = true; }

                if (Input.GetMouseButtonDown(0))
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        for (int counter = 0; counter < screwList.Count; counter++)
                        {
                            if (hit.collider.gameObject == screws.transform.GetChild(screwList[counter]).gameObject)
                            {
                                amountOfScrews -= 1;

                                screws.transform.GetChild(screwList[counter]).gameObject.GetComponent<Animator>().SetBool("hasBeenUnscrewed", true);

                                if (screws.transform.GetChild(screwList[counter]).gameObject.GetComponent<MeshCollider>() != null)
                                { screws.transform.GetChild(screwList[counter]).gameObject.GetComponent<MeshCollider>().enabled = false; }
                                else
                                { screws.transform.GetChild(screwList[counter]).gameObject.GetComponent<BoxCollider>().enabled = false; }
                            }
                        }
                    }
                }

                UpdateMinigameTimer();
            }
            else 
            { hasFailedMinigame = true; }
        }
        else
        {
            cameraGO.SetActive(true);

            for (int counter = 0; counter < screwList.Count; counter++)
            {
                screws.transform.GetChild(screwList[counter]).transform.position = screwListPositions[counter];
                screws.transform.GetChild(screwList[counter]).gameObject.GetComponent<BoxCollider>().enabled = true;
                screws.transform.GetChild(screwList[counter]).gameObject.SetActive(false);
            }

            hasMinigameEnded = true;
            hasMinigameStarted = false;

            minigame = "";

            minigameCamera.SetActive(false);

            transform.parent.GetComponent<ComputerComponent>().isInMinigame = false;

            userInterface.SetUserIntefaceActive(userInterface.minigameUI, false);
        }
    }

    private void PlayMinigame(string minigame)
    {
        if(!hasMinigameEnded)
        {
            switch (minigame)
            {
                case "Motherboard": MotherboardMinigame(); break;
            }
        }
    }

    private void UpdateMinigameTimer()
    {
        if(minigameTimerIntToFloat > 0f)
        {
            minigameTimer = (int)minigameTimerIntToFloat;
            minigameTimerIntToFloat -= Time.deltaTime;

            Debug.Log(minigameTimer);

            userInterface.UpdateMinigameTimer(minigameTimer, initialMinigameTimer);
        }
        else
        { hasTimerStopped = true; }
    }

    public void SetMinigame(string minigame)
    {
        hasMinigameEnded = false;

        this.minigame = minigame;

        userInterface.SetUserIntefaceActive(userInterface.minigameUI, true);
    }
}