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

    [SerializeField] private Transform shirt;
    [SerializeField] private LineCollision seamLineCollision;
    [SerializeField] private GameObject needle;
    [SerializeField] private GameObject camera2D;
    [SerializeField] private bool insideLine = false;
    [SerializeField] private float increaseSpeedAmount = 0.01f;
    [SerializeField] private float sewingSpeed = 0.05f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float sewingInterval = 0.070f;

    private float quality = 100f;
    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private float increaseDamageAmount = 0.1f;
    [SerializeField] private float damageSpeed = 1f;

    private Vector2 startingPosition;

    void Start()
    {
        if (onMiniGameFinished == null)
        {
            onMiniGameFinished = new UnityEvent<int>();
        }

        startingPosition = shirt.position;
    }
    private void FixedUpdate()
    {
        if (minigameRunning)
        {
            sewingSpeed += increaseSpeedAmount * Time.time / 10000;
            movementSpeed += increaseSpeedAmount / 2 * Time.time / 10000;
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
        StartCoroutine(IntervalCalc());
        StartCoroutine(IntervalAudioCue());

        insideLine = seamLineCollision.IsGameObjectInTrigger(needle);

        Vector2 shirtPosition = shirt.position;
        float inputFloat = Input.GetAxis("Horizontal");

        shirtPosition.x -= Time.deltaTime * inputFloat * movementSpeed;
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
