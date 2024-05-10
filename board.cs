using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{
    private GameObject currentShip; // Reference to the currently instantiated ship
    private Renderer shipRenderer;//the ship's renderer component
    private Renderer shipRendererCopy;//a copy of the orriginal color :/
    GameObject ship;
    int boardSize = 5;
    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Ship");
        shipRenderer = ship.GetComponent<Renderer>();
        shipRendererCopy = shipRenderer;
        Quaternion rotation = Quaternion.Euler(0f,90f,0f);
        GameObject originalTile = GameObject.Find("Tile");
        for(int y= 0; y<boardSize; y++){
            for(int x=0; x<boardSize;x++){
                GameObject tile = Instantiate(originalTile, new Vector3((x - boardSize/2),0.3f,(y- boardSize/2)), rotation);
                tile.AddComponent<Tile>();
                Tile tileComponent = tile.GetComponent<Tile>();
                tileComponent.x = x;
                tileComponent.y = y;
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Material material = ship.GetComponent<Material>();
        if(Physics.Raycast(ray, out hit))
        {
            Tile hitTile = hit.collider.GetComponent<Tile>();
            if(hitTile!=null){
            Vector3 shipPosition = new Vector3(hitTile.x - (boardSize - 1) * 0.5f, 0.5f, hitTile.y - (boardSize - 1) * 0.5f);
            if (hitTile.hasShip){
                shipRenderer.material.color = Color.red;
            }
            else{
                shipRenderer = shipRendererCopy;
                if (Input.GetMouseButtonDown(0))
                {
                        Instantiate(ship, shipPosition, Quaternion.identity);
                        hitTile.hasShip = true;
                }
            }

            ship.transform.position = new Vector3(hitTile.x - (boardSize - 1) * 0.5f, 0.9f, hitTile.y - (boardSize - 1) * 0.5f);
            }
        }
    }
}
