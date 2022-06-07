using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] 
    private Transform _bulletOrigin;
    
    [SerializeField] 
    private Transform _cameraOrigin;

    private GameObject _bullet;
    [SerializeField] 
    private ParticleSystem _muzzleFlash;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    private void OnUseLeft(InputValue useValue)
    {
        ShotLogic();
        ShotVisuals();
    }

    private void ShotLogic()
    {
        _bullet = ObjectPooler.SharedInstance.GetPooledObject(); 
        if (_bullet != null)
        {
            _bullet.transform.position = _bulletOrigin.position;
            _bullet.transform.LookAt(_cameraOrigin.position + _cameraOrigin.forward * 50f);
            _bullet.SetActive(true);
        }
    }

    private void ShotVisuals()
    {
        _muzzleFlash.Play();
    }
}
