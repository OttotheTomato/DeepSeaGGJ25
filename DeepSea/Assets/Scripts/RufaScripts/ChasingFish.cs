using game;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AnnoyingFish : MonoBehaviour
{
    private Rigidbody _RB;
    private Transform _PlayerTransform;
    private Vector3 _TargetPosition;

    public float _Speed = 2f, _ClearingRange = 0.5f, _TurningSpeed = 0.15f;
    public Vector3 _Offset = new Vector3(0f, 1f, 0f);

    private bool _SeesPlayer = false, _Patrolling = true;
    public bool _Flashed = false;

    private PatrolPoint[] _PatrolPoints;


    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _PlayerTransform = FindObjectOfType<PlayerMovement>().transform;

        _TargetPosition = transform.position;

        _PatrolPoints = FindObjectsOfType<PatrolPoint>();

        Debug.Log("PatrolPoints: " + _PatrolPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (_Flashed)
        {
            _TargetPosition = _PlayerTransform.position;
            Vector3 _Direction = (transform.position - _TargetPosition).normalized;

            transform.forward = Vector3.Lerp(transform.forward, _Direction, _TurningSpeed * Time.deltaTime);
            _RB.AddForce(_Direction * _Speed * 2f);
        }
        else if (CanSeeTarget(_PlayerTransform) && _SeesPlayer)
        {
            _TargetPosition = _PlayerTransform.position;
            Vector3 _Direction = (_TargetPosition + _Offset - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, _Direction, _TurningSpeed * Time.deltaTime);
            _RB.AddForce(_Direction * _Speed);            
        }
        else if (_Patrolling)
        {
            if (Vector3.Distance(transform.position, _TargetPosition) < _ClearingRange)
            {
                int _RandomInt = Random.Range(0, _PatrolPoints.Length);

                if (CanSeeTarget(_PatrolPoints[_RandomInt].transform))
                {
                    _TargetPosition = _PatrolPoints[_RandomInt].transform.position;
                }
            }
            else
            {
                Vector3 _Direction = (_TargetPosition - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, _Direction, _TurningSpeed * Time.deltaTime);

                _RB.AddForce(_Direction * _Speed);
            }
        }
        else
        {
            Vector3 _Direction = (_TargetPosition - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, _Direction, _TurningSpeed * Time.deltaTime);

            _RB.AddForce(_Direction * _Speed);

            if (Vector3.Distance(transform.position, _TargetPosition) < _ClearingRange)
                _Patrolling = true;
        }
    }

    private void OnCollisionEnter(Collision _Collision)
    {
        if (_Collision.transform == _PlayerTransform)
        {
            //Debug.Log("Collided with Player");

            PlayerOxygenController _OxygenControl = _Collision.gameObject.GetComponent<PlayerOxygenController>();
            _OxygenControl.RemoveOxygen(8f);
        }
    }
    
    private void OnTriggerEnter(Collider _Entity)
    {
        if (_Entity.transform == _PlayerTransform)
        {
            //Debug.Log("Found player");

            _SeesPlayer = true;
        }
    }

    private void OnTriggerExit(Collider _Entity)
    {
        if (_Entity.transform == _PlayerTransform)
        {
            //Debug.Log("Lost player");

            _SeesPlayer = false;
        }
    }
    
    private bool CanSeeTarget(Transform _Target)
    {
        RaycastHit[] _Objects = Physics.RaycastAll(transform.position, _Target.position + _Offset - transform.position, Vector3.Distance(transform.position, _Target.position));
        
        for (int i = 0; i < _Objects.Length; i++)
        {
            if (_Objects[i].collider.gameObject.CompareTag("Obstruction"))
                return false;
        }
        return true;
    }
}
