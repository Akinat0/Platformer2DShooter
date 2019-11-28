using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 10;
    [SerializeField] private float jumpSpeed = 20;
    [SerializeField] private float jumpDuration = 2;

    [SerializeField] private Sprite deadCudeSprite;
    
    private bool _isAlive = true;
    private bool _isGrounded = false;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float _movementDirection = 1;

    private Color[] _colors = {Color.cyan, Color.green, Color.red, Color.yellow, Color.white, Color.blue, Color.magenta, Color.gray};

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _spriteRenderer.color = _colors[Random.Range(0, _colors.Length)];
        
        Vector3 playerPos = FindObjectOfType<PlayerControl>().transform.position;

        if (transform.position.x > playerPos.x)
        {
            _movementDirection = -1; 
        }
        else
        {
            _spriteRenderer.flipX = true;
        }

        InvokeRepeating(nameof(Jump), jumpDuration, jumpDuration);
    }

    void Update()
    {
        if (_isAlive)
            _rigidbody.velocity = new Vector2(horizontalSpeed * _movementDirection, _rigidbody.velocity.y);
    }

    void Jump()
    {
        if (_isAlive && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _isGrounded = false;
        }
    }

    void Die()
    {
        _isAlive = false;
        _rigidbody.velocity = Vector2.zero;
        _spriteRenderer.sprite = deadCudeSprite;
        
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Enemy"))
            _isGrounded = true;
        
        if (coll.collider.CompareTag("Player") && _isAlive)
        {
            ReloadScene();
            return;
        }

        if (coll.collider.CompareTag("Bullet"))
        {
            Destroy(coll.gameObject);
            Die();
            return;
        }

        if (coll.collider.CompareTag("LevelEdge"))
        {
            Destroy(gameObject);
            return;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}