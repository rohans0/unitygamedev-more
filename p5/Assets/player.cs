using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500.0f;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private GameObject score;
	

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(
            new Vector3(
                0,
                0,
                (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) - (Input.GetKey(KeyCode.RightArrow) ? 1 : 0)
            ) * Time.deltaTime * rotationSpeed
        );

		movementSpeed += (
			(Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0)
		) * .03f;

		Vector3 newPos = new Vector3(
			-Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad),
			Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad),
			0
		);

		transform.position += newPos * Time.deltaTime * movementSpeed;
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
		TextMeshProUGUI s = score.GetComponent<TextMeshProUGUI>();
		if (other.transform.CompareTag("Finish"))
		{
			other.transform.position = 
				Camera.main.ScreenToWorldPoint(
					new Vector3(
						Random.Range(.05f, .95f) * Screen.width,
						Random.Range(.05f, .95f) * Screen.height,
						Camera.main.nearClipPlane
					
				));
			s.text = (int.Parse(s.text) + 1).ToString();
		}
		else if (other.transform.CompareTag("Respawn"))
		{
			transform.position = 
				Camera.main.ScreenToWorldPoint(
					new Vector3(
						Random.Range(.05f, .95f) * Screen.width,
						Random.Range(.05f, .95f) * Screen.height,
						Camera.main.nearClipPlane
					)
				);
			s.text = "0";
		}
    }
    
}
