using System.Collections.Generic;
using UnityEngine;

public class HitColor : MonoBehaviour
{
    public Renderer[] renderers { get; private set; }
    public Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();
    public List<Color> hitColors { get; private set; }

    void Start()
    {
        SetRenderers();
    }

    public void ChangeColor(Renderer[] renderers, Color color)
    {
        for (int i = 0; i < renderers.Length; ++i)
        {
            if (renderers[i] is not TrailRenderer)
            {
                Material[] materials = renderers[i].materials;

                for (int j = 0; j < materials.Length; ++j)
                {
                    // materials[j].SetColor("_BaseColor", color);
                    if (materials[j].HasProperty("_BaseColor"))
                    {
                        materials[j].SetColor("_BaseColor", color);
                    }
                    else if (materials[j].HasProperty("_Color"))
                    {
                        materials[j].SetColor("_Color", color);
                    }
                }
            }
        }
    }

    public void ReChangeColor(Renderer[] renderers, Dictionary<Renderer, Color> dics)
    {
        foreach (var renderer in renderers)
        {
            if (!dics.ContainsKey(renderer))
            {
                // Debug.LogWarning($"[ReChangeColor] Renderer {renderer.name} was not found in original color dictionary.");
                continue;
            }

            Material[] materials = renderer.materials;
            for (int j = 0; j < materials.Length; ++j)
            {
                if (materials[j].HasProperty("_BaseColor"))
                {
                    materials[j].SetColor("_BaseColor", dics[renderer]);
                }
                else if (materials[j].HasProperty("_Color"))
                {
                    materials[j].SetColor("_Color", dics[renderer]);
                }
            }
        }
    }

    public void SetRenderers()
    {
        renderers = GetComponentsInChildren<Renderer>();
        List<Renderer> validRenderer = new List<Renderer>();

        foreach (var renderer in renderers)
        {
            if (renderer is TrailRenderer) continue;

            if (renderer.gameObject.CompareTag("Item")) continue;
            {
                if (renderer.material != null)
                {
                    var mat = renderer.material;

                    if (mat.HasProperty("_BaseColor"))
                    {
                        originalColors[renderer] = mat.GetColor("_BaseColor");
                    }
                    else if (mat.HasProperty("_Color"))
                    {
                        originalColors[renderer] = mat.GetColor("_Color");
                    }

                    validRenderer.Add(renderer);
                }
            }

            renderers = validRenderer.ToArray();
        }
    }
}
