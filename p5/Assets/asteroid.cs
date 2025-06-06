using TMPro;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
	[SerializeField] private const int asteroidCount = 6;
    private GameObject[] asteroids;
    void Start()
    {
		asteroids = new GameObject[asteroidCount];
		for (int i = 0; i < asteroidCount; i++)
		{
			asteroids[i] = new();
			GameObject a = asteroids[i];
            a.AddComponent<SpriteRenderer>().sprite = sprite;
            a.AddComponent<BoxCollider2D>();
			a.tag = "Respawn";
            a.transform.SetParent(gameObject.transform);
            a.transform.localScale = new Vector3(10,10,1);
            a.transform.localEulerAngles = new Vector3(0,0,Random.Range(-180, 180));
				a.transform.position = 
					Camera.main.ScreenToWorldPoint(
						new Vector3(
							Random.Range(.2f, .8f) * Screen.width,
							Random.Range(0,1) * Screen.height,
							Camera.main.nearClipPlane
						)
					);
		}
    }

    void Update()
    {
		for (int i = 0; i < asteroidCount; i++)
		{	
			GameObject a = asteroids[i];
			Vector3 newPos = new Vector3(
				-Mathf.Sin(a.transform.eulerAngles.z * Mathf.Deg2Rad),
				Mathf.Cos(a.transform.eulerAngles.z * Mathf.Deg2Rad),
				Camera.main.nearClipPlane
			) * Time.deltaTime * 5.0f;
			
			newPos = Camera.main.WorldToScreenPoint(newPos + a.transform.position);

			if (newPos.x < -2 || newPos.x > Screen.width + 2 ||
				newPos.y < -2 || newPos.y > Screen.height + 2)
			{
				a.transform.position = 
					Camera.main.ScreenToWorldPoint(
						new Vector3(
							Random.Range(.2f, .8f) * Screen.width,
							Random.Range(0,2) * Screen.height,
							Camera.main.nearClipPlane
						)
					);
            	a.transform.localEulerAngles = new Vector3(0,0,Random.Range(-180, 180));
			}
			
			else
			{
				a.transform.position = Camera.main.ScreenToWorldPoint(newPos);
			}
		}
    }
}
