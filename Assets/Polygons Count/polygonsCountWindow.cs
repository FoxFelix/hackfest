using UnityEngine;
using UnityEditor;
using System.Collections;

public class polygonsCountWindow : EditorWindow {
	int totalPolygons;
	int totalVertex;
	int currentLine = 0;
	int ptr = 0;
	int lastObjectId;
	ArrayList allPolygons;
	ArrayList allVertex;
	ArrayList allNames;
	ArrayList xprocessed;

	public Vector2 scrollPosition = Vector2.zero;
	
	[MenuItem ("Window/Polygons Count")]

	public static void init ()
	{
		polygonsCountWindow window = (polygonsCountWindow)EditorWindow.GetWindow(typeof(polygonsCountWindow));
		window.Show();
	}

	void OnInspectorUpdate()
	{
		Repaint();
	}


	void OnGUI ()
	{
		if (!Application.isPlaying)
		{
			if (Selection.activeGameObject) 
			{
				int width = Screen.width;
				int height = Screen.height;
				int leftColWidth = width - 180;

				GUIStyle styleSmallNumbers = new GUIStyle (GUI.skin.label);
				styleSmallNumbers.alignment = TextAnchor.MiddleRight;

				GUIStyle styleNumbers = new GUIStyle (GUI.skin.label);
				styleNumbers.fontSize = 20;
				styleNumbers.alignment = TextAnchor.MiddleRight;

				GUIStyle styleTitle = new GUIStyle(GUI.skin.label);
				styleTitle.fontSize = 14;

				int newObjectId = 0;
				foreach(GameObject GO in Selection.gameObjects) {
					newObjectId += GO.GetInstanceID();
				}
						
				if (lastObjectId != newObjectId)
				{
					ptr++;
					lastObjectId = newObjectId;

					allPolygons = new ArrayList ();
					allVertex = new ArrayList ();
					allNames = new ArrayList ();
					xprocessed = new ArrayList ();
					
					currentLine = 0;
					totalPolygons = 0;
					totalVertex = 0;
						
					foreach(GameObject GO in Selection.gameObjects) {
						scanAChild (GO.transform);
					}
				}
				if (height > 150 && currentLine > 1) // Hiden if the area is too small 
				{ 
					GUI.Label (new Rect (10, 70, 60, 15), "Details:");

					if(currentLine > 1000) {
						GUI.Label (new Rect (10, 90, width - 20, 100), currentLine+" elements are selected. Select less than 1000 elements to view the details.");
					} else {
						
						scrollPosition = GUI.BeginScrollView (new Rect (0, 90, width - 5, height - 115), scrollPosition, new Rect (0, 0, width - 20, currentLine * 14 + 30));

						if(xprocessed != null) {
							for (int subv = 0; subv < currentLine; subv++) {
								if(xprocessed[subv] != null) xprocessed[subv] = (int)allPolygons[subv];
							}
						

							for (int v = 0; v < currentLine; v++) 
							{
								int maxFoundIndex = 0;
								for (int subv = 0; subv < currentLine; subv++) 
								{
									if((int)xprocessed[subv] > (int)xprocessed[maxFoundIndex])
									{
										maxFoundIndex = subv;
									}
								}
								int thisPolycount = (int)allPolygons[maxFoundIndex];
								xprocessed[maxFoundIndex] = 0;

								GUI.Label (new Rect (20, v * 14, leftColWidth - 10, 15), (string)allNames [maxFoundIndex]);

								if (thisPolycount >= 10000) 
								{
									GUI.Label (new Rect (leftColWidth, v * 14, 70, 15), (thisPolycount / 1000) + "K", styleSmallNumbers);
								} else {
									GUI.Label (new Rect (leftColWidth, v * 14, 70, 15), "" + thisPolycount, styleSmallNumbers);
								}

								if ((int)allVertex [maxFoundIndex] >= 10000) 
								{
									GUI.Label (new Rect (leftColWidth + 80, v * 14, 70, 15), ((int)allVertex [maxFoundIndex] / 1000) + "K", styleSmallNumbers);
								} else {
									GUI.Label (new Rect (leftColWidth + 80, v * 14, 70, 15), "" + (int)allVertex [maxFoundIndex], styleSmallNumbers);
								}
							}
						}
						GUI.EndScrollView ();
					}
				}

				string title;
				int scount = Selection.gameObjects.Length;
				if(scount == 1) 
				{
					title = Selection.activeGameObject.name;
				} else {
					title = scount + " objects selected";
				}
				GUI.Label (new Rect (10, 10, width - 20, 20), title, styleTitle);
				GUI.Label (new Rect (leftColWidth, 35, 70, 18), "Polygons", styleSmallNumbers);
				if (totalPolygons >= 1000000)
				{
					int poly = (int)totalPolygons;
					GUI.Label (new Rect (leftColWidth, 50, 70, 30), ((float)poly / 1000000).ToString ("F1") + "M", styleNumbers);
				} else {
					if (totalPolygons >= 10000) 
					{
						int poly = (int)totalPolygons;
						GUI.Label (new Rect (leftColWidth, 50, 70, 30), (poly / 1000) + "K", styleNumbers);
					} else {
						GUI.Label (new Rect (leftColWidth, 50, 70, 30), "" + totalPolygons, styleNumbers);
					}
				}
				GUI.Label (new Rect (leftColWidth + 80, 35, 70, 18), "Vertex", styleSmallNumbers);
				if (totalVertex >= 1000000)
				{
					int vertx = (int)totalVertex;
					GUI.Label (new Rect (leftColWidth + 80, 50, 70, 30), ((float)vertx / 1000000).ToString ("F1") + "M", styleNumbers);
				} else {
					if (totalVertex >= 10000)
					{
						int vertx = (int)totalVertex;
						GUI.Label (new Rect (leftColWidth + 80, 50, 70, 30), (vertx / 1000) + "K", styleNumbers);
					} else {
						GUI.Label (new Rect (leftColWidth + 80, 50, 70, 30), "" + totalVertex, styleNumbers);
					}
				}


			} else {
				lastObjectId = 0;
				GUILayout.Label ("No selection", EditorStyles.boldLabel);
			}

		} else {
			lastObjectId = 0;
			GUILayout.Label ("This tool is enabled in editor mode only.", EditorStyles.boldLabel);
		}
	}
	
	void scanAChild(Transform parentChild) {
		int polygonsCount = 0;
		int vertexCount = 0;

		MeshFilter objectMeshFilter;
		Mesh objectMesh;

		objectMeshFilter = parentChild.GetComponent<MeshFilter> ();
		if (objectMeshFilter != null)
		{
			objectMesh = objectMeshFilter.sharedMesh;
			polygonsCount = objectMesh.triangles.Length / 3;
			vertexCount = objectMesh.vertexCount;
			
			totalPolygons = totalPolygons + polygonsCount;
			totalVertex = totalVertex + vertexCount;

			allPolygons.Add(polygonsCount);
			xprocessed.Add(polygonsCount);
			allVertex.Add(vertexCount);
			allNames.Add(parentChild.name);
			currentLine++;
		}

		foreach (Transform child in parentChild)
		{
			objectMeshFilter = child.GetComponent<MeshFilter> ();
			if (objectMeshFilter != null)
			{
				objectMesh = objectMeshFilter.sharedMesh;
				if(objectMesh != null)
				{
					if(objectMesh.triangles != null)
					{
						polygonsCount = objectMesh.triangles.Length / 3;
						vertexCount = objectMesh.vertexCount;
						
						totalPolygons = totalPolygons + polygonsCount;
						totalVertex = totalVertex + vertexCount;

						allPolygons.Add(polygonsCount);
						xprocessed.Add(polygonsCount);
						allVertex.Add(vertexCount);
						allNames.Add(child.name);
						currentLine++;
					}
				}
			}

			if(child.transform.childCount > 0) {
				scanAChild(child.transform);
			}
		}
	}
}
