using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager userInterfaceInstance;

    [SerializeField]AudioSource writingSound;

    PlayerMovement myPlayer;
    MouseLook myMouse;
    public bool gameOver;
    public bool gameFinished;
    public GameObject gameFinishedUI;

    public Slider suspicionMeter;
    public GameObject suspicionMeterUI;

    [SerializeField] GameObject notebook;
    public GameObject kitchenCompletedTick;
    public GameObject bathroomCompletedTick;
    public GameObject livingroomCompletedTick;
    public GameObject bedroomCompletedTick;

    public GameObject pressEtoInteract;
    public GameObject holdEtoInteract;
    public GameObject holdingE;
    public GameObject circularLoadingPart;
    public GameObject gameOverUI;
    public GameObject doorForced;

    int kitchenCompletionInt;
    int kitchenObjectsFixed;
    [SerializeField] List<GameObject> kitchenObjectsList;
    [SerializeField] List<GameObject> kitchenProgress;

    int bathroomCompletionInt;
    int bathroomObjectsFixed;
    [SerializeField] List<GameObject> bathroomObjectsList;
    [SerializeField] List<GameObject> bathroomProgress;

    int livingroomCompletionInt;
    int livingroomObjectsFixed;
    [SerializeField] List<GameObject> livingroomObjectsList;
    [SerializeField] List<GameObject> livingroomProgress;

    int bedroomCompletionInt;
    int bedroomObjectsFixed;
    [SerializeField] List<GameObject> bedroomObjectsList;
    [SerializeField] List<GameObject> bedroomProgress;

    int roomsCleaned;
    private float timer_end = 0f;
    int aTest;
    GameObject aTestGO;

    public void Awake()
    {
        if (userInterfaceInstance == null)
        {
            userInterfaceInstance = this;
            DontDestroyOnLoad(userInterfaceInstance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        kitchenCompletedTick.SetActive(false);
        bathroomCompletedTick.SetActive(false);
        livingroomCompletedTick.SetActive(false);
        bedroomCompletedTick.SetActive(false);
        pressEtoInteract.SetActive(false);
        holdEtoInteract.SetActive(false);
        holdingE.SetActive(false);
        gameOverUI.SetActive(false);
        gameFinishedUI.SetActive(false);
        doorForced.SetActive(false);

        suspicionMeterUI.SetActive(true);

        livingroomCompletionInt = 0;
        livingroomObjectsFixed = 0;

        kitchenCompletionInt = 0;
        kitchenObjectsFixed = 0;

        bathroomCompletionInt = 0;
        bathroomObjectsFixed = 0;

        bedroomCompletionInt = 0;
        bedroomObjectsFixed = 0;

        roomsCleaned = 0;

        myPlayer = new PlayerMovement();
        myMouse = new MouseLook();

        gameOver = false;
        gameFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (notebook.activeSelf == false)
            {
                notebook.SetActive(true);
            }
            else
            {
                notebook.SetActive(false);
            }
        }

        if (suspicionMeter.value == suspicionMeter.maxValue)
        {
            gameOver = true;
            gameOverUI.SetActive(true);
        }

        if (roomsCleaned == 4)
        {
            doorForced.SetActive(false);
            gameFinished = true;
            gameFinishedUI.SetActive(true);
            timer_end += 1f * Time.deltaTime;

            if (timer_end > 5f)
            {
                Application.Quit();
            }
        }
    }

    public void KitchenCompletion()
    {
        writingSound.Play();
        kitchenProgress[kitchenObjectsFixed].SetActive(true);
        kitchenObjectsFixed++;
        kitchenCompletionInt += 100 / kitchenObjectsList.Count;
        
        if(kitchenCompletionInt == 100 || kitchenObjectsFixed == kitchenObjectsList.Count)
        {
            kitchenCompletedTick.SetActive(true);
            roomsCleaned++;
        }
    }

    public void BathroomCompletion()
    {
        writingSound.Play();
        bathroomProgress[bathroomObjectsFixed].SetActive(true);
        bathroomObjectsFixed++;
        bathroomCompletionInt += 100 / bathroomObjectsList.Count;
        
        if (bathroomCompletionInt == 100 || bathroomObjectsFixed == bathroomObjectsList.Count)
        {
            bathroomCompletedTick.SetActive(true);
            roomsCleaned++;
        }
    }

    public void LivingroomCompletion()
    {
        writingSound.Play();
        livingroomProgress[livingroomObjectsFixed].SetActive(true);
        livingroomObjectsFixed++;
        livingroomCompletionInt += 100 / livingroomObjectsList.Count;
        
        if (livingroomCompletionInt == 100 || livingroomObjectsFixed == livingroomObjectsList.Count)
        {
            livingroomCompletedTick.SetActive(true);
            
            roomsCleaned++;
        }
    }

    public void BedroomCompletion()
    {
        writingSound.Play();
        bedroomProgress[bedroomObjectsFixed].SetActive(true);
        bedroomObjectsFixed++;
        bedroomCompletionInt += 100 / bedroomObjectsList.Count;

        if (bedroomCompletionInt == 100 || bedroomObjectsFixed == bedroomObjectsList.Count)
        {
            bedroomCompletedTick.SetActive(true);
            
            roomsCleaned++;
        }
    }


    public void CheckTagForCleaning(GameObject gameObject)
    {
        if (gameObject.tag.ToString() == "Kitchen")
        {
            KitchenCompletion();
        }
        else if (gameObject.tag.ToString() == "Bathroom")
        {
            BathroomCompletion();
        }
        else if (gameObject.tag.ToString() == "Livingroom")
        {
            LivingroomCompletion();
        }
        else if (gameObject.tag.ToString() == "Bedroom")
        {
            BedroomCompletion();
        }
    }
}