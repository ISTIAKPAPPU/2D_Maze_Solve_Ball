using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TouchAndGo : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;

    public GameObject endParticles;

    private Touch _touch;
    private Vector3 _touchPosition, _whereToMove;
    private bool _isMoving = false;
    private ParticleSystem _hitParticle;

    private float _previousDistanceToTouchPos, _currentDistanceToTouchPos;

    private void Start()
    {
        GameValues.IsGameOver = false;
        _hitParticle = transform.Find("Hit").gameObject.GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameValues.IsGameOver) return;
        if (_isMoving)
            _currentDistanceToTouchPos = (_touchPosition - transform.position).magnitude;

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                _previousDistanceToTouchPos = 0;
                _currentDistanceToTouchPos = 0;
                _isMoving = true;
                if (Camera.main is { }) _touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                _touchPosition.z = 0;
                _whereToMove = (_touchPosition - transform.position).normalized;
                _rb.velocity = new Vector2(_whereToMove.x * moveSpeed, _whereToMove.y * moveSpeed);
            }
        }

        if (_currentDistanceToTouchPos > _previousDistanceToTouchPos)
        {
            _isMoving = false;
            _rb.velocity = Vector2.zero;
        }

        if (_isMoving)
            _previousDistanceToTouchPos = (_touchPosition - transform.position).magnitude;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("EndGame"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.Hit);
            _hitParticle.Play();
        }
        else
        {
            if (endParticles.transform.GetChild(0).gameObject.activeSelf)
                endParticles.transform.GetChild(0).gameObject.SetActive(false);
            endParticles.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            GameValues.IsPlayerWin = true;
            GameValues.IsGameOver = true;
            PanelLoader.Instance.OnWinPanelStart();
            Destroy(transform.gameObject);
        }

        _isMoving = false;
        _rb.velocity = Vector2.zero;
    }
    
}