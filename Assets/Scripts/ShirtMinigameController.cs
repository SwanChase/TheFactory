using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ShirtMinigameController : MonoBehaviour
{
    private bool minigameRunning = false;

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

    public Transform shirt;
    public LineCollision seamLineCollision;
    public GameObject needle;
    public GameObject camera2D;
    public bool insideLine = false;
    public float increaseSpeedAmount = 0.01f;
    public float sewingSpeed = 0.05f;
    public float movementSpeed = 1.0f;
    public float sewingInterval = 0.070f;
    public float maxSewingInterval = 0.4f;

    private float quality = 100f;
    public TMP_Text qualityText;
    public float increaseDamageAmount = 0.1f;
    public float damageSpeed = 1f;
    //public AK.Wwise.Event MyEvent;

    private Vector2 startingPosition;
    private float timePast;

    void Start()
    {
        if (onMiniGameFinished == null)
        {
            onMiniGameFinished = new UnityEvent<int>();
        }

        startingPosition = shirt.position;
    }
    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        if (minigameRunning)
        {
            sewingSpeed += increaseSpeedAmount * Time.time / 10000;
            movementSpeed += increaseSpeedAmount / 2 * Time.time / 10000;
            damageSpeed += increaseDamageAmount / 2 * Time.time / 1000;

            if (Time.time >= timePast)
            {
                timePast = Mathf.FloorToInt(Time.time) + 10f;
                NextUpdate();
            }
            StartCoroutine(IntervalCalc());
        }
    }


    void Update()
    {
        if (!minigameRunning)
        {
            return;
        }

        insideLine = seamLineCollision.IsGameObjectInTrigger(needle);

        Vector2 shirtPosition = shirt.position;
        float inputFloat = Input.GetAxis("Horizontal");

        shirtPosition.x -= Time.deltaTime * inputFloat * movementSpeed;
        shirtPosition.x = Mathf.Clamp(shirtPosition.x, -12.35f, -11.13f);

        shirtPosition.y -= Time.deltaTime * sewingSpeed;
        if (shirtPosition.y < -0.88f)
        {
            Debug.Log("MiniGame Done " + shirtPosition.y);
            onMiniGameFinished.Invoke(((int)quality));
        }
        if (quality <= 0)
        {
            ResetMinigame();
        }
        qualityText.text = quality.ToString("0.0");
        shirt.position = shirtPosition;

    }
    void NextUpdate()
    {
        StartCoroutine(IntervalAudioCue());

    }

    IEnumerator IntervalCalc()
    {
        while (minigameRunning)
        {
            yield return new WaitForSeconds(.5f);
            if (sewingInterval>= maxSewingInterval)
            {
                sewingInterval -= increaseSpeedAmount * Time.deltaTime / 10;
            }
            yield return new WaitForSeconds(.5f);
            AudioSetup();
        }
    }
    IEnumerator IntervalAudioCue()
    {
        while (minigameRunning)
        {
            yield return new WaitForSeconds(sewingInterval);
            AkSoundEngine.PostEvent("Unsew", gameObject);
            //yield return new WaitForSeconds(sewingInterval);
            AkSoundEngine.PostEvent("Sew", gameObject);
            //Debug.Log("Sewing interval: "+sewingInterval);
        }
    }
    private void AudioSetup()
    {
        float inputMS = sewingInterval * 1000;
        AkSoundEngine.SetRTPCValue("Sewing_Speed", inputMS);
        Debug.Log("Sewing interval x 1000: "+sewingInterval *1000);
    }


    public void DecreaseShirtDurability()
    {
        quality = quality - (damageSpeed * Time.deltaTime);
    }

    public void ResetMinigame()
    {
        shirt.position = startingPosition;
        quality = 100;
    }
}
