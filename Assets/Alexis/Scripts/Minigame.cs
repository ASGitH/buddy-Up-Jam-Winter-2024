using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

//public class Note
//{
//    // Keep track of 
//}

public class Minigame : MonoBehaviour
{
    #region Private
    private bool hasMinigameEnded, hasMinigameStarted;
    private bool hasTimerStopped;
    private bool isGoingDown, isSlottingCPUIn;

    private Computer computer;

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

    private Vector3 CPUInitialPosition, CPUPositionToSlotFrom;
    #endregion

    #region Public
    public bool hasPassedMinigame;

    public GameObject cameraGO;
    public GameObject screws;
    public GameObject userInterfaceGO;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        computer = transform.parent.parent.GetChild(0).GetComponent<Computer>();

        minigameCamera = transform.GetChild(0).gameObject;

        userInterface = userInterfaceGO.GetComponent<UserInterface>();
    }

    // Update is called once per frame
    void Update()
    { PlayMinigame(minigame); }

    private void CPUMinigame()
    {
        if (!hasMinigameStarted)
        {
            CPUInitialPosition = transform.parent.GetChild(1).position;

            MinigameInitialization();

            hasMinigameStarted = true;

            isGoingDown = true;

            minigameTimer = 8;
            minigameTimerIntToFloat = minigameTimer;

            initialMinigameTimer = minigameTimer;

            transform.parent.GetChild(1).gameObject.SetActive(true);

            userInterface.InitializeMinigameTimer();
        }
        else if (!hasTimerStopped)
        {
            if (!hasPassedMinigame)
            {
                //if (amountOfScrews == 0)
                //{ hasPassedMinigame = true; }

                if(!isSlottingCPUIn)
                {
                    if (!isGoingDown)
                    {
                        if (CPUInitialPosition.y - transform.parent.GetChild(1).position.y <= -0.75f)
                        { isGoingDown = true; }
                        else
                        { transform.parent.GetChild(1).position -= (Vector3.down * Time.deltaTime) * 1.75f; }
                    }
                    else
                    {
                        if (CPUInitialPosition.y - transform.parent.GetChild(1).position.y >= 0.75f)
                        { isGoingDown = false; }
                        else
                        { transform.parent.GetChild(1).position += (Vector3.down * Time.deltaTime) * 1.75f; }
                    }
                }
                else
                {
                    if (Vector3.Distance(CPUPositionToSlotFrom, transform.parent.GetChild(1).position) < .75f)
                    { transform.parent.GetChild(1).position -= new Vector3(1.75f * Time.deltaTime, 0f, -1.75f * Time.deltaTime); }
                    else
                    {
                        if (CPUInitialPosition.y - CPUPositionToSlotFrom.y > -0.10f && CPUInitialPosition.y - CPUPositionToSlotFrom.y < 0f)
                        { hasPassedMinigame = true; }
                    }
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.name == "CPU")
                        {
                            CPUPositionToSlotFrom = transform.parent.GetChild(1).position;

                            isSlottingCPUIn = true; 
                        }
                    }
                }
            }

            UpdateMinigameTimer();
        }
        else
        {
            cameraGO.SetActive(true);

            computer.GetComponent<ComputerComponent>().isInMinigame = false;

            hasMinigameStarted = false;

            isSlottingCPUIn = false;

            minigame = "";

            minigameCamera.SetActive(false);

            transform.parent.GetChild(1).gameObject.SetActive(false);
            transform.parent.GetChild(1).position = CPUInitialPosition;
            transform.parent.GetComponent<ComputerComponent>().isInMinigame = false;

            userInterface.SetUserIntefaceActive(userInterface.minigameUI, false);

            if (hasPassedMinigame)
            { computer.SetComputerComponentInPlace(transform.parent.gameObject, "CPU"); }
        }
    }

    private void GPUMinigame()
    {
        if (!hasMinigameStarted)
        {
            MinigameInitialization();

            hasMinigameStarted = true;
        }
        else if (!hasPassedMinigame)
        {
            //
        }
        else
        {
            cameraGO.SetActive(true);

            computer.GetComponent<ComputerComponent>().isInMinigame = false;

            hasMinigameStarted = false;

            minigame = "";

            minigameCamera.SetActive(false);

            transform.parent.GetComponent<ComputerComponent>().isInMinigame = false;

            userInterface.SetUserIntefaceActive(userInterface.minigameUI, false);

            if (hasPassedMinigame)
            { computer.SetComputerComponentInPlace(transform.parent.gameObject, "GPU"); }
        }
    }

    private void MinigameInitialization()
    {
        cameraGO.SetActive(false);

        computer.GetComponent<ComputerComponent>().isInMinigame = true;

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

            //int amountOfScrewsToRemove;

            amountOfScrews = Random.Range(1, screws.transform.childCount / 4);
            // amountOfScrewsToRemove = screws.transform.childCount - amountOfScrews;

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
        else if(!hasTimerStopped)
        {
            if(!hasPassedMinigame)
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
            }

            UpdateMinigameTimer();
        }
        else
        {
            cameraGO.SetActive(true);

            computer.GetComponent<ComputerComponent>().isInMinigame = false;

            for (int counter = 0; counter < screwList.Count; counter++)
            {
                screws.transform.GetChild(screwList[counter]).transform.position = screwListPositions[counter];
                screws.transform.GetChild(screwList[counter]).gameObject.GetComponent<BoxCollider>().enabled = true;
                screws.transform.GetChild(screwList[counter]).gameObject.SetActive(false);
            }

            hasMinigameStarted = false;

            minigame = "";

            minigameCamera.SetActive(false);

            transform.parent.GetComponent<ComputerComponent>().isInMinigame = false;

            userInterface.SetUserIntefaceActive(userInterface.minigameUI, false);

            if(hasPassedMinigame)
            { computer.SetComputerComponentInPlace(transform.parent.gameObject, "Motherboard"); }
        }
    }

    private void PlayMinigame(string minigame)
    {
        if(!hasMinigameEnded)
        {
            switch (minigame)
            {
                case "CPU": CPUMinigame(); break;
                case "GPU": GPUMinigame(); break;
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