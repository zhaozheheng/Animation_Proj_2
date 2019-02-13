using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;
using System.IO;

public class ParseSkel : MonoBehaviour {

	//Stack<Cube_node> stackCube = new Stack<Cube_node> ();
	//Create_tree tree = null;
	//public File Text = 
	GameObject gameObject;
	string filePath = "dragon4unity.skel";
	private Mesh m;
	public Material mat;
	private string firstName;
	// Use this for initialization
	void Start () {
		parseFile ();
	}

	public void parseFile() {
		Stack<Cube_node> stackCube = new Stack<Cube_node> ();

		List<string> lines = new List<string> (File.ReadAllLines(filePath));

		string childName = "";
		Cube_node root = null;
		//Cube_node childNode;
		string[] childSplit;
		float[] nowOffset = { 0, 0, 0 };
		float[] nowBoxmin = { -0.1f, -0.1f, -0.1f };
		float[] nowBoxmax = { 0.1f, 0.1f, 0.1f };
		float[] nowPose = { 0, 0, 0 };
		float[] nowRotxlimit = { -100000, 100000 };
		float[] nowRotylimit = { -100000, 100000 };
		float[] nowRotzlimit = { -100000, 100000 };

		for (int i = 0; i < lines.Count; i = i + 2) {

			if (lines[i].Contains("{") && i == 0) {
				childSplit = lines [i].Split (' ');
				childName = childSplit [1];
				firstName = childSplit [1];
				i = i + 2;
				//print (childName);
				while (!lines[i].Contains("{") && !lines[i].Contains("}")) {
					if (lines[i].Contains("offset")) {
						childSplit = lines [i].Split (' ');
						nowOffset[0] = float.Parse(childSplit[1]);
						nowOffset[1] = float.Parse(childSplit[2]);
						nowOffset[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("boxmin")) {
						childSplit = lines [i].Split (' ');
						nowBoxmin[0] = float.Parse(childSplit[1]);
						nowBoxmin[1] = float.Parse(childSplit[2]);
						nowBoxmin[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("boxmax")) {
						childSplit = lines [i].Split (' ');
						nowBoxmax[0] = float.Parse(childSplit[1]);
						nowBoxmax[1] = float.Parse(childSplit[2]);
						nowBoxmax[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("pose")) {
						childSplit = lines [i].Split (' ');
						nowPose[0] = float.Parse(childSplit[1]);
						nowPose[1] = float.Parse(childSplit[2]);
						nowPose[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("rotxlimit")) {
						childSplit = lines [i].Split (' ');
						nowRotxlimit[0] = float.Parse(childSplit[1]);
						nowRotxlimit[1] = float.Parse(childSplit[2]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("rotylimit")) {
						childSplit = lines [i].Split (' ');
						nowRotylimit[0] = float.Parse(childSplit[1]);
						nowRotylimit[1] = float.Parse(childSplit[2]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("rotzlimit")) {
						childSplit = lines [i].Split (' ');
						nowRotzlimit[0] = float.Parse(childSplit[1]);
						nowRotzlimit[1] = float.Parse(childSplit[2]);
						i = i + 2;
						continue;
					}
				}

				root = new Cube_node(childName, nowOffset, nowBoxmin, nowBoxmax, nowPose, nowRotxlimit, nowRotylimit, nowRotzlimit);
				//tree = new Create_tree (root);
				drawMesh (root);
				stackCube.Push(root);
				//continue;
			}

			Cube_node childNode = null;

			if (lines[i].Contains("{")) {
				childSplit = lines [i].Split (' ');
				childName = childSplit [1];
				i = i + 2;

				while (!lines[i].Contains("{") && !lines[i].Contains("}")) {
					if (lines[i].Contains("offset")) {
						childSplit = lines [i].Split (' ');
						nowOffset[0] = float.Parse(childSplit[1]);
						nowOffset[1] = float.Parse(childSplit[2]);
						nowOffset[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("boxmin")) {
						childSplit = lines [i].Split (' ');
						nowBoxmin[0] = float.Parse(childSplit[1]);
						nowBoxmin[1] = float.Parse(childSplit[2]);
						nowBoxmin[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("boxmax")) {
						childSplit = lines [i].Split (' ');
						nowBoxmax[0] = float.Parse(childSplit[1]);
						nowBoxmax[1] = float.Parse(childSplit[2]);
						nowBoxmax[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("pose")) {
						childSplit = lines [i].Split (' ');
						nowPose[0] = float.Parse(childSplit[1]);
						nowPose[1] = float.Parse(childSplit[2]);
						nowPose[2] = float.Parse(childSplit[3]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("rotxlimit")) {
						childSplit = lines [i].Split (' ');
						nowRotxlimit[0] = float.Parse(childSplit[1]);
						nowRotxlimit[1] = float.Parse(childSplit[2]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("rotylimit")) {
						childSplit = lines [i].Split (' ');
						nowRotylimit[0] = float.Parse(childSplit[1]);
						nowRotylimit[1] = float.Parse(childSplit[2]);
						i = i + 2;
						continue;
					}

					if (lines[i].Contains("rotzlimit")) {
						childSplit = lines [i].Split (' ');
						nowRotzlimit[0] = float.Parse(childSplit[1]);
						nowRotzlimit[1] = float.Parse(childSplit[2]);
						i = i + 2;
						continue;
					}
				}
				Cube_node fatherNode = stackCube.Peek ();
				childNode = new Cube_node (childName, fatherNode, nowOffset, nowBoxmin, nowBoxmax, nowPose, nowRotxlimit, nowRotylimit, nowRotzlimit);
				drawMesh (childNode);
				stackCube.Push(childNode);
				//tree.link_fach (fatherNode, childNode);
				i = i - 2;
				continue;
			}
			if (lines[i].Contains("}") && stackCube.Count!=0) {
				stackCube.Pop ();
			}


		}
	}

	public void drawMesh(Cube_node cnode){
		/*MeshFilter filter = gameObject.AddComponent< MeshFilter >();
		Mesh mesh = filter.mesh;
		mesh.Clear();*/

		#region Vertices
		Vector3 p0 = new Vector3( cnode.boxmin[0],	cnode.boxmin[1], cnode.boxmax[2] );
		Vector3 p1 = new Vector3( cnode.boxmax[0], 	cnode.boxmin[1], cnode.boxmax[2] );
		Vector3 p2 = new Vector3( cnode.boxmax[0], 	cnode.boxmin[1], cnode.boxmin[2] );
		Vector3 p3 = new Vector3( cnode.boxmin[0],	cnode.boxmin[1], cnode.boxmin[2] );	

		Vector3 p4 = new Vector3( cnode.boxmin[0],	cnode.boxmax[1],  cnode.boxmax[2] );
		Vector3 p5 = new Vector3( cnode.boxmax[0], 	cnode.boxmax[1],  cnode.boxmax[2] );
		Vector3 p6 = new Vector3( cnode.boxmax[0], 	cnode.boxmax[1],  cnode.boxmin[2] );
		Vector3 p7 = new Vector3( cnode.boxmin[0],	cnode.boxmax[1],  cnode.boxmin[2] );

		Vector3[] vertices = new Vector3[]
		{
			// Bottom
			p0, p1, p2, p3,

			// Left
			p7, p4, p0, p3,

			// Front
			p4, p5, p1, p0,

			// Back
			p6, p7, p3, p2,

			// Right
			p5, p6, p2, p1,

			// Top
			p7, p6, p5, p4
		};
		#endregion

		#region Normales
		Vector3 up 	= Vector3.up;
		Vector3 down 	= Vector3.down;
		Vector3 front 	= Vector3.forward;
		Vector3 back 	= Vector3.back;
		Vector3 left 	= Vector3.left;
		Vector3 right 	= Vector3.right;

		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,

			// Left
			left, left, left, left,

			// Front
			front, front, front, front,

			// Back
			back, back, back, back,

			// Right
			right, right, right, right,

			// Top
			up, up, up, up
		};
		#endregion	

		#region UVs
		Vector2 _00 = new Vector2( 0f, 0f );
		Vector2 _10 = new Vector2( 1f, 0f );
		Vector2 _01 = new Vector2( 0f, 1f );
		Vector2 _11 = new Vector2( 1f, 1f );

		Vector2[] uvs = new Vector2[]
		{
			// Bottom
			_11, _01, _00, _10,

			// Left
			_11, _01, _00, _10,

			// Front
			_11, _01, _00, _10,

			// Back
			_11, _01, _00, _10,

			// Right
			_11, _01, _00, _10,

			// Top
			_11, _01, _00, _10,
		};
		#endregion

		#region Triangles
		int[] triangles = new int[]
		{
			// Bottom
			3, 1, 0,
			3, 2, 1,			

			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,

			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,

			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,

			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,

			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

		};
		#endregion

		/*mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		mesh.RecalculateBounds();
		mesh.Optimize();*/
		//vertices for a cube
		/*Vector3 v000 = new Vector3(cnode.boxmin[0], cnode.boxmin[1], cnode.boxmin[2]);
		Vector3 v001 = new Vector3(cnode.boxmin[0], cnode.boxmin[1], cnode.boxmax[2]);
		Vector3 v100 = new Vector3(cnode.boxmax[0], cnode.boxmin[1], cnode.boxmin[2]);
		Vector3 v101 = new Vector3(cnode.boxmax[0], cnode.boxmin[1], cnode.boxmax[2]);	

		Vector3 v010 = new Vector3(cnode.boxmin[0], cnode.boxmax[1], cnode.boxmin[2]);
		Vector3 v011 = new Vector3(cnode.boxmin[0], cnode.boxmax[1], cnode.boxmax[2]);
		Vector3 v110 = new Vector3(cnode.boxmax[0], cnode.boxmax[1], cnode.boxmin[2]);
		Vector3 v111 = new Vector3(cnode.boxmax[0], cnode.boxmax[1], cnode.boxmax[2]);

		Vector3[] vertices = new Vector3[] {
			// Bottom
			v000,v100,v101,v001,

			// Left
			v000,v001,v011,v010,

			// Front
			v000,v010,v110,v100,

			// Back
			v001,v101,v111,v011,

			// Right
			v100,v110,v111,v101,

			// Top
			v010,v011,v111,v110
		};
		//triangles for a cube
		int[] triangles = new int[] {
			// Bottom
			3, 1, 0,
			3, 2, 1,			

			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,

			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,

			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,

			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,

			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
		};


		Vector3 up 		= Vector3.up;
		Vector3 down 	= Vector3.down;
		Vector3 front 	= Vector3.forward;
		Vector3 back 	= Vector3.back;
		Vector3 left 	= Vector3.left;
		Vector3 right 	= Vector3.right;
		//print (right);
		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,

			// Left
			left, left, left, left,

			// Front
			front, front, front, front,

			// Back
			back, back, back, back,

			// Right
			right, right, right, right,

			// Top
			up, up, up, up
		};*/

		GameObject gameObject = new GameObject (cnode.objectName);
		//print (cnode.objectName);
		/*print (cnode.offset);
		print (cnode.boxmin);
		print (cnode.boxmax);
		print (cnode.pose);
		print (cnode.rotxlimit);
		print (cnode.rotylimit);
		print (cnode.rotzlimit);*/
		if (cnode.objectName != firstName){
			string fatherName = cnode.father.objectName;
			//print (fatherName);
			gameObject.transform.SetParent (GameObject.Find(fatherName).transform);
		}
		gameObject.transform.localPosition = new Vector3 (cnode.offset[0],cnode.offset[1],cnode.offset[2]);

		float rotx = Mathf.Clamp(cnode.pose[0]*Mathf.Rad2Deg, cnode.rotxlimit[0]*Mathf.Rad2Deg, cnode.rotxlimit[1]*Mathf.Rad2Deg);
		float roty = Mathf.Clamp(cnode.pose[1]*Mathf.Rad2Deg, cnode.rotylimit[0]*Mathf.Rad2Deg, cnode.rotylimit[1]*Mathf.Rad2Deg);
		float rotz = Mathf.Clamp(cnode.pose[2]*Mathf.Rad2Deg, cnode.rotzlimit[0]*Mathf.Rad2Deg, cnode.rotzlimit[1]*Mathf.Rad2Deg);

		gameObject.transform.localRotation = Quaternion.AngleAxis(rotz, Vector3.forward)*Quaternion.AngleAxis(roty, Vector3.up)*Quaternion.AngleAxis(rotx, Vector3.right);
		MeshFilter mf = gameObject.AddComponent<MeshFilter>();
		MeshRenderer mr = gameObject.AddComponent<MeshRenderer> ();

		Mesh m = new Mesh ();
		m.vertices = vertices;
		m.normals = normales;
		m.uv = uvs;
		m.triangles = triangles;

		mf.mesh = m;
		mr.material = mat;
	}
}
