using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField]
    public EventReference Ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField]
    public EventReference Music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField]
    public EventReference PlayerFootsteps { get; private set; }

    [field: Header("Music Puzzle")]
    [field: SerializeField]
    public EventReference MusicNotes { get; private set; }

    [field: SerializeField]
    public EventReference Alarm { get; private set; }

    [field: Header("Dance Puzzle")]
    [field: SerializeField]
    public EventReference DanceMusic { get; private set; }
}
