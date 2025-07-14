using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance { get; private set; }
    private List<Portal> portals = new List<Portal>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Register(Portal portal)
    {
        if (!portals.Contains(portal))
        {
            portals.Add(portal);
        }
    }

    public void Unregister(Portal portal)
    {
        if (portals.Contains(portal))
        {
            portals.Remove(portal);
        }
    }

    public Portal GetPortalId(string portalId)
    {
        return portals.Find(p => p.thisPortalId == portalId);
    }
}
