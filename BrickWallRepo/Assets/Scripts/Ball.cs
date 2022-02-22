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
        _rigidbody = GetComponent<Rigidbody>();//balldaki deðerleri ýnspectordan ulaþmak için.
        _renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }
    void Launch()
    {
        _rigidbody.velocity = Vector3.up * _speed;//topu kaybettiðimizde yeni top yukarý cýkar.
    }
    
    void FixedUpdate()//**fixed update yaptýk cunku velocityi absorbe etti reflect fonksiyonu
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed; //**top ne kadar hýzlý yada yavaþ hareket ederse etsin hýzýný bi aralýðýa soktuk.
        _velocity = _rigidbody.velocity;//collisiondan önce yazdýkki reflect fonskiyonu absorbe etmesin ve baþka degere atadýk.
        if (!_renderer.isVisible)
        {
            GameManager.Instance.Balls--;
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);//**reflect sayesinde topu geldiði açýyla geri gönderdik ve paddlerla kontak oluþturduk.
    }




}
