using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SpreadsheetToScriptableObject
{
    private static string enemyCSVPath = "/Scripts/Editor/CSVs/Enemies.csv", skillsCSVPath = "/Scripts/Editor/CSVs/Skills.csv"; //String paths for CSV files
    private const string dataListPath = "Assets/Scriptable Objects/DataList.asset"; //String path for the data list housing all created assets

    //Function that returns the data list from said path if found
    private static DataList GetDataList()
    {
        DataList dl = AssetDatabase.LoadAssetAtPath<DataList>(dataListPath);
        if (dl == null)
            Debug.LogError("DataList.asset not found!");
        return dl;
    }

    [MenuItem("Generation/Generate Skills")] //Ceates a new selection under the "Generation" tab in the unity editor to generate skills
    public static void GenerateSkills()
    {
        DataList dataList = GetDataList(); //Calls for data list refrence

        //Gets each line from the skills CSV file and seperates them into an array
        string[] allLines = File.ReadAllLines(Application.dataPath + skillsCSVPath);

        dataList.allSkills.Clear(); //Clears previous list of skills

        //Loop that skips the first line of the CSV file (since they are headers) and creates a new scriptable object per line in the CSV
        for (int i = 1; i < allLines.Length; i++)
        {
            //Splits the data in the line by the "," divider
            string[] splitData = allLines[i].Split(',');

            //Creates instance of the skills scriptable object and sets the data from the CSV into the it using the split data
            Skills skill = ScriptableObject.CreateInstance<Skills>();
            skill.skillName = splitData[0];
            skill.skillDescription = splitData[1];
            skill.damage = float.Parse(splitData[2]);
            skill.numOfHits = int.Parse(splitData[3]);
            skill.delay = float.Parse(splitData[4]);
            skill.cost = int.Parse(splitData[5]);

            //Creates the skill scriptable object as a new assets inside of its respective folder
            AssetDatabase.CreateAsset(skill, $"Assets/Scriptable Objects/Skills/{skill.skillName}.asset");
            dataList.allSkills.Add(skill); //Adds the current skill to the skill list in the data list
        }
        //Saves the assets
        EditorUtility.SetDirty(dataList); //Overwrites the data list
        AssetDatabase.SaveAssets();
    }
}
