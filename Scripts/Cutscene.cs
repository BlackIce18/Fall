using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    public void StartCutScene()
    {
        _director.Play();
    }
}
