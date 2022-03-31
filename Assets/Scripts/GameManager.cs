using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public WarDrobe warDrobe;
    [SerializeField]private float roomTemperature = 23f;
    [SerializeField] private float bodyTemperature = 37f;
    private float quality = 10f;
    public float durability = 100;

    public TMP_Text RoomTempTextfield;
    public TMP_Text BodyTempTextfield;
    public TMP_Text ClothingQualityTextField;
    public TMP_Text ClothingDurabilityTextfield;


    public float Quality
    {
        get
        {
            return quality;
        }

        set
        {
            quality = value;
        }
    }

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
            bodyTemperature -= roomTemperature / 350 * Time.deltaTime; //player health = body temp (temp) 
        }

        if (bodyTemperature < 35f)
        {
            EndScene();
        }
        RoomTempTextfield.text = "Room Temp: " + roomTemperature.ToString("0.0") + "�C";
        BodyTempTextfield.text = "Body Temp: " + bodyTemperature.ToString("0.0") + "�C";
        ClothingQualityTextField.text = "Quality %" + quality.ToString("0.0");
        ClothingDurabilityTextfield.text = "Durability %" + durability.ToString("0.0");

        playerfeedback();
    }

    public void EndScene()
    {
        SceneManager.LoadScene("Lose_Scene");
    }

    public void winScene()
    {
        SceneManager.LoadScene("Win_Scene");
    }

    public void SetNewShirt(int qualityScore, int durabilityScore)
    {
        quality = qualityScore;
        durability = durabilityScore;
        if (durability < 0)
        {
            durability = 0;
        }
        if (quality < 0)
        {
            quality = 0;
        }
    }

    void playerfeedback()
    {
        if (durability < 40)
        {
            ClothingDurabilityTextfield.color = Color.red;
        }
        else ClothingDurabilityTextfield.color = new Color(49, 70, 70);

    }
}
