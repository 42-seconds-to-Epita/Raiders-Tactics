using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamProjectile : BaseProjectile
{
    private int i = 0;
    private float attackSpeed;
    private GameObject launcher;
    private GameObject target;

    private Vector3 lastKnoPos;
    private Quaternion lastKnoRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(i);
        if (i != 5000)
        {
            i += (int) attackSpeed;
            if(lastKnoPos != target.transform.position){
                lastKnoPos = target.transform.position;
                lastKnoRot = Quaternion.LookRotation(lastKnoPos - transform.position);
            }
 
            if(transform.rotation != lastKnoRot){
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lastKnoRot, speed * Time.deltaTime);
            }
            return;
        }
        
        Destroy(this);
    }

    public override void FireProjectile(GameObject launcher, GameObject target, int damage, float attackSpeed)
    {
        this.attackSpeed = attackSpeed;
        this.target = target;
        this.launcher = launcher;
    }
}
