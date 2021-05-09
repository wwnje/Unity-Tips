using UnityEngine;
using UnityEditor;

namespace Orvnge
{
    [InitializeOnLoad]
    public class HierarchyLabel : MonoBehaviour
    {
        static HierarchyLabel()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (null == obj)
            {
                return;
            }

            if (obj != null && obj.name.StartsWith("---", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.grey);
                EditorGUI.DropShadowLabel(selectionRect, obj.name.Replace("-", ""));
            }

            if (obj != null && obj.name.StartsWith("***", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.green);
                EditorGUI.DropShadowLabel(selectionRect, obj.name.Replace("*", ""));
            }

            // 如果给定预制件实例有任何重载，则返回 true
            if (PrefabUtility.HasPrefabInstanceAnyOverrides(obj, false))
            {
                EditorGUI.DrawRect(selectionRect, Color.gray);
                EditorGUI.LabelField(selectionRect, obj.name + "*");
            }

            HighlightObj(obj, "LedgeChecker", selectionRect);
            HighlightObj(obj, "DamageDetector", selectionRect);
        }

        static void HighlightObj(GameObject obj, string objName, Rect selectionRect)
        {
            if (obj == null || !obj.name.Equals(objName)) return;

            EditorGUI.DrawRect(selectionRect, Color.magenta);
            EditorGUI.DropShadowLabel(selectionRect, obj.name);
        }
    }
}