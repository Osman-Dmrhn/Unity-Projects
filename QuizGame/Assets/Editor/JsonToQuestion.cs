using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class JsonToQuestion : EditorWindow
{
    private string jsonFilePath = "Assets/Questions.json";

    [MenuItem("Tools/Question Data Importer")]
    public static void ShowWindow()
    {
        GetWindow<JsonToQuestion>("Question Data Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Question Data from JSON", EditorStyles.boldLabel);

        jsonFilePath = EditorGUILayout.TextField("JSON File Path", jsonFilePath);

        if (GUILayout.Button("Import Questions"))
        {
            ImportQuestions();
        }
    }

    private void ImportQuestions()
    {
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("JSON file not found at: " + jsonFilePath);
            return;
        }

        string jsonContent = File.ReadAllText(jsonFilePath);

        // JSON'i bir listeye deserialize ediyoruz
        List<QuestionModel> questions = JsonUtility.FromJson<QuestionModelWrapper>("{\"questions\":" + jsonContent + "}").questions;

        foreach (var question in questions)
        {
            CreateQuestionDataAsset(question);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Questions imported successfully!");
    }

    private void CreateQuestionDataAsset(QuestionModel question)
    {
        QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
        questionData.questionText = question.questionText;
        questionData.trueAnswers = question.trueAnswers;
        questionData.answers = question.answers;

        // Asset'in kaydedileceði yol
        string sanitizedQuestionText = question.questionText.Length > 10
            ? question.questionText.Substring(0, 10).Replace(" ", "_")
            : question.questionText.Replace(" ", "_");
        string assetPath = $"Assets/QuestionData/{sanitizedQuestionText}.asset";

        AssetDatabase.CreateAsset(questionData, AssetDatabase.GenerateUniqueAssetPath(assetPath));
    }
}

[System.Serializable]
public class QuestionModel
{
    public string questionText;
    public int trueAnswers;
    public List<string> answers;
}

[System.Serializable]
public class QuestionModelWrapper
{
    public List<QuestionModel> questions;
}

