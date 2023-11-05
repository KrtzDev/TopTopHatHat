using UnityEngine;

public class CollectibleTopHat : MonoBehaviour
{
    [SerializeField] int _amount = 1;
    private AudioSource _source;
    private bool _isCollected = false;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponentInParent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TopHatCharacter>() != null && !_isCollected)
        {
            Health _health = other.GetComponent<Health>();

            _health.IncreaseMaxHealth(_amount, false);
            _health.Heal(_amount);

            CollectTopHat();
        }
    }

    private void CollectTopHat()
    {
        _isCollected = true;
        _source.Play();
        gameObject.SetActive(false);
    }
}
