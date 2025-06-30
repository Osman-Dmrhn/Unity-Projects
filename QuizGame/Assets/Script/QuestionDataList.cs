using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New QuestionList", menuName = "Quiz/QuestionList")]
public class QuestionDataList : ScriptableObject
{
    public List<QuestionData> data;
}
