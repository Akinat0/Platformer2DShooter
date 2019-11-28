using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float horizontalSpeed;

    private bool _isGrounded = false;
    private bool _looksAtRightSide = true;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update () 
    {
        //Jumping
        if (Input.GetKeyDown (KeyCode.UpArrow)) 
        {
            if(_isGrounded)
            {
                _rigidbody2D.velocity = new Vector2 (_rigidbody2D.velocity.x, jumpVelocity);
                _isGrounded = false;
            }
        }

        float horizontalVelocity = 0;

        //Left Right Movement
        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
        {
            horizontalVelocity = -horizontalSpeed;
        }
        if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
        {
            horizontalVelocity = horizontalSpeed;
        }

        _rigidbody2D.velocity = new Vector2 (horizontalVelocity, _rigidbody2D.velocity.y);

        //Rotate hero
        if (_rigidbody2D.velocity.x < 0 && !_looksAtRightSide 
            || _rigidbody2D.velocity.x > 0 && _looksAtRightSide)
        {
            transform.Rotate(new Vector3(0, 180));
            _looksAtRightSide = !_looksAtRightSide;
        }
    }


    void OnCollisionEnter2D(Collision2D coll) {
        
        Debug.Log(coll.collider.tag);
        
        if(coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Enemy"))
            _isGrounded = true;
        
        if(coll.collider.CompareTag("LevelEdge"))
            RestartLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Portal"))
            LoadNextLevel();
    }


    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

