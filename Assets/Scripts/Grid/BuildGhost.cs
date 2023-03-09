using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
    private Transform visual;
    private PlacedObject placedObjectTypeSO;

    public bool isSoutenance;

    private void Start() {
        if (isSoutenance)
        {
            return;
        }
        
        RefreshVisual();

        GridSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        if (isSoutenance)
        {
            return;
        }
        
        RefreshVisual();
    }

    private void LateUpdate() {
        if (isSoutenance)
        {
            return;
        }
        
        Vector3 targetPosition = GridSystem.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 1f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, GridSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if (isSoutenance)
        {
            return;
        }
        if (visual is not null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        ObjectType selectedObject = GridSystem.Instance.GetSelected();

        if (selectedObject is not null) {
            visual = Instantiate(selectedObject.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 11);
        }
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        if (isSoutenance)
        {
            return;
        }
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }
}
