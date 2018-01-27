using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cool : MonoBehaviour {

	public void MoveCube()
    {
        StartCoroutine(Test()); Debug.Log("Check One");
    }

    private IEnumerator Test()
    {
        float lerp = 0;
        while(lerp < 1)
        {
            lerp += Time.deltaTime;
            transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 2, 0), lerp);
            yield return new WaitForEndOfFrame();
        }
    }
}
