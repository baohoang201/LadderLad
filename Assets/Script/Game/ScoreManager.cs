using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DemoObserver;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private Text tmpScore;
    private Action<object> _OnReceiveEventRef, _OnReceiveEventRef2;
    public int score;

    void Awake()
    {
        instance = this;
        score = 0;
        _OnReceiveEventRef = (param) => UpdateScore((int) param);
        _OnReceiveEventRef2 = (param) => SaveHighScore();
    }

    void OnEnable()
    {
        this.RegisterListener(EventID.UpdateScore, (param) => UpdateScore((int)param));
        this.RegisterListener(EventID.OnSaveHighScore, (param) => SaveHighScore());
    }


    public void SaveHighScore()
    {
        var high = PlayerPrefs.GetInt("highScore");
        if (score > high) PlayerPrefs.SetInt("highScore", score);
    }

    public void UpdateScore(int scorePlus)
    {
        score += scorePlus;
        tmpScore.text = score.ToString();
    }
}
