using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;
    public int id;
    private float speedMove;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        RandomSprite();
    }

    private void RandomSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        box.size = spriteRenderer.sprite.bounds.size;
        speedMove = 3;
    }

    void Update()
    {
        if (id == 0) transform.Translate(Vector2.up * speedMove * Time.deltaTime);
        else transform.Translate(Vector2.down * speedMove * Time.deltaTime);
    }
}
