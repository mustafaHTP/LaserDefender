using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Player Config")]
    [Space(1)]
    [SerializeField] private bool isPlayer;

    [Header("Score")]
    [Space(1)]
    [SerializeField] private int deathScore;

    [Header("Health Config")]
    [SerializeField] private int healthAmount;

    [Header("VFX Explosion")]
    [SerializeField] private ParticleSystem vfxExplosion;

    [Header("Camera Shake Config")]
    [SerializeField] private bool applyCameraShake;

    private int _currentHealth;

    public int CurrentHealth { get => _currentHealth; }
    public int HealthAmount { get => healthAmount; }

    private LevelManager _levelManager;
    private AudioPlayer _audioPlayer;
    private CameraShaker _cameraShaker;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _currentHealth = healthAmount;

        _levelManager = FindObjectOfType<LevelManager>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _cameraShaker = FindObjectOfType<CameraShaker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.TryGetComponent(out DamageDealer damageDealer))
        {
            TakeDamage(damageDealer.DamageAmount);
            _audioPlayer.PlayDamageSFX();
            ShakeCamera();
            CreateVFXExplosion();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            _scoreKeeper.AddScore(deathScore);
        }
        else
        {
            _levelManager.LoadGameOver();
        }

        Destroy(gameObject);
    }

    private void CreateVFXExplosion()
    {
        ParticleSystem cloneVFX = Instantiate(
            vfxExplosion,
            transform.position,
            Quaternion.identity);

        Destroy(cloneVFX, cloneVFX.main.duration);
    }

    private void ShakeCamera()
    {
        if (_cameraShaker != null && applyCameraShake)
        {
            _cameraShaker.Play();
        }
    }
}
