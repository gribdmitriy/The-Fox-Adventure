using UnityEngine;

public class Parallax : MonoBehaviour {

		private float startPosX, startPosY;
		public GameObject cam;
		public float parallaxEffect;

		void Start () {
				startPosX = transform.position.x;
				startPosY = transform.position.y;
		}

		void Update () {
				float distX = (cam.transform.position.x*parallaxEffect);
				float distY = (cam.transform.position.y*(parallaxEffect/2));
				transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
		}

}
