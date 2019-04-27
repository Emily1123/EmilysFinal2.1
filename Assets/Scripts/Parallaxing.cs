using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{
	public Transform[] backgrounds;

	private float[] _parallaxScales;
	public float smoothing = 1f;

	private Transform _cam;
	private Vector3 _previousCamPos;

	void Awake ()
    {
		_cam = Camera.main.transform;
	}

	void Start ()
    {
		_previousCamPos = _cam.position;

		_parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++)
        {
			_parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	void Update ()
    {
		for (int i = 0; i < backgrounds.Length; i++)
        {
			float parallax = (_previousCamPos.x - _cam.position.x) * _parallaxScales[i];
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		_previousCamPos = _cam.position;
	}
}
