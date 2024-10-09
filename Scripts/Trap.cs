using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    block,
    spider
}
public class Trap : MonoBehaviour
{
    [SerializeField] private TrapType type;
    public TrapType Type { get { return type; } }

}
