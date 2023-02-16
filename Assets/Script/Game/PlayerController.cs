using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DemoObserver;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform left, right;
    private float step;
    private int targetStep;
    public static PlayerController instance;
    private Rigidbody2D rb;
    private float jumpPower, speedMove;
    [SerializeField] private List<Vector2> spawnPoint, listBackup;
    [SerializeField] private GameObject ladderPrefab;
    [SerializeField] private Transform obsParent, eviroment;
    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        spawnPoint = new List<Vector2>();
        listBackup = new List<Vector2>();

        targetStep = 12;
        jumpPower = 8.5f;
        speedMove = 5f;
        transform.position = left.position;

        DistanceStep();
        SetUpPoint();
    }

    private void OnEnable()
    {
        this.RegisterListener(EventID.GameOver, (param) => CancleLadder());
    }

    private void Start()
    {
        InvokeRepeating("SpawnLadder", 0, .5f);
    }



    private void Update()
    {
        ClampPos();
    }

    private void SpawnLadder()
    {
        var randomList = Random.Range(0, 2);
        if (randomList == 0) InstiateLadder(0, randomList);
        else InstiateLadder(20, randomList);
    }

    private void ClearPoint()
    {
        for (int i = 0; i < listBackup.Count; i++)
        {
            spawnPoint.Add(listBackup[i]);
        }
        listBackup.Clear();
    }

    private void InstiateLadder(int offset, int id)
    {
        if (spawnPoint.Count > 1)
        {
            var index = Random.Range(1, spawnPoint.Count);
            listBackup.Add(spawnPoint[index]);
            var ladder = Instantiate(ladderPrefab, new Vector3(spawnPoint[index].x, spawnPoint[index].y + offset), Quaternion.identity);
            ladder.transform.SetParent(obsParent);
            ladder.GetComponent<Ladder>().id = id;
            spawnPoint.Remove(spawnPoint[index]);
            Destroy(ladder, 6);
        }
        else ClearPoint();

    }


    private void SetUpPoint()
    {
        spawnPoint.Add(new Vector2(transform.position.x, -10));
        for (int i = 1; i < targetStep; i++)
        {
            spawnPoint.Add(new Vector2(transform.position.x + step * i, -10));
        }
    }
    private void ClampPos()
    {
        var posX = Mathf.Clamp(transform.position.x, left.position.x, right.position.x);
        transform.position = new Vector2(posX, transform.position.y);
    }

    private void SetGarvity(int gravity, Vector2 velocity)
    {
        rb.gravityScale = gravity;
        rb.mass = gravity;
        rb.velocity = velocity;
    }

    private void CancleLadder() => CancelInvoke();
    private void DistanceStep() => step = (Vector2.Distance(left.position, right.position)) / targetStep;
    public void MoveLeft() => transform.position = new Vector2(transform.position.x - step, transform.position.y);
    public void MoveRight() => transform.position = new Vector2(transform.position.x + step, transform.position.y);
    public void MoveUp() => transform.Translate(Vector2.up * speedMove * Time.deltaTime);
    public void MoveDown() => transform.Translate(Vector2.down * speedMove * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TAG.Ladder))
        {
            transform.SetParent(other.gameObject.transform);
            SetGarvity(0, Vector2.zero);
        }

        if (other.gameObject.CompareTag(TAG.Lose)) GameManager.instance.GameOver();
        if (other.gameObject.CompareTag(TAG.Score))
        {
            this.PostEvent(EventID.UpdateScore, 10);
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TAG.Ladder))
        {
            transform.SetParent(other.gameObject.transform);
            SetGarvity(0, Vector2.zero);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TAG.Ladder))
        {
            if (eviroment.gameObject.activeInHierarchy)
            {
                transform.SetParent(eviroment);
                SetGarvity(1, Vector2.zero);
            }

        }
       ;
    }

}
