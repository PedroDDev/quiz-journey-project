using UnityEngine;

[System.Serializable]
public class Test
{
    [System.Serializable]
    public struct testData
    {
        public int index;
        public string question;
        public string[] answersOptions;
        public int correctAnswerIndex;
        public int score;
    }

    public testData[] tests;
}
