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
    [SerializeField] List<Transform> nodes;

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
    [SerializeField] private GameObject needle;
    [SerializeField] private GameObject camera2D;
    [SerializeField] private bool insideLine = false;
    [SerializeField] private float increaseSpeedAmount = 0.01f;
    [SerializeField] private float sewingSpeed = 0.05f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float sewingInterval = 0.070f;

    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private float increaseDamageAmount = 0.1f;
    [SerializeField] private float damageSpeed = 1f;

    private float quality = 100f;


    private Vector2 startingPosition;

    void Start()
    {
        if (onMiniGameFinished == null)
        {
            onMiniGameFinished = new UnityEvent<int>();
        }
        //nodes = lineNodes.NodesList;
        foreach (Transform node in nodes)
        {

        }
        startingPosition = shirt.position;
    }
    private void FixedUpdate()
    {
        if (minigameRunning)
        {

        }
    }

    void Update()
    {
        if (!minigameRunning)
        {
            return;
        }
        AudioSetup();
        StartCoroutine(IntervalCalc());
        StartCoroutine(IntervalAudioCue());

        insideLine = seamLineCollision.IsGameObjectInTrigger(needle);

        Vector2 shirtPosition = shirt.position;

        if (shirtPosition.y < -0.88f)
        {
            Debug.Log("MiniGame Done " + shirtPosition.y);
            FinishedMiniGame();
        }
        if (quality <= 0)
        {
            ResetMinigame();
        }
        qualityText.text = quality.ToString("0.0");
        shirt.position = shirtPosition;
    }

    public void FinishedMiniGame()
    {
        onMiniGameFinished.Invoke(((int)quality));
    }

    IEnumerator IntervalCalc()
    {
        while (minigameRunning)
        {
            yield return new WaitForSeconds(.5f);
            sewingInterval -= increaseSpeedAmount * Time.deltaTime / 10;
            yield return new WaitForSeconds(.5f);
        }
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
        quality = quality - (damageSpeed * Time.deltaTime);
    }
    public void DecreaseShirtDurabilityHigh()
    {
        quality = quality - 50;
    }

    public void ResetMinigame()
    {
        shirt.position = startingPosition;
        quality = 100;
    }
}
