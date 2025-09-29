using UnityEngine;

public class DrawOnMesh : MonoBehaviour
{
    public Camera cam;
    public int textureSize = 1024;
    public Color drawColor = Color.black;
    public int brushSize = 10;

    private Texture2D drawTexture;
    private Renderer rend;
    private Vector2? lastUV = null;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Create a new writable texture
        drawTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        ClearTexture();

        // Apply it to this mesh’s material
        rend.material = new Material(rend.material); // clone to avoid editing shared material
        rend.material.mainTexture = drawTexture;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // left=draw, right=erase
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Renderer hitRend = hit.collider.GetComponent<Renderer>();

                if (hitRend == rend) // only draw on THIS mesh
                {
                    Vector2 uv = hit.textureCoord; // always in [0,1]
                    int x = (int)(uv.x * textureSize);
                    int y = (int)(uv.y * textureSize);

                    Color c = Input.GetMouseButton(0) ? drawColor : Color.white;

                    if (lastUV.HasValue)
                    {
                        Vector2 prev = lastUV.Value * textureSize;
                        DrawLine((int)prev.x, (int)prev.y, x, y, c);
                    }
                    else
                    {
                        DrawCircle(x, y, c);
                    }

                    lastUV = uv;
                    drawTexture.Apply();
                }
            }
        }
        else
        {
            lastUV = null;
        }
    }

    void DrawCircle(int cx, int cy, Color c)
    {
        for (int x = -brushSize; x < brushSize; x++)
        {
            for (int y = -brushSize; y < brushSize; y++)
            {
                if (x * x + y * y <= brushSize * brushSize)
                {
                    int px = cx + x;
                    int py = cy + y;

                    if (px >= 0 && px < textureSize && py >= 0 && py < textureSize)
                    {
                        drawTexture.SetPixel(px, py, c);
                    }
                }
            }
        }
    }

    void DrawLine(int x0, int y0, int x1, int y1, Color c)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = (x0 < x1) ? 1 : -1;
        int sy = (y0 < y1) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            DrawCircle(x0, y0, c);

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }

    void ClearTexture()
    {
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                drawTexture.SetPixel(x, y, Color.white);
            }
        }
        drawTexture.Apply();
    }
}
