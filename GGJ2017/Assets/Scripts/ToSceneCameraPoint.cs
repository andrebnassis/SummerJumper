#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ToSceneCameraPoint : EditorWindow {

	private Vector3 cameraScenePosition;
	private Vector3 cameraSceneRotation;
	[MenuItem("Window/PaulaoPlugins/ToSceneCameraPoint")]
	static void Init()
	{
		ToSceneCameraPoint window = GetWindow<ToSceneCameraPoint>();
		window.Show();
	}

	void OnGUI()
	{
		if( !Application.isPlaying )
		{
			EditorGUILayout.LabelField("Scene Camera Transform");
			Vector3 positionCamera = SceneView.lastActiveSceneView == null ? Vector3.zero : SceneView.lastActiveSceneView.camera.transform.position;
			Vector3 rotationCamera = SceneView.lastActiveSceneView == null ? Vector3.zero : SceneView.lastActiveSceneView.camera.transform.rotation.eulerAngles;

			cameraScenePosition = EditorGUILayout.Vector3Field("Position: ", positionCamera);
			cameraSceneRotation = EditorGUILayout.Vector3Field(" Rotation: ", rotationCamera);
			EditorGUILayout.Separator();
			string nameObject = Selection.activeGameObject == null ? "Select one object" : Selection.activeGameObject.name;
			
			EditorGUILayout.LabelField("Object Selected: "+nameObject);

			if( Selection.activeGameObject != null )
			{
				Rect rectButton = EditorGUILayout.BeginHorizontal();

					rectButton.height = 20;
					if( GUI.Button(rectButton,"Move THIS to CameraScene Point!" ) )
					{
						Selection.activeGameObject.transform.position = cameraScenePosition;
						Selection.activeGameObject.transform.rotation = Quaternion.Euler(cameraSceneRotation);
					}
				EditorGUILayout.EndHorizontal();
			}
		}
	}

	void OnInspectorUpdate()
	{

		this.Repaint();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
#endif