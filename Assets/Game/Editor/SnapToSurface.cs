using UnityEditor;
using UnityEngine;

public class SnapToSurface : EditorWindow
{
    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("X"))
            {
                Drop(new Vector3(1, 0, 0));
            }
            if (GUILayout.Button("Y"))
            {
                Drop(new Vector3(0, 1, 0));
            }
            if (GUILayout.Button("Z"))
            {
                Drop(new Vector3(0, 0, 1));
            }
        }

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("-X"))
            {
                Drop(new Vector3(-1, 0, 0));
            }
            if (GUILayout.Button("-Y"))
            {
                Drop(new Vector3(0, -1, 0));
            }
            if (GUILayout.Button("-Z"))
            {
                Drop(new Vector3(0, 0, -1));
            }
        }
    }

    [MenuItem("Window/Snap to Surface")]
    private static void ShowWindow()
    {
        SnapToSurface window = EditorWindow.GetWindow<SnapToSurface>();
        window.titleContent.text = "Snap To Surface";
    }

    private static void Drop(Vector3 dir)
    {
        foreach (GameObject gameObj in Selection.gameObjects)
        {
            // If the object has a collider we can do a nice sweep test for accurate placement
            Collider collider = gameObj.GetComponent<Collider>();
            if (collider != null && collider is not CharacterController)
            {
                // Figure out if we need a temp rigid body and add it if needed
                Rigidbody rigidBody = gameObj.GetComponent<Rigidbody>();
                bool addedRigidBody = false;
                if (rigidBody == null)
                {
                    rigidBody = gameObj.AddComponent<Rigidbody>();
                    addedRigidBody = true;
                }

                // Sweep the rigid body downwards and, if we hit something, move the object the distance
                if (rigidBody.SweepTest(dir, out RaycastHit hit))
                {
                    gameObj.transform.position += dir * hit.distance;
                }

                // If we added a rigid body for this test, remove it now
                if (addedRigidBody)
                {
                    DestroyImmediate(rigidBody);
                }
            }
            // Without a collider, we do a simple raycast from the transform
            else
            {
                // Change the object to the "ignore raycast" layer so it doesn't get hit
                int savedLayer = gameObj.layer;
                gameObj.layer = 2;

                // Do the raycast and move the object down if it hit something
                if (Physics.Raycast(gameObj.transform.position, dir, out RaycastHit hit))
                {
                    gameObj.transform.position = hit.point;
                }

                // Restore layer for the object
                gameObj.layer = savedLayer;
            }
        }
    }
}
