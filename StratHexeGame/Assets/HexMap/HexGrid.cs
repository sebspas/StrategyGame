using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

    [SerializeField]
    int Width = 6;

    [SerializeField]
    int Height = 6;

    [SerializeField]
    HexCell CellPrefab;

    [SerializeField]
    Text cellLabelPrefab;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    private HexCell[] cells;

    private Canvas gridCanvas;

    private HexMesh hexMesh;

    public Texture2D noiseSource;

    void Awake()
    {
        HexMetrics.noiseSource = noiseSource;
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[Height * Width];

        for (int z = 0, i = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    void OnEnable()
    {
        HexMetrics.noiseSource = noiseSource;
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.INNERRADIUS * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.INNERRADIUS * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(CellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        // Connect the neigboors
        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - Width]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - Width - 1]);
                }
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - Width]);
                if (x < Width - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - Width + 1]);
                }
            }
        }

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
        cell.uiRect = label.rectTransform;
        cell.Elevation = 0;
    }

    public HexCell GetCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * Width + coordinates.Z / 2;
        return cells[index];
    }

    public void Refresh()
    {
        hexMesh.Triangulate(cells);
    }
}
