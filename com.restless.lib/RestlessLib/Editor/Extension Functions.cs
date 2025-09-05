using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ExtensionFunctions
{
    // LEGACY IMPORTED CODE FOR REFERENCE
    /*
    [MenuItem("CONTEXT/SpriteRenderer/Calculate Sort Order")]
    static void CalculateSortOrder(MenuCommand command)
    {
        SpriteRenderer renderer = (SpriteRenderer)command.context;
        renderer.sortingOrder = (int)(renderer.transform.position.y * -10);
    }

    [MenuItem("CONTEXT/SpriteRenderer/Add as static Renderer")]
    static void AddToStaticRenderers(MenuCommand command)
    {
        SpriteRenderer renderer = (SpriteRenderer)command.context;
        EntitySortOrderManager.EntitiesSOManager.StaticRenderers.Add(renderer);
    }

    [MenuItem("CONTEXT/SortingGroup/Add to static Groups")]
    static void AddToStaticGroups(MenuCommand command)
    {
        SortingGroup renderer = (SortingGroup)command.context;
        EntitySortOrderManager.EntitiesSOManager.StaticGroups.Add(renderer);
    }

    [MenuItem("CONTEXT/SortingGroup/Calculate Sort Oder")]
    static void CalculateSortOrderGroup(MenuCommand command)
    {
        SortingGroup renderer = (SortingGroup)command.context;
        renderer.sortingOrder = (int)(renderer.transform.position.y * -10);
    }

    [MenuItem("CONTEXT/Transform/Snap To Pixel Grid")]
    static void SnapToPixelGrid(MenuCommand command)
    {
        int PixelsPerUnit = 64;

        Transform transform = (Transform)command.context;
        Vector2 pos = transform.position;

        pos.x = Mathf.Round(pos.x * PixelsPerUnit) / PixelsPerUnit;
        pos.y = Mathf.Round(pos.y * PixelsPerUnit) / PixelsPerUnit;

        transform.position = pos;
    }
    [MenuItem("CONTEXT/Transform/Random Rotate Z")]
    static void RandomRotate(MenuCommand command)
    {
        float ZAngle = Random.Range(0, 360);

        Transform transform = (Transform)command.context;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, ZAngle);
    }
    */
}
