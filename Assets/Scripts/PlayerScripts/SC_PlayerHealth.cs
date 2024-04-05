using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerHealth : MonoBehaviour
{
    private Coroutine _damageCoroutine;
    //private BoxCollider _collider;      //player collider     OUDE VERSIE
    private CapsuleCollider _collider;


    [SerializeField] private int _health;
    public int CurrentHealthPlayer  //wordt NIET HIER (ge)Set maar in een fucntie
    {
        get { return _health; }
    }
    [SerializeField] private int _starterHealth;
    public int StarterHealthPlayer
    {
        get { return _starterHealth; }
    }

    [SerializeField] private int _shield;
    [SerializeField] private int _maxShield;

    [SerializeField] private float _damageDelay;
    private bool damageDelayOn = false;

    [SerializeField] private bool _invincibleMode;


    [SerializeField] protected GameObject hitEffectHp = null;
    [SerializeField] protected GameObject hitEffectShield = null;
    protected float hitEffectRepeatTime = 0.03f;
    protected float hitEffectTimer;

    private bool hpTookDamageWhenHit = true;    //puur voor hitEffect


    private void Awake()
    {
        //_collider = GetComponent<BoxCollider>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        // Set the health to the starter health
        _health = _starterHealth;
        Physics.IgnoreLayerCollision(6, 7, false);

        ServiceLocator.Main.HealthUIManager.UpdateHealthUI(_health);
        ServiceLocator.Main.ShieldUIManager.UpdateShieldUI(_shield);
    }


    private void FixedUpdate()
    {
        if (damageDelayOn)
        {
            if (hitEffectTimer < hitEffectRepeatTime)
            {
                hitEffectTimer += Time.fixedDeltaTime;
            }
            else
            {
                if (hpTookDamageWhenHit)
                {
                    if (hitEffectHp == isActiveAndEnabled) { hitEffectHp.SetActive(false); }
                    else { hitEffectHp.SetActive(true); }
                }
                else
                {
                    if (hitEffectShield == isActiveAndEnabled) { hitEffectShield.SetActive(false); }
                    else { hitEffectShield.SetActive(true); }
                }
                hitEffectTimer = 0;     //repeating timer
            }
        }
        else if (hitEffectTimer != 0)      //als damageDelayOn false is
        {
            if (hitEffectHp == isActiveAndEnabled) { hitEffectHp.SetActive(false); }
            if (hitEffectShield == isActiveAndEnabled) { hitEffectShield.SetActive(false); }
            hitEffectTimer = 0;
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (_invincibleMode == false)       //voor als je een powerup pakt; je kan alleen maar damage krijgen als je niet invincible bent
        {
            _damageCoroutine ??= StartCoroutine(TakeDamage());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_invincibleMode == false)
        {
            _damageCoroutine ??= StartCoroutine(TakeDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_invincibleMode == false)       //voor als je een powerup pakt; je kan alleen maar damage krijgen als je niet invincible bent
        {
                if (other.gameObject.CompareTag("Projectile") && other.gameObject.layer != 9)       //layer 9 is de layer van playerprojectiles
            {
                _damageCoroutine ??= StartCoroutine(TakeDamage());
            }
        }

    }

    private IEnumerator TakeDamage()
    {

        // Disable collision so you cant just run into enemies during invincible time
        // _collider.enabled = false;
        Physics.IgnoreLayerCollision(6, 7, true);
        damageDelayOn = true;

        // Remove one point of health and check if the health is 0 (or somehow less than 0)
        if (_shield > 0)
        {
            _shield--;
            hpTookDamageWhenHit = false;
            hitEffectShield.SetActive(true);
            ServiceLocator.Main.ShieldUIManager.UpdateShieldUI(_shield);
        }
        else
        {
            _health--;
            hpTookDamageWhenHit = true;
            hitEffectHp.SetActive(true);
            ServiceLocator.Main.HealthUIManager.UpdateHealthUI(_health);
        }
        

        if (_health <= 0)
        {
            ServiceLocator.Main.DeathScreen.ShowDeathScreen();
            Destroy(gameObject);
        }

        // Wait for the delay
        yield return new WaitForSeconds(_damageDelay);

        // Enable collision and let the coroutine be able to run again
        // _collider.enabled = true;
        Physics.IgnoreLayerCollision(6, 7, false);
        damageDelayOn = false;

        _damageCoroutine = null;

    }


    public void RestoreHealth()
    {
        if (_health < _starterHealth && _health > 0)
        {
            _health++;
            ServiceLocator.Main.HealthUIManager.UpdateHealthUI(_health);
        }
    }

    public void SetShield()
    {
        _shield = _maxShield;
        ShieldEffect();
        ServiceLocator.Main.ShieldUIManager.UpdateShieldUI(_shield);
    }


    public void PlayerInvincibleMode()              //wordt vanuit de "SC_Player" aangeroepen
    {
        Physics.IgnoreLayerCollision(6, 7, true);
        _invincibleMode = true;
    }

    public void EndPlayerInvincibleMode()
    {
        Physics.IgnoreLayerCollision(6, 7, false);
        _invincibleMode = false;
    }



    private void ShieldEffect()
    {
        hitEffectShield.SetActive(true);
        Invoke("TurnOffShieldEffect", 1);
    }

    private void TurnOffShieldEffect()
    {
        if (hitEffectShield == isActiveAndEnabled) { hitEffectShield.SetActive(false); }
    }


}
