using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 20f;
    Rigidbody _rigidbody;
    Vector3 _velocity;
    Renderer _renderer;

   
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();//balldaki de�erleri �nspectordan ula�mak i�in.
        _renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }
    void Launch()
    {
        _rigidbody.velocity = Vector3.up * _speed;//topu kaybetti�imizde yeni top yukar� c�kar.
    }
    
    void FixedUpdate()//**fixed update yapt�k cunku velocityi absorbe etti reflect fonksiyonu
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed; //**top ne kadar h�zl� yada yava� hareket ederse etsin h�z�n� bi aral���a soktuk.
        _velocity = _rigidbody.velocity;//collisiondan �nce yazd�kki reflect fonskiyonu absorbe etmesin ve ba�ka degere atad�k.
        if (!_renderer.isVisible)
        {
            GameManager.Instance.Balls--;
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);//**reflect sayesinde topu geldi�i a��yla geri g�nderdik ve paddlerla kontak olu�turduk.
    }




}
