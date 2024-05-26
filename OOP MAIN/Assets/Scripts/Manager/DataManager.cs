using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
           
    }
    private void Update()
    {
        SavePosition();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        transform.position = new Vector3 (PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
    }
    public void SetMusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void SetFXData(float value)
    {
        PlayerPrefs.SetFloat("FXVolume", value);
    }
    public void ExperinceData(float value)
    {
        PlayerPrefs.SetFloat("Experience", value);
    }
    public void LevelData(int value)
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }
    public void ExperienceToNextLevel(float value)
    {
        PlayerPrefs.SetFloat("ExperienceTNL", value);
    }
    public void CurrentStars(int value)
    {
        PlayerPrefs.SetInt("StarAmount", value);
    }
    public void CurrenntCoin(int value)
    {
        PlayerPrefs.SetInt("CoinAmount", value);
    }
    public void MaxHealth(float value)
    {
        PlayerPrefs.SetFloat("MaxHealth", value);
    }
    public void CurrentHealth(float value)
    {
        PlayerPrefs.SetFloat("CurrentHealth",value);
    }
    public void SavePosition()
    {
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);
    }
}
