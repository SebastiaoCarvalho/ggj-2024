using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI liveText;
    
    public Image healthBar;
    private float _hp = 0f;
    private float _maxHealth = 100f;
    private int _lives = 3;
    private float _lerpSpeed;

    public float HP {
        get { return _hp; }
    }

    public int Lives {
        get { return _lives; }
    }
    private Vector3 _startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        liveText.text = "x" + _lives;
        if (_hp > _maxHealth) _hp = _maxHealth;

        _lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }


    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, _hp / _maxHealth, _lerpSpeed);
    }

    public void ColorChanger()
    {
        Color _healthColor = Color.Lerp(Color.red, Color.green, (_hp / _maxHealth));

        healthBar.color =_healthColor;

    }


    public void TakeDamage(float damage) {
        _hp += damage;
        Debug.Log("Player HP: " + _hp);
    }

    public bool OutOfBounds() {
        return transform.position.y < -10;
    }

    public void Respawn() {
        _lives--;
        transform.position = _startPosition;
        _hp = 0f;
        GetComponent<Damageable>().HP = 0f;
    }

}
