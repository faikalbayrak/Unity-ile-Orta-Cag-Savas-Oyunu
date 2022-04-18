using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAxe : MonoBehaviour,IWeapons
{
    float damage = 10;
    float Range;
    Animator anim;

    public AudioSource AttackVoice;
    public AudioSource EquipVoice;
    public AudioSource UnEquipVoice;

    public ParticleSystem AttackTrace;
    public ParticleSystem BloodEffect;
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        anim = GetAnimator();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (anim.GetBool("isAttacking"))
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().healt -= (damage - other.gameObject.GetComponent<Enemy>().armor);
                other.gameObject.GetComponent<Enemy>().armor_healt -= damage;
                Debug.Log(other.gameObject.GetComponent<Enemy>().healt);
                BloodEffect.Play();
            }
        }
        
    }

    public Animator GetAnimator()
    {
        return transform.GetComponentInParent<Animator>();
    }
}
