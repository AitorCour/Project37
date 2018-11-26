using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PushObjects : MonoBehaviour {
    public AudioClip soundClip;
    public float ObMass = 300;
    public float PushAtMass = 100;
    public float PushingTime;
    public float ForceToPush;
    Rigidbody rb;
    public float vel;
    AudioSource AudioPlayer;
    Vector3 dir;

    Vector3 lastPos ;
    float _PushingTime =0;

 

void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) return;

        AudioPlayer = GetComponent<AudioSource>();
        if (soundClip != null)
        {           
            AudioPlayer.clip = soundClip;
            AudioPlayer.Stop();
        }
        AudioPlayer.volume = 0;
        AudioPlayer.pitch = 0.5f;
        rb.mass = ObMass;
    }

    bool IsMoving()
    {
        if (rb.velocity.magnitude > 0.06f)
        {
            return true;
        }
        return false;

    }

     private void Update()
    {
        vel = rb.velocity.magnitude;
        if (Input.GetKeyUp(KeyCode.F))
        {
            rb.isKinematic = true;
            if (soundClip != null)
            {
                AudioPlayer.Stop();
            }

            AudioPlayer.volume = 0f;
            AudioPlayer.pitch = 0.2f;
        }

        if (rb.isKinematic==false)
        {
            _PushingTime += Time.deltaTime;
            if (_PushingTime >= PushingTime)
            {
                _PushingTime = PushingTime;
            }

            rb.mass = Mathf.Lerp(ObMass, PushAtMass, _PushingTime / PushingTime);
            rb.AddForce(dir * ForceToPush, ForceMode.Force);
        }
        else
        {
            rb.mass = ObMass;
            _PushingTime = 0;
           
        }

        if (IsMoving() == true && rb.isKinematic == false)
        {
            if (AudioPlayer.isPlaying == false)
            {
                AudioPlayer.Play();
                //AudioPlayer.loop = true ;
            }

           StartCoroutine( SoundChangeHigh());
        }
        else
        {

            //AudioPlayer.volume = 0f;
            //AudioPlayer.pitch = 0.2f;
            StartCoroutine(SoundChangeLow());
        }
       
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagManager.Player)
        {
            
            if (Input.GetKey(KeyCode.F))
            {
               

                rb.isKinematic = false;
                          

                dir = collision.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
              
               
            }
        }

    }


    IEnumerator SoundChangeHigh()
    {
        if (Input.GetKey(KeyCode.F))
        {
            AudioPlayer.volume = Mathf.Lerp(0, 0.5f, PushAtMass / rb.mass);
            AudioPlayer.pitch = Mathf.Lerp(0.2f, 1f, PushAtMass / rb.mass);
        }
        yield return new WaitForSeconds(0.1f);

    }
    IEnumerator SoundChangeLow()
    {
        if (Input.GetKey(KeyCode.F))
        {
            AudioPlayer.volume =1- Mathf.Lerp(0F, 0.5f, Time.deltaTime);
            AudioPlayer.pitch = 1- Mathf.Lerp(0.2f, 1f, Time.deltaTime);
        }

        yield return new WaitForSeconds(0.1f);
    }
  
}
