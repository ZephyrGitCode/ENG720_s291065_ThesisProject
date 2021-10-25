using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BayatGames.SaveGameFree;

public class CustomSettings : MonoBehaviour
{
    LaserPointer laserPointer;
    LineRenderer lineRenderer;
    DrawStatistics drawStatistics;

    public UnityEngine.Material[] MatList;

    // Start is called before the first frame update
    void Awake()
    {
        laserPointer = FindObjectOfType<LaserPointer>();
        LoadPointer();
        drawStatistics = FindObjectOfType<DrawStatistics>();
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

    /* Custom Data --------------------------------------------------------- */
    [System.Serializable]
    public class CustomDataScene
    {
        public List<float> accuracy;

        public List<float> objectives;

        public CustomDataScene()
        {
            // accuracy list
            accuracy = new List<float>() {};
            objectives = new List<float>() {};
        }
    }

    [System.Serializable]
    public class CustomDataCollisions
    {
        public List<float> collisions;

        public CustomDataCollisions()
        {
            // accuracy list
            collisions = new List<float>() {};
        }
    }

    public CustomDataScene customDataScene;
    public CustomDataCollisions customDataCollisions;
    public string identifier;

    /* Saving code --------------------------------------------------------- */
    public void SaveStop(float accuracy)
    {
        // Called from GamePlayLogic: user stops at sign, takes float as accuracy, saves accuracy against the current scene

        // Get active scene as identifer for save data
        identifier = SceneManager.GetActiveScene().name;

        // Load previous save data
        List<float> saveAccuracy = LoadAccuracy(identifier);

        // Append to previous save data
        saveAccuracy.Add(accuracy);

        // Save custom Data
        SaveGame.Save<CustomDataScene>(identifier, customDataScene);
    }

    public void SaveCollision(int objectHit)
    {
        // Called from GamePlayLogic: collides with object, gives int, adds collision
        // 1 = pedestrian, 2 = cars

        // Load previous save data
        List<float> saveCollision = LoadCollisions();
        
        // Append to previous save data
        saveCollision.Add(objectHit);

        // Save custom Data
        SaveGame.Save<CustomDataCollisions>("collisions", customDataCollisions);
    }

    public void SaveObjective(int objectiveCount)
    {
        // Called from GamePlayLogic: User completes level, takes count of objectives for scene, saves objective against current scene
        // Saves count of objectives at scene win.

        // Get active scene as identifer for save data
        identifier = SceneManager.GetActiveScene().name;

        // Load previous save data
        List<float> saveObjective = LoadObjectives(identifier);

        // Append to previous save data
        saveObjective.Add(objectiveCount);

        // Save custom Data
        SaveGame.Save<CustomDataScene>(identifier, customDataScene);
    }

    /* Loading Code --------------------------------------------------------- */
    public List<float> LoadAccuracy(string sceneName)
    {
        customDataScene = SaveGame.Load<CustomDataScene>(
            sceneName,
            new CustomDataScene());
        List<float> saveAccuracy = customDataScene.accuracy;
        return saveAccuracy;
    }

    public List<float> LoadCollisions()
    {
        // load collision data
        customDataCollisions = SaveGame.Load<CustomDataCollisions>("collisions", new CustomDataCollisions());
        List<float> saveCollisions = customDataCollisions.collisions;
        return saveCollisions;
    }

    public List<float> LoadObjectives(string sceneName)
    {
        customDataScene = SaveGame.Load<CustomDataScene>(
            sceneName,
            new CustomDataScene());
        List<float> saveObjective = customDataScene.objectives;
        return saveObjective;
    }
    
    List<string> sceneList = new List<string>() {"1_ZebraCrossingSimple", "2_ZebraCrossingAdvanced", "3_StopSignSimple", "4_StopSignAdvanced", "5_RightofWaySimple", "6_RightofWayAdvanced"};
    
    public List<float> LoadAccuracyNum(int StatNum)
    {
        // Load Custom Data
        customDataScene = SaveGame.Load<CustomDataScene>(
                sceneList[StatNum],
                new CustomDataScene());

        // Load data into list
        List<float> stoppingStats = customDataScene.accuracy;

        // Return list to statistics
        return stoppingStats;
    }

    public List<float> LoadObjectiveNum(int StatNum)
    {
        // Load Custom Data
        customDataScene = SaveGame.Load<CustomDataScene>(
                sceneList[StatNum],
                new CustomDataScene());

        // Load data into list
        List<float> objectiveStats = customDataScene.objectives;

        // Return list to statistics
        return objectiveStats;
    }

    public List<List<float>> LoadAccuracyAll()
    {
        List<List<float>> stoppingStats = new List<List<float>>();
        foreach (string sceneName in sceneList)
        {
            // foreach scenename in list, get accuracy data, store into list of lists.
            customDataScene = SaveGame.Load<CustomDataScene>(
                sceneName,
                new CustomDataScene());
            stoppingStats.Add(customDataScene.accuracy);
        }
        return stoppingStats;
    }

    void MenuStats(float StatNum)
    {
        /*
        // stopping range
        List<List<float>> stoppingStats;
        stoppingStats = LoadAccuracyAll();
        foreach (List<float> numbers in stoppingStats)
        {
            
        }
        drawStatistics.WriteStats();
        //LoadAccuracyAll()
        */

        // Statnum 1 = Stoping range, 2 = Collisions, 3 = Other
        switch(StatNum)
        {
            case 1:
                //load stopping range UI
                break;
            case 2:
                // Collisions
                break;
            case 3:
                // Other
                break;
        
        default:
            Debug.Log("Failure");
            break;
        }
    }

    /* Clearing Data Code --------------------------------------------------------- */
    public void ClearData(int sceneNum) {
        // Clear data for certain scene
        //SaveGame.Delete(sceneNum);
    }

    public void ClearAllData() {
        // Clears all data
        SaveGame.Clear();
    }

    private void Start() {
        //LoadCollisions();
        //ClearAllData(); -- debug
        //DebugData();
    }

    void DebugData()
    {
        identifier = SceneManager.GetActiveScene().name;
        List<float> saveAccuracy = LoadAccuracy(identifier);
        if (saveAccuracy != null)
        {
            foreach (float number in saveAccuracy)
            {
                Debug.Log("Zep: "+number);
            }
            
        }
    }
}
