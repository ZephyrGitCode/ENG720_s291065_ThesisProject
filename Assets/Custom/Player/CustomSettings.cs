using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BayatGames.SaveGameFree;

public class CustomSettings : MonoBehaviour
{
    LaserPointer laserPointer;
    LineRenderer lineRenderer;

    public UnityEngine.Material[] MatList;

    // Start is called before the first frame update
    void Awake()
    {
        laserPointer = FindObjectOfType<LaserPointer>();
        LoadPointer();
    }

    private void LoadPointer()
    {
        int matNum = SaveGame.Load<int>("LaserPointerMat");
        if (laserPointer!=null && matNum!=null)
        {
            lineRenderer = laserPointer.GetComponent<LineRenderer>();
            UnityEngine.Material[] mats = lineRenderer.materials;
            mats[0] = MatList[matNum];
            lineRenderer.materials = mats;
        }
    }

    public void SelectMaterial(int matNum)
    {
        if (laserPointer!=null)
        {
            lineRenderer = laserPointer.GetComponent<LineRenderer>();
            UnityEngine.Material[] mats = lineRenderer.materials;
            mats[0] = MatList[matNum];
            lineRenderer.materials = mats;
            SaveGame.Save<int>("LaserPointerMat", matNum);
        }
    }


    [System.Serializable]
    public class CustomData
    {
        public string scene;
        public List<float> accuracy;

        public CustomData()
        {
            scene = "";

            // accuracy list
            accuracy = new List<float>() {};
        }
    }

    public CustomData customData;
    public string identifier;

    public List<float> LoadAccuracy(string sceneName)
    {
        customData = SaveGame.Load<CustomData>(
            sceneName,
            new CustomData());
        List<float> saveAccuracy = customData.accuracy;
        return saveAccuracy;
    }
    
    public void SaveStop(float accuracy)
    {
        // return average?

        //User stops, takes float as accuracy, save accuracy against the current scene

        // Get active scene as identifer for save data
        identifier = SceneManager.GetActiveScene().name;

        // Load previous save data
        List<float> saveAccuracy = LoadAccuracy(identifier);

        // Append to previous save data
        saveAccuracy.Add(accuracy);

        // Save custom Data
        SaveGame.Save<CustomData>(identifier, customData);
    }
}
