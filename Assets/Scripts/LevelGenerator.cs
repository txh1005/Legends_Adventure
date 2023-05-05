using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor,shopColor;
    public int distanceToEnd;
    public bool includeShop;
    public int minDistanceToShop, maxDistanceToShop;

    public Transform generatorPoint;

    public enum Direction { up,right,down,left};
    public Direction selectedDirection;

    public float xOffset = 18f,yOffset=10f;

    public LayerMask whatIsRoom;

    private GameObject endRoom,shopRoom;

    private List<GameObject> layoutRoomObj = new List<GameObject>();

    public RoomPrefabs rooms;

    private List<GameObject> generatedOutlines = new List<GameObject>();

    public RoomCenter centerStart, centerEnd,centerShop;
    public RoomCenter[] potentialCenters;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerator();
        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            layoutRoomObj.Add(newRoom);
            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObj.RemoveAt(layoutRoomObj.Count - 1);
                endRoom = newRoom;
            }
            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerator();
            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerator();
            }
        }
        if (includeShop)
        {
            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop+1);
            shopRoom = layoutRoomObj[shopSelector];
            layoutRoomObj.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;

        }
        //tao room outline
        CreateRoomOutline(Vector3.zero);
        foreach (GameObject room in layoutRoomObj)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);
        if (includeShop)
        {
            CreateRoomOutline(shopRoom.transform.position);
        }
        foreach (GameObject outline in generatedOutlines)
        {
            bool genarateCenter = true;
            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, outline.transform.rotation).theDoor = outline.GetComponent<Door>();
                genarateCenter = false;
            }
            if (outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, outline.transform.rotation).theDoor = outline.GetComponent<Door>();
                genarateCenter = false;
            }
            if (includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(centerShop, outline.transform.position, outline.transform.rotation).theDoor = outline.GetComponent<Door>();
                    genarateCenter = false;
                }
            }
            if (genarateCenter)
            {
                int centerSelect = Random.Range(0, potentialCenters.Length);
                Instantiate(potentialCenters[centerSelect], outline.transform.position, outline.transform.rotation).theDoor = outline.GetComponent<Door>();
            }
        }
    }

        // Update is called once per frame
        void Update()
        {

        }
        public void MoveGenerator()
        {
            switch (selectedDirection)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0f, yOffset, 0f);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                    break;
                default:
                    break;
            }
        }
        public void CreateRoomOutline(Vector3 roomPosition)
        {
            bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
            bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
            bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);
            bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);

            int directionCount = 0;
            if (roomAbove)
            {
                directionCount++;
            }
            if (roomBelow)
            {
                directionCount++;
            }
            if (roomLeft)
            {
                directionCount++;
            }
            if (roomRight)
            {
                directionCount++;
            }
            switch (directionCount)
            {
                case 0:
                    Debug.LogError("Found no room exists!");
                    break;
                case 1:
                    if (roomAbove)
                    {
                        generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                    }
                    if (roomBelow)
                    {
                        generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                    }
                    if (roomLeft)
                    {
                        generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                    }
                    if (roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                    }
                    break;
                case 2:
                    if (roomAbove && roomBelow)
                    {
                        generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                    }
                    if (roomAbove && roomLeft)
                    {
                        generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                    }
                    if (roomAbove && roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                    }
                    if (roomRight && roomBelow)
                    {
                        generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                    }
                    if (roomRight && roomLeft)
                    {
                        generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                    }
                    if (roomLeft && roomBelow)
                    {
                        generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                    }
                    break;
                case 3:
                    if (roomAbove && roomBelow && roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
                    }
                    if (roomAbove && roomBelow && roomLeft)
                    {
                        generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                    }
                    if (roomAbove && roomLeft && roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                    }
                    if (roomLeft && roomRight && roomBelow)
                    {
                        generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                    }
                    break;
                case 4:
                    if (roomAbove && roomBelow && roomLeft && roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, transform.rotation));
                    }
                    break;
                default:
                    break;
            }
        }
}
[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;
}
