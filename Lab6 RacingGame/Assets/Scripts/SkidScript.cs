using UnityEngine;
using System.Collections;

public class SkidScript : MonoBehaviour {
	public float currentFriction;
	float skidAt;
	float skidSoundEmittion;
	float skidDensity;
	public GameObject skidSound;
	float skidMarkWidth;
	int skidding;
	Vector3[] lastPosition;
	public Material skidMaterial;

	// Use this for initialization
	void Start () {
		skidAt = 1.5f;
		skidSoundEmittion = 10f;
		skidDensity = 0f;
		skidMarkWidth = 0.2f;
		lastPosition = new Vector3[2];

	}
	
	// Update is called once per frame
	void Update () {
		WheelHit hit;
		WheelCollider wheel = (WheelCollider)(collider);
		wheel.GetGroundHit (out hit);
		currentFriction = Mathf.Abs(hit.sidewaysSlip);

		if(skidAt <= currentFriction && skidDensity <= 0)
		{
			Instantiate(skidSound, hit.point, Quaternion.identity);
			skidDensity = 1;
		}
		skidDensity -= skidSoundEmittion * Time.deltaTime;

		if(skidAt <= currentFriction)
		{
			SkidMesh();
		}
		else
		{
			skidding = 0;
		}
	}

	void SkidMesh()
	{
		WheelHit hit;
		WheelCollider wheel = (WheelCollider)(collider);
		wheel.GetGroundHit (out hit);
		GameObject mark = new GameObject ("Mark");
		mark.AddComponent<MeshFilter>();
		MeshFilter meshFilter = (MeshFilter) mark.GetComponent<MeshFilter>();

		mark.AddComponent<MeshRenderer>();

		Mesh treadMarkMesh = new Mesh ();
		Vector3[] vertices = new Vector3[4];
		int[] triangles = new int[6] {0, 1, 2, 2, 3, 0};
		Vector3 tempVector = new Vector3(skidMarkWidth, 0.01f, 0);
		if(skidding == 0)
		{
			tempVector.x = skidMarkWidth;
			vertices[0] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) *  tempVector;

			tempVector.x = -skidMarkWidth;
			vertices[1] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) *  tempVector;

			tempVector.x = -skidMarkWidth;
			vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) *  tempVector;

			tempVector.x = skidMarkWidth;
			vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) *  tempVector;

			lastPosition[0] = vertices[2];
			lastPosition[1] = vertices[3];
			skidding = 1;

		}
		else
		{
			vertices[1] = lastPosition[0];
			vertices[0] = lastPosition[1];

			tempVector.x = -skidMarkWidth;
			vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) *  tempVector;

			tempVector.x = skidMarkWidth;
			vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) *  tempVector;
		
			lastPosition[0] = vertices[2];
			lastPosition[1] = vertices[3];
		}
		treadMarkMesh.vertices = vertices;
		treadMarkMesh.triangles = triangles;
		treadMarkMesh.RecalculateNormals ();
		Vector2[] UVmap = new Vector2[treadMarkMesh.vertices.Length];
	
		UVmap[0] = new Vector2(1,0);
		UVmap[1] = new Vector2(0,0);
		UVmap[2] = new Vector2(0,1);
		UVmap[3] = new Vector2(1,1);

		treadMarkMesh.uv = UVmap;
		meshFilter.mesh = treadMarkMesh;
		mark.renderer.material = skidMaterial;
	}
}
