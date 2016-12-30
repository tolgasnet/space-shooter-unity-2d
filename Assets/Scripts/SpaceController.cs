using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour {

    public float meteorFrequencyInSeconds = 2.0f;
    public Rigidbody2D meteor;
    private const float meteorDestroyTime = 2.5f;

    // Use this for initialization
    void Start ()
    {
        var coroutine = InstantiateMeteor(meteorFrequencyInSeconds);
        StartCoroutine(coroutine);
    }

    private IEnumerator InstantiateMeteor(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            var meteorX = UnityEngine.Random.Range(-5.0f, 5.0f);
            var meteorPosition = new Vector3(meteorX, transform.position.y + 5);
            var meteorClone = Instantiate(meteor, meteorPosition, transform.rotation);
            Destroy(meteorClone.gameObject, meteorDestroyTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
		
	}
}
