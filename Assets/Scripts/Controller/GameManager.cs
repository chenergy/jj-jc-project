using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject character;

	private static GameManager instance = null;

	void Awake(){
		if (instance != null){
			GameObject.Destroy(this.gameObject);
		} else {
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		}
	}
}

