using UnityEngine;

public class EyeWall : MonoBehaviour
{
    [SerializeField] private GameObject _lookAt;
    public GameObject target;

    private void Start()
    {
        if(target == null)
        {
            target = GameManager.instance.Character.gameObject;
            Debug.LogWarning("target (null) => target = Character");
        }
    }

    private void Update()
    {
        if(target != null) 
        {
            Look();
        }
    }

    public void Look()
    {
        _lookAt.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 4, target.transform.position.z);
    }
}
