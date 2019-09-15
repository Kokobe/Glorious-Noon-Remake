using UnityEngine;
using System.Collections.Generic;

public class WaveSim : MonoBehaviour {

	public GameObject plane;
	public int waves;
	public float speedDivder = 5f;
	Mesh mesh;
	List<Vector3> vertPos = new List<Vector3>();
	// Use this for initialization
	void Start () {
	
		mesh = plane.GetComponent<MeshFilter> ().mesh;


		for (int i = 0; i < (mesh.vertexCount); i++) {


			Vector3 v = mesh.vertices [i];


			v.y += Random.Range (-1f, 1f) * 10f;
			vertPos.Add (v);




		}
		mesh.SetVertices (vertPos);

	}

	void UpdateVertices(){
		vertPos.Clear ();


			for (int i = 0; i < (mesh.vertexCount); i++) {

				
					Vector3 v = mesh.vertices [i];


			v.y += Mathf.Sin (Time.time*.9f + i % 10)/7f;
					vertPos.Add (v);

				


			}





		mesh.SetVertices (vertPos);

	}

	// Update is called once per frame
	void Update () {
		
		plane.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (Time.time/speedDivder, Time.time/speedDivder);
		UpdateVertices ();
	}
}
