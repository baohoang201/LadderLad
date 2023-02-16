using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Image logoScale;
    void Start()
    {
       ButtonScale();
    }
    private void OnclickPlay() => SceneManager.LoadScene(1);
    private void ButtonScale() => btnPlay.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo).Play();
    private void LogoScale() => logoScale.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo).Play();


}
