using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DemoObserver;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private Text txtScore, txtHighScore;

    public void LoadText()
    {
        txtScore.text = ScoreManager.instance.score.ToString();
        var highScore = PlayerPrefs.GetInt("highScore");
        txtHighScore.text = highScore.ToString();
        this.PostEvent(EventID.OnSaveHighScore);

    }
    public void PlayButtonOnClick() => SceneManager.LoadScene(0);

}

