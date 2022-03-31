using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CuttingMinigameController : MonoBehaviour
{
    private bool minigameRunning = false;
    private GameManager clothQuality = new GameManager();
    private LineController lineNodes = new LineController();
    [SerializeField] private List<Transform> nodes;

    public LineController lineController;

    public bool MinigameRunning
    {
        get
        {
            return minigameRunning;
        }

        set
        {
            minigameRunning = value;
            camera2D.SetActive(value);
        }
    }

    public UnityEvent<int> onMiniGameFinished;

    [SerializeField] private Transform shirt;
    [SerializeField] private LineCollision seamLineCollision;
    [SerializeField] private GameObject scissors;
    [SerializeField] private GameObject camera2D;
    [SerializeField] private bool insideLine = false;
    [SerializeField] private float movementSpeed = 1.0f;

    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private float increaseDamageAmount = 0.1f;
    [SerializeField] private float damageSpeed = 1f;
    private float durability = 100f;

    public float sewingSpeed = 0.05f;




    private Vector2 startingPosition;
    private Vector3 startingPositionSciccors;


    void Start()
    {
        if (onMiniGameFinished == null)
        {
            onMiniGameFinished = new UnityEvent<int>();
        }
        //lineController = lineController;
        //nodes = lineNodes.NodesList;
        foreach (Transform node in nodes)
        {

        }
        startingPosition = shirt.position;
        startingPositionSciccors = scissors.transform.position;
        startingPositionSciccors.z -= 1;


    }
    private void FixedUpdate()
    {
        if (minigameRunning)
        {
            damageSpeed += increaseDamageAmount / 2 * Time.time / 1000;
        }
    }

    void Update()
    {
        if (!minigameRunning)
        {
            return;
        }
        AudioSetup();
        StartCoroutine(IntervalAudioCue());

        insideLine = seamLineCollision.IsGameObjectInTrigger(scissors);

        Vector2 shirtPosition = shirt.position;

        if (shirtPosition.y < -0.88f)
        {
            Debug.Log("MiniGame Done " + shirtPosition.y);
            FinishedMiniGame();
        }
        if (durability <= 0)
        {
            FinishedMiniGame();
            ResetMinigame();
        }
        qualityText.text = "Durability: "+ durability.ToString("0.0");
        shirt.position = shirtPosition;
    }

    public void FinishedMiniGame()
    {
        onMiniGameFinished.Invoke(((int)durability));
    }

    IEnumerator IntervalAudioCue()
    {
        while (minigameRunning)
        {
            yield return new WaitForSeconds(sewingSpeed);
            //AkSoundEngine.PostEvent("Sew", gameObject);
        }
    }
    private void AudioSetup()
    {
        //AkSoundEngine.SetRTPCValue("Sewing_Speed", sewingInterval*1000);
    }


    public void DecreaseShirtDurability()
    {
        durability = durability - (damageSpeed * Time.deltaTime);
    }
    public void DecreaseShirtDurabilityHigh()
    {
        durability = durability - 90;
    }

    public void ResetMinigame()
    {
        shirt.position = startingPosition;
        scissors.transform.position = startingPositionSciccors;

        durability = 100;
    }
}
