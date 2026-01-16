using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static float score = 0;

    public static float BestScore = 0;

    public static Dictionary<string, float> scoreTable = new();
    public static Dictionary<float, string> bestScoreTable = new();
    // Start is called before the first frame update
    void Start()
    {
        score = 0; 
        BestScore = 0;
        bestScoreTable[0] = "None";
    }

    // Update is called once per frame
    void Update()
    {
        score = scoreTable[this.transform.parent.transform.parent.name];
        GetComponent<Text>().text = "Score: " + score.ToString() + "\nBest Score: " + BestScore.ToString() + " - " + bestScoreTable[BestScore]; // str(score)
    }

    public static void AddScore(string name, float amount)
    {
        if (name != null)
        {
            scoreTable[name] += amount;
            Debug.Log($"[{name}] 점수: {scoreTable[name]}");
        }
    }

    public static void RegisterAgent(string name)
    {
        if (name != null)
        {
            scoreTable[name] = 0f;
            Debug.Log($"[등록됨] {name} 점수 초기화됨.");
        }
    }

    public static void setBestScore(string name)
    {
        BestScore = scoreTable[name];
        bestScoreTable[BestScore] = name;
    }

    public static float getScore(string name)
    {
        return scoreTable[name];
    }
}
