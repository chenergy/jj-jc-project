using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
	public static SceneController Instance { get; private set; }

	private Coroutine loadSceneRoutine;

	public void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Instance.transform.position = transform.position;
			Destroy (this.gameObject);
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}
	}

	public void LoadNewScene(string sceneName)
	{
		if (loadSceneRoutine != null)
		{
			return;
		}

		loadSceneRoutine = StartCoroutine (LoadNewSceneRoutine (sceneName));
	}

	private IEnumerator LoadNewSceneRoutine(string sceneName)
	{
		LightAcceptorTrigger lat = FindObjectOfType<LightAcceptorTrigger> ();
		PlayerCharacter pc = FindObjectOfType<PlayerCharacter> ();
		SpriteRenderer cameraSprite = Camera.main.GetComponentInChildren<SpriteRenderer> ();
		float startCameraSize = Camera.main.orthographicSize;

		if (lat != null)
		{
			float timer = 0;
			float duration = 2;
			Vector3 startPos = Camera.main.transform.position;
			Quaternion startRot = Camera.main.transform.rotation;

			while (timer < duration)
			{
				float t = Mathf.Sin (timer / duration * Mathf.PI / 2);
				Camera.main.transform.position = Vector3.Lerp (startPos, lat.cameraTransform.position, t);
				Camera.main.transform.rotation = Quaternion.Lerp (startRot, lat.cameraTransform.rotation, t);
				Camera.main.orthographicSize = Mathf.Lerp (startCameraSize, 5f, t);
				timer += Time.deltaTime;
				yield return null;
			}

			Camera.main.transform.position = lat.cameraTransform.position;
			Camera.main.transform.rotation = lat.cameraTransform.rotation;
			Camera.main.orthographicSize = 5f;

			yield return new WaitForSeconds (3f);
		}

		if (cameraSprite != null)
		{
			float timer = 0;
			float duration = 1;
			while (timer < duration)
			{
				cameraSprite.color = new Color (0, 0, 0, timer / duration);
				timer += Time.deltaTime;
				yield return null;
			}

			cameraSprite.color = Color.black;
		}

		Camera.main.orthographicSize = 10f;
		Camera.main.transform.localPosition = Vector3.zero;
		Camera.main.transform.localRotation = Quaternion.identity;

		SceneManager.LoadScene (sceneName);

		if (cameraSprite != null)
		{
			float timer = 0;
			float duration = 1;
			while (timer < duration)
			{
				cameraSprite.color = new Color (0, 0, 0, 1 - timer / duration);
				timer += Time.deltaTime;
				yield return null;
			}

			cameraSprite.color = new Color (0, 0, 0, 0);
		}

		loadSceneRoutine = null;
	}
}

