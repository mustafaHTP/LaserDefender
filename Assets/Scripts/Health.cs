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

    public int HealthAmount { get => healthAmount; }

    private AudioPlayer _audioPlayer;
    private CameraShaker _cameraShaker;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
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
        healthAmount -= damageAmount;
        if (healthAmount <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            _scoreKeeper.AddScore(deathScore);
            Debug.Log("score: " + _scoreKeeper.Score);
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
