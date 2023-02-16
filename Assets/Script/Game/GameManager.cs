using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DemoObserver;
public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private Transform appleParent;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private List<Transform> spawnPoint, listBackup;
    public static GameManager instance;
    void Awake()
    {
        instance = this;
        listBackup = new List<Transform>();

        InvokeRepeating("InstiateApple", 0, 2);
    }

    private void InstiateApple()
    {
        if (appleParent.childCount < 5)
        {
            if (spawnPoint.Count > 1)
            {
                var index = Random.Range(0, spawnPoint.Count);
                listBackup.Add(spawnPoint[index]);
                var apple = Instantiate(applePrefab, spawnPoint[index].position, Quaternion.identity);
                apple.transform.SetParent(appleParent);
                spawnPoint.Remove(spawnPoint[index]);
                Destroy(apple, 6);
            }
            else ClearPoint();
        }

    }

    private void ClearPoint()
    {
        for (int i = 0; i < listBackup.Count; i++)
        {
            spawnPoint.Add(listBackup[i]);
        }
        listBackup.Clear();
    }

    public void GameOver()
    {
        UIManager.EnableGameOverPopUp(true);
        gameOverPopup.LoadText();
        UIManager.EnableMainGame(false);
        UIManager.EnableEnviroment(false);
        CancelInvoke();
        this.PostEvent(EventID.OnSaveHighScore);
        this.PostEvent(EventID.GameOver);
    }

}
