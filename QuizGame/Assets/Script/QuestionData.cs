using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Question",menuName ="Quiz/Question")]
public class QuestionData : ScriptableObject
{
    public string questionText;
    public int trueAnswers;
    public List<string> answers;
}
