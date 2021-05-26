using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSnapping : MonoBehaviour
{
    [SerializeField] private Vector3 gridsize = new Vector3(2,1,2);

    private void OnDrawGizmos()
    {
        //if only want to have snapping while editing
        //if(!Application.isPlaying && this.transform.hasChanged){call snappingfunction}

        SnapToGrid();
    }
    private void SnapToGrid()
    {
        Vector3 Position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / this.gridsize.x) * this.gridsize.x,
            Mathf.RoundToInt(this.transform.position.y / this.gridsize.y) * this.gridsize.y,
            Mathf.RoundToInt(this.transform.position.z / this.gridsize.z) * this.gridsize.z
            );
        this.transform.position = Position;
    }




}
