using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnnoyingFish : MonoBehaviour
{
    private Rigidbody _RB;
    private Transform _TargetTransform, _PlayerTransform;

    public float _Speed = 2f;
    public Vector3 _Offset = new Vector3(0f, 1f, 0f);

    private bool _SeesPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _PlayerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer() && _SeesPlayer)
        {
            Vector3 _Direction = (_PlayerTransform.position + _Offset - transform.position).normalized;
            transform.forward = _Direction;
            _RB.AddForce(_Direction * _Speed);            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == _PlayerTransform)
        {
            Debug.Log("Collided with Player");
        }
    }
    
    private void OnTriggerEnter(Collider _Entity)
    {
        if (_Entity.transform == _PlayerTransform)
        {
            Debug.Log("Found player");

            _SeesPlayer = true;
        }
    }

    private void OnTriggerExit(Collider _Entity)
    {
        if (_Entity.transform == _PlayerTransform)
        {
            Debug.Log("Lost player");

            _SeesPlayer = false;
        }
    }
    
    private bool CanSeePlayer()
    {
        RaycastHit[] _Objects = Physics.RaycastAll(transform.position, _PlayerTransform.position + _Offset - transform.position);
        Debug.Log(_Objects.Length);
        
        for (int i = 0; i < _Objects.Length; i++)
        {
            if (_Objects[i].collider.gameObject.CompareTag("Obstruction"))
                return false;
        }
        return true;
    }
}
