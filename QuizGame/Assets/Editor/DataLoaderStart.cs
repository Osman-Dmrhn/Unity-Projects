using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataLoaderStart : EditorWindow
{
    private List<QuestionData> loadedQuestionData;
    private string dataFolderPath = "Assets/Question/";       // Sorularýn bulunduðu varsayýlan klasör yolu
    private string saveFilePath = "Assets/Question/QuestionDataList/QuestionDataList.asset"; // Kaydedilecek dosyanýn varsayýlan yolu

    [MenuItem("Tools/Question Data Loader")]
    public static void ShowWindow()
    {
        GetWindow<DataLoaderStart>("Question Data Loader");
    }

    private void OnGUI()
    {
        GUILayout.Label("Question Data Loader", EditorStyles.boldLabel);

        // Klasör yolu giriþ alaný
        GUILayout.Label("Question Data Folder Path:");
        dataFolderPath = GUILayout.TextField(dataFolderPath);

        if (GUILayout.Button("Load Question Data"))
        {
            LoadQuestionData();
        }

        // Yüklenen sorularý göster
        if (loadedQuestionData != null && loadedQuestionData.Count > 0)
        {
            GUILayout.Label("Loaded Question Data:");

            // Kaydetme yolu giriþ alaný
            GUILayout.Space(10);
            GUILayout.Label("Save QuestionDataList Path:");
            saveFilePath = GUILayout.TextField(saveFilePath);

            if (GUILayout.Button("Save as QuestionDataList"))
            {
                SaveAsQuestionDataList();
            }
        }
        else
        {
            GUILayout.Label("No Question Data loaded.");
        }
    }

    private void LoadQuestionData()
    {
        loadedQuestionData = new List<QuestionData>();

#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:QuestionData", new[] { dataFolderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            QuestionData questionData = AssetDatabase.LoadAssetAtPath<QuestionData>(assetPath);
            if (questionData != null)
            {
                loadedQuestionData.Add(questionData);
            }
        }

        Debug.Log("Question Data loaded: " + loadedQuestionData.Count);
#endif
    }

    private void SaveAsQuestionDataList()
    {
        if (loadedQuestionData == null || loadedQuestionData.Count == 0)
        {
            Debug.LogWarning("No Question Data to save.");
            return;
        }

        // Yeni bir QuestionDataList ScriptableObject oluþtur
        QuestionDataList questionDataList = ScriptableObject.CreateInstance<QuestionDataList>();
        questionDataList.data = new List<QuestionData>(loadedQuestionData);

#if UNITY_EDITOR
        try
        {
            AssetDatabase.CreateAsset(questionDataList, saveFilePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("QuestionDataList saved at: " + saveFilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save QuestionDataList: " + e.Message);
        }
#endif
    }
}
