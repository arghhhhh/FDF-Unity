using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnim_3 : MonoBehaviour
{
    public bool loop = false;
    public float speed = 1f;
    public int frameRate = 30;
    private float timePerFrame = 0f;
    private float elapsedTime = 0f;
    private int currentFrame = 0;

    [SerializeField]
    private Sprite[] sprites;

    Vector3 previous;
    float velocity;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false;
        LoadSpriteSheet();
    }

    private void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>("mario_coon");
        if (sprites != null && sprites.Length > 0)
        {
            timePerFrame = 1f / frameRate * speed;
            Play();
        }
        else
            Debug.LogError("Failed to load spritesheet");
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timePerFrame)
        {
            elapsedTime = 0;
            currentFrame++;
            SetSprite();
            if (currentFrame > sprites.Length)
            {
                if (loop)
                    currentFrame = 0;
                else
                    enabled = false;
            }
        }        

        if (transform.position.x < previous.x)
            spriteRenderer.flipX = true;
            
        else
            spriteRenderer.flipX = false;

        previous = transform.position;
    }

    void SetSprite()
    {
        if (currentFrame >= 0 && currentFrame < sprites.Length)
            spriteRenderer.sprite = sprites[currentFrame];
    }

    public void Play()
    {
        enabled = true;
    }
}
