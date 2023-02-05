using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(NextRoom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator NextRoom()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
