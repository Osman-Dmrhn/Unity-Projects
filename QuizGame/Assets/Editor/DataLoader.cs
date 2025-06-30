using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    public string folderPath = "Assets/QuestionData"; // Hedef klasör yolu

    public List<QuestionDataList> LoadQuestionDataLists()
    {
        List<QuestionDataList> questionDataLists = new List<QuestionDataList>();

#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:QuestionDataList", new[] { folderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            QuestionDataList questionDataList = AssetDatabase.LoadAssetAtPath<QuestionDataList>(assetPath);
            if (questionDataList != null)
            {
                questionDataLists.Add(questionDataList);
            }
        }
#endif

        return questionDataLists;
    }


}

