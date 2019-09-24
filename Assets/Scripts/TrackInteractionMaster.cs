using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackInteractionMaster : MonoBehaviour
{
    public Text timeText, healthText;
    public GameObject destroyable1_1, destroyable1_2, destroyable2, destroyable3_1, destroyable3_2;
    float time = 120;
    // Start is called before the first frame update
    void Update()
    {

        time -= Time.deltaTime;
        timeText.text = time.ToString();
        if (time < 105)
        {
            Debug.Log("Caida");
            //DropBlock(destroyable1_1);
            //DropBlock(destroyable1_2);
        }
        if (time < 110)
        {
            Destroy(destroyable2);
        }

        if (time < 90)
        {
            Rigidbody destroyRigid1 = destroyable3_1.AddComponent<Rigidbody>();
            Rigidbody destroyRigid2 = destroyable3_2.AddComponent<Rigidbody>();
            destroyRigid1.useGravity = true;
            destroyRigid2.useGravity = true;
        }
    }


}
