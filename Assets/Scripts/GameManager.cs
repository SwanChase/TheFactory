using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float roomTemperature = 23f;
    public float bodyTemperature = 37f;
    public float quality = 10f;
    public float durability = 100;

    public TMP_Text RoomTempTextfield;
    public TMP_Text BodyTempTextfield;
    public TMP_Text ClothingQualityTextField;
    public TMP_Text ClothingDurabilityTextfield;

    void Start()
    {
        StartCoroutine(Cool());
    }
    IEnumerator Cool()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
        }
    }

    private void Update()
    {
        if (durability <= 100)
        {
            durability -= roomTemperature * ((110 - quality) / 1000) * Time.deltaTime;
        }
        if (durability <= 0)
        {
            durability += roomTemperature * ((110 - quality) / 1000) * Time.deltaTime;
            bodyTemperature -= roomTemperature / 1000 * Time.deltaTime; //player health = body temp (temp) 

        }

        if (bodyTemperature < 35f)
        {
            //SceneManager.LoadScene("Game Over");
        }
        RoomTempTextfield.text = "Room Temp: " + roomTemperature.ToString("0.0") + "°C";
        BodyTempTextfield.text = "Body Temp: " + bodyTemperature.ToString("0.0") + "°C";
        ClothingQualityTextField.text = "Quality %" + quality.ToString("0.0");
        ClothingDurabilityTextfield.text = "Durability %" + durability.ToString("0.0");
    }

    public void SetNewShirt(int qualityScore)
    {
        quality = qualityScore;
        durability = 100;
    }
}
