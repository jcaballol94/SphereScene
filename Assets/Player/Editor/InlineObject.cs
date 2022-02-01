using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace jCaballol94.SphereScene
{
    [CustomPropertyDrawer(typeof(InlineObjectAttribute))]
    public class InlineObject : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, label);

            if (property.isExpanded && property.objectReferenceValue)
            {
                var serializedObject = new SerializedObject(property.objectReferenceValue);
                var iterator = serializedObject.GetIterator();
                iterator.NextVisible(true);
                while (iterator.NextVisible(false))
                {
                    height += EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(iterator);
                }
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw the header
            var labelText = label.text;
            var labelTooltip = label.tooltip;

            position.height = EditorGUI.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property, new GUIContent(labelText, labelTooltip));
            property.isExpanded = InlineFoldout(position, property.isExpanded);

            if (property.isExpanded && property.objectReferenceValue)
            {
                EditorGUI.indentLevel++;
                var serializedObject = new SerializedObject(property.objectReferenceValue);
                var iterator = serializedObject.GetIterator();
                iterator.NextVisible(true);
                while (iterator.NextVisible(false))
                {
                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                    position.height = EditorGUI.GetPropertyHeight(iterator);
                    EditorGUI.PropertyField(position, iterator);
                }
                serializedObject.ApplyModifiedProperties();
                EditorGUI.indentLevel--;
            }
        }

        private static bool InlineFoldout (Rect position, bool value)
        {
            // Calculate the correct position
            position = EditorGUI.IndentedRect(position);

            // The rect where to draw
            var drawingPosition = position;
            drawingPosition.xMin -= EditorStyles.inspectorDefaultMargins.padding.left - EditorStyles.inspectorDefaultMargins.padding.right;
            drawingPosition.xMax += EditorStyles.inspectorDefaultMargins.padding.right;

            // The rect to check for clicks in
            var clickPos = position;
            clickPos.xMin = drawingPosition.xMin;
            clickPos.xMax = position.xMin;

            // Provess GUI events
            var evt = Event.current;
            var id = GUIUtility.GetControlID(FocusType.Passive);
            switch (evt.type)
            {
                case EventType.Repaint:
                    // Actually draw the content
                    EditorStyles.foldout.Draw(drawingPosition, GUIContent.none, id, value);
                    break;
                case EventType.MouseDown:
                    // Toggle on click
                    if (clickPos.Contains(evt.mousePosition))
                    {
                        value = !value;
                        evt.Use();
                    }
                    break;
            }

            return value;
        }
    }
}