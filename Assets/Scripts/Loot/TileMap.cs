using UnityEngine;

public class TileMap : MonoBehaviour
{

    [SerializeField] HeartObject healthPrefab;
    [SerializeField] ExpObject expPrefab;
    [SerializeField] TileMap[] availableTilemaps;
    [SerializeField] Player player;

    private Bounds tileBounds;
    private BoxCollider2D boxCollider;
    private float halfOfTileX;
    private float halfOfTileY;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        tileBounds = boxCollider.bounds;
        halfOfTileX = tileBounds.extents.x;
        halfOfTileY = tileBounds.extents.y;
        bool shouldSpawnHealthObject = Random.value > 0.5f;
        int expCount = Random.Range(1, 5);
        if (shouldSpawnHealthObject)
        {
            SpawnHealth();
        }
        SpawnExp(expCount);
    }

    private void Update()
    {
        DestroyFarTiles();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpawnMap();

        }
    }

    private void SpawnMap()
    {
        Vector3[] tilePositionCandidates = new Vector3[] {
        new Vector3(tileBounds.min.x - halfOfTileX, tileBounds.center.y, 0), //left
        new Vector3(tileBounds.max.x + halfOfTileX, tileBounds.center.y, 0), //right
        new Vector3(tileBounds.center.x, tileBounds.max.y + halfOfTileY, 0), //top
        new Vector3(tileBounds.center.x, tileBounds.min.y - halfOfTileY, 0), //bottom
        new Vector3(tileBounds.min.x - halfOfTileX, tileBounds.max.y + halfOfTileY, 0), //top left
        new Vector3(tileBounds.max.x + halfOfTileX, tileBounds.max.y + halfOfTileY, 0), //top right
        new Vector3(tileBounds.min.x - halfOfTileX, tileBounds.min.y - halfOfTileY, 0), //bottom left
        new Vector3(tileBounds.max.x + halfOfTileX, tileBounds.min.y - halfOfTileY, 0), //bottom right
        };

        foreach (Vector3 position in tilePositionCandidates)
        {
            if (!IsTileCreatedInPosition(position))
            {
                SpawnTileMap(position);

            }
        }
    }

    private bool IsTileCreatedInPosition(Vector3 place)
    {
        return Physics2D.OverlapBox(place, boxCollider.bounds.size * 0, 5, LayerMask.GetMask("Background"));
    }

    private void SpawnTileMap(Vector3 position)
    {
        TileMap tileMap = Instantiate(availableTilemaps[0], position, Quaternion.identity);
        tileMap.name = "TileMap";

    }

    private void DestroyFarTiles()
    {
        if ((transform.position - player.transform.position).magnitude > tileBounds.size.magnitude * 2)
        {
            Destroy(this.gameObject);
        }
    }

    private void SpawnExp(int expCount)
    {
        for (int i = 0; i < expCount; i++)
        {

            CreateCollectableInsideBorders(expPrefab);
        }
    }

    private void SpawnHealth()
    {
        CreateCollectableInsideBorders(healthPrefab);
    }

    private void CreateCollectableInsideBorders(Collectable prefab)
    {
        var position = CreateRandomPosition();
        if (!Physics2D.OverlapCircle(position, 0.2f, LayerMask.GetMask("Actor")))
        {
            Instantiate(prefab, position, Quaternion.identity, transform);
        }

    }

    private Vector3 CreateRandomPosition()
    {
        return new Vector3(
                        x: Random.Range(tileBounds.min.x, tileBounds.max.x),
                        y: Random.Range(tileBounds.min.y, tileBounds.max.y),
                        z: 0
                        );
    }
}
