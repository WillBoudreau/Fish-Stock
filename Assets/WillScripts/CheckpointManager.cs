using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Checkpoint Manager")]

    [Header("Checkpoint Variables")]
    public GameObject[] checkpoints; // Array of checkpoints
    public GameObject currentCheckpoint; // Current checkpoint
    public int currentCheckpointIndex = 0; // Current checkpoint index

}
