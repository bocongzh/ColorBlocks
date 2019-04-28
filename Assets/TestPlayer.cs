using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class TestPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 0.1f;
    public PhotonView photonView;
    public GameObject body;
    public GameObject tailObject;

    private static Vector2 dir = Vector2.right;
    int id = 0;

    bool inDomain = false;
    private Color color;
    private static HashSet<String> colorSet = new HashSet<string>();
    private static Hashtable domain = new Hashtable();
    private static Hashtable tail = new Hashtable();
    private static int countColor = 0;
    private static GameObject curObj;

    float minX;
    float minY;
    float maxX;
    float maxY;

    float startX;
    float startY;

    Vector2 startPoint;
    Vector2 endPoint;


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            id = transform.name.ToCharArray()[0] - '0';

            color = Color.red;
            //startX = transform.position.x;
            minX = transform.position.x;
            maxX = transform.position.x;

            //startY = transform.position.y;
            minY = transform.position.y;
            maxY = transform.position.y;

            createDomain(); //create start block

            if (id == 2 || id == 4)
            {
                dir = Vector2.left;
                float x = transform.position.x - 2;
                float y = transform.position.y;
                transform.position = new Vector2(x, y);
            }

            InvokeRepeating("Move", speed, speed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow) && dir != Vector2.left)
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.LeftArrow) && dir != Vector2.right)
            dir = Vector2.left;
        else if (Input.GetKey(KeyCode.UpArrow) && dir != Vector2.down)
            dir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow) && dir != Vector2.up)
            dir = Vector2.down;
    }

    public static int getCount()
    {
        return countColor;
    }



    public void BlueClick()
    {
        colorSet.Add("blue");
        countColor++;
        //Debug.Log(countColor);
        //Debug.Log("click");
    }
    public void GreenClick()
    {
        colorSet.Add("green");
        countColor++;
        //Debug.Log(countColor);
        //Debug.Log("click");
    }

    private void ChangeColor(Color color)
    {
        foreach (Vector2 k in domain.Keys)
        {
            ((GameObject)domain[k]).GetComponent<Renderer>().material.color = color;

        }
        foreach (Vector2 k in tail.Keys)
        {
            //Debug.Log("tail");
            ((GameObject)tail[k]).GetComponent<Renderer>().material.color = color;
        }

    }

    void Move()
    {
        Vector2 vector = transform.position;
        transform.Translate(dir);
        bool isContain = domain.ContainsKey(vector);

        if (countColor == 2)
        {
            if (colorSet.Contains("red") && colorSet.Contains("green"))
            {
                color = Color.yellow;
            }
            else if (colorSet.Contains("red") && colorSet.Contains("blue"))
            {
                color = new Color(128, 0, 128);
            }
            else if (colorSet.Contains("green") && colorSet.Contains("blue"))
            {
                color = Color.cyan;
            }
            else if (colorSet.Contains("red"))
            {
                color = Color.red;
            }
            else if (colorSet.Contains("green"))
            {
                color = Color.green;
            }
            else if (colorSet.Contains("blue"))
            {
                color = Color.blue;
            }

            ChangeColor(color);
            countColor = 0;
            colorSet.Clear();

        }

        if (!domain.ContainsKey(vector) && !tail.ContainsKey(vector))
        {
            if (inDomain)
            {
                startPoint = vector;
            }
            inDomain = false;
            GameObject trace = PhotonNetwork.Instantiate(tailObject.name, vector, Quaternion.identity, 0);
            curObj = trace;
            //trace.GetComponent<Renderer>().material.color = color;
            tail.Add(vector, trace);

        }


        //if (!inDomain && isContain)
        //{
        //    fillTail();
        //    inDomain = true;
        //}
        //if (!isContain)
        //{
        //    GameObject trace = PhotonNetwork.Instantiate(body.name, vector, Quaternion.identity, 0);
        //    // trace.GetComponent<Renderer>().material.color = color;
        //    tail.Add(vector, trace);
        //    inDomain = false;
        //}

    }

    public void contolByButtonUp()
    {
        if (dir != Vector2.up && dir != Vector2.down)
        {
            dir = Vector2.up;
        }
    }

    public void contolByButtonDown()
    {
        if (dir != Vector2.up && dir != Vector2.down)
        {
            dir = Vector2.down;
        }
    }

    public void contolByButtonLeft()
    {
        if (dir != Vector2.left && dir != Vector2.right)
        {
            dir = Vector2.left;
        }
    }

    public void contolByButtonRight()
    {
        if (dir != Vector2.left && dir != Vector2.right)
        {
            dir = Vector2.right;
        }
    }

    private void fillTail()
    {
        ICollection tailKey = tail.Keys;
        Hashtable visited = new Hashtable();
        foreach (Vector2 point in tailKey)
        {
            if (visited.ContainsKey(point))
            {
                continue;
            }
            visited.Add(point, true);
            floodFill(visited, new Vector2(point.x + 1, point.y));
            floodFill(visited, new Vector2(point.x - 1, point.y));
            floodFill(visited, new Vector2(point.x, point.y + 1));
            floodFill(visited, new Vector2(point.x, point.y - 1));
        }
    }

    private void floodFill(Hashtable visited, Vector2 point)
    {
        if (IsOutOfBounds(point) || visited.ContainsKey(point) || tail.ContainsKey(point) || domain.ContainsKey(point))
        {
            return;
        }
        Stack<Vector2> filledStack = new Stack<Vector2>();
        Queue<Vector2> coords = new Queue<Vector2>();
        bool surrounded = true;

        coords.Enqueue(point);

        while (coords.Count > 0)
        {
            Vector2 coord = coords.Dequeue();
            if (IsOutOfBounds(coord))
            {
                surrounded = false;
                continue;
            }
            if (visited.ContainsKey(coord) || tail.ContainsKey(coord) || domain.ContainsKey(coord))
            {
                continue;
            }
            visited.Add(coord, true);
            if (surrounded)
            {
                filledStack.Push(coord);
            }
            coords.Enqueue(new Vector2(coord.x + 1, coord.y));
            coords.Enqueue(new Vector2(coord.x - 1, coord.y));
            coords.Enqueue(new Vector2(coord.x, coord.y + 1));
            coords.Enqueue(new Vector2(coord.x, coord.y - 1));
        }
        if (surrounded)
        {
            while (filledStack.Count > 0)
            {
                Vector2 coordPoint = filledStack.Pop();
                //GameObject body = Instantiate(tailPrefab, coordPoint, Quaternion.identity);
                GameObject d = PhotonNetwork.Instantiate(body.name, coordPoint, Quaternion.identity, 0);
                // d.GetComponent<Renderer>().material.color = color;
                domain.Add(coordPoint, d);
            }
        }
        if (!surrounded)
        {
            return;
        }
    }




    private bool IsOutOfBounds(Vector2 point)
    {
        return (point.x < -15 || point.x >= 25 || point.y < -11 || point.y >= 11);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        float x = collision.transform.position.x;
        float y = collision.transform.position.y;
        if (x == startPoint.x && y == startPoint.y)
        {
            //Debug.Log("Start Point");
        }
        else
        {
            Vector2 position = collision.transform.position;
            if (domain.ContainsKey(position))
            {
                //collide domain
                //Debug.Log("collide domain");
                if (!inDomain)
                {
                    //first collide domain
                    endPoint = (Vector2)collision.transform.position;
                    inDomain = true;
                    //Debug.Log("first collide domain");

                    fill();
   
                }
                else
                {
                    //head is in domain
                }
            }
            else if (tail.ContainsKey(position) && (GameObject)tail[position] != curObj || IsOutOfBounds(position))
            {
                //collide tail
                //Debug.Log("collide tail");
                foreach (Vector2 k in domain.Keys)
                {
                    PhotonNetwork.Destroy((GameObject)domain[k]);
                    //domain.Remove(k);
                }
                domain.Clear();
                //key = tail.Keys;
                foreach (Vector2 k in tail.Keys)
                {
                    PhotonNetwork.Destroy((GameObject)tail[k]);
                }
                tail.Clear();
                PhotonNetwork.Destroy(photonView);
            }
            else if (collision.name.Contains("border"))
            {
                foreach (Vector2 k in domain.Keys)
                {
                    PhotonNetwork.Destroy((GameObject)domain[k]);
                    //domain.Remove(k);
                }
                domain.Clear();
                //key = tail.Keys;
                foreach (Vector2 k in tail.Keys)
                {
                    PhotonNetwork.Destroy((GameObject)tail[k]);
                }
                tail.Clear();
                PhotonNetwork.Destroy(photonView);
            }
            else
            {
                //collide border
            }
        }
    }

    private void fill()
    {
        //scanLineFill();
        fillTail();
        minX = float.MaxValue;
        maxX = float.MinValue;

        minY = float.MaxValue;
        maxY = float.MinValue;
        ICollection key = tail.Keys;
        foreach (Vector2 k in key)
        {
            if (!domain.ContainsKey(k))
            {
                domain.Add(k, tail[k]);
                PhotonNetwork.Destroy((GameObject)tail[k]);
                GameObject d = PhotonNetwork.Instantiate(body.name, k, Quaternion.identity, 0);
            }
        }
        tail.Clear();
        //pairPoint.Clear();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            transform.position = (Vector2)stream.ReceiveNext();
        }
    }

    void createDomain()
    {
        Vector2 vector = transform.position;
        int width = 3;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector2 v = new Vector2(vector.x - i, vector.y + 1 - j);
                GameObject d = PhotonNetwork.Instantiate(body.name, v, Quaternion.identity, 0);
                // d.GetComponent<Renderer>().material.color = color;
                if (!domain.ContainsKey(v))
                {
                    domain.Add(v, d);
                }

                //Debug.Log(v);
            }
        }
    }


}
