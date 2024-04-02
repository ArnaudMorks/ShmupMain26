using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_PickupUIManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve _textSizeCurve;
    [SerializeField] private float _textAnimationSpeed;
    [SerializeField] private TextMeshProUGUI[] _pickupTexts;
    [SerializeField] private float _defaultSize;
    private float[] _pickupTextAnimationIndex = new float[3];
    private int _bulletSpeedAmount = 0;
    private int _fireRateAmount = 0;
    private int _movementSpeedAmount = 0;

    private void Update()
    {
        for(int i = 0; i < _pickupTextAnimationIndex.Length; i++)
        {
            if (_pickupTextAnimationIndex[i] > 0)
            {
                _pickupTextAnimationIndex[i] -= _textAnimationSpeed * Time.deltaTime;
            }

            if (_pickupTextAnimationIndex[i] < 0)
            {
                _pickupTextAnimationIndex[i] = 0;
            }

            _pickupTexts[i].fontSize = _textSizeCurve.Evaluate(_pickupTextAnimationIndex[i]) * _defaultSize;
        }
    }

    public void ModifyPickupAmount(string pickup)
    {
        switch(pickup)
        {
            case "BulletSpeed":
                if (_pickupTexts[0].IsActive() == false) { _pickupTexts[0].gameObject.SetActive(true); }

                _bulletSpeedAmount++;
                _pickupTextAnimationIndex[0] = 1;
                _pickupTexts[0].text = _bulletSpeedAmount.ToString();
                break;
            case "FireRate":
                if (_pickupTexts[1].IsActive() == false) { _pickupTexts[1].gameObject.SetActive(true); }

                _fireRateAmount++;
                _pickupTextAnimationIndex[1] = 1;
                _pickupTexts[1].text = _fireRateAmount.ToString();
                break;
            case "MovementSpeed":
                if (_pickupTexts[2].IsActive() == false) { _pickupTexts[2].gameObject.SetActive(true); }

                _movementSpeedAmount++;
                _pickupTextAnimationIndex[2] = 1;
                _pickupTexts[2].text = _movementSpeedAmount.ToString();
                break;
        }
    }
}
