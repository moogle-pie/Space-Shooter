using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public or private reference, private usually has _variable
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned
    [SerializeField] //allows private variables to be manipulated by designers but not by players or other objects
    private float _speed = 3.5f; // meters per second. default value of 0 if no = 
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;

    


       // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0) **application, teleportation is just a position change with some particle effects
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //MOVEMENT CONTROL: Y,X
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //you can also have a local variable.. 
        //Vector3 direction = new Vector3(horizontalInput, verticalInpuit, 0);
        //transfor.Translate(direction * _speed * Time.deltaTime);

        //MOVEMENT RESTRICTION, BETWEEN 2 VALUES ON THE Y
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //PLAYER WRAPPING
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRate;
            
        if (_isTripleShotActive == true)
        {
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        //if tripleshot active is true, fire 3 lasers
        //else fire 1 laser
        //instantiate 3 lasers 
    }

    public void Damage()
    {
        _lives -= 1;

        if(_lives < 1)
        {
            //communicate w/ spawn manager to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
