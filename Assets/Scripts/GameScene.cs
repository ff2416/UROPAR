using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameScene : MonoBehaviourPunCallbacks
{
    bool ifAdd=false;
    bool ifDelete=false;
    float time=0;
    [SerializeField]
    Camera cam;
    string Addobject="Cube1";

    void Start()
    {
        if(PhotonNetwork.CurrentRoom==null)
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Add
        if(ifAdd)
        {
            time=time+Time.deltaTime;
            if(time>=0.3)
            {
                Touch touch =Input.GetTouch(0);
                Ray ray=cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    if(hit.collider.gameObject.tag=="Plane")
                    {
                        
                        Vector3 pos=new Vector3(hit.point.x,((hit.point.y)+0.05f),hit.point.z);
                        PhotonNetwork.Instantiate(Addobject,pos,hit.transform.rotation);
                        ifAdd=false;
                    }
                    else
                    {
                        Vector3 pos=new Vector3(hit.point.x,hit.point.y,hit.point.z);
                        PhotonNetwork.Instantiate(Addobject,pos,hit.transform.rotation);
                        ifAdd=false;
                    }
                }
                
            }
        }
        //Delete
        if(ifDelete)
        {
            time=time+Time.deltaTime;
            if(time>=0.3)
            {
                Touch touch =Input.GetTouch(0);
                Ray ray=cam.ScreenPointToRay(touch.position);
                RaycastHit hit1;
                if(Physics.Raycast(ray,out hit1))
                {
                    if(hit1.collider.gameObject.tag!="Plane")
                    {
                        PhotonView pv=PhotonView.Get(hit1.collider.gameObject);
                        pv.RPC("delete",RpcTarget.All);
                        ifDelete=false;
                    }
                    
                }
                
            }
        }
    }
    public void OnClickButtonAdd()
    {
        ifAdd=true;
        time=0;
    }
    public void OnClickButtonDelete()
    {
        ifDelete=true;
        time=0;
    }
    public void OnClickButtonCube1()
    {
        Addobject="Cube1";
    }
    public void OnClickButtonCube2()
    {
        Addobject="Cube2";
    }
    public void OnClickButtonCylinder1()
    {
        Addobject="Cylinder1";
    }
    public void OnClickButtonCylinder2()
    {
        Addobject="Cylinder2";
    }
    public void OnClickButtonSphere1()
    {
        Addobject="Sphere1";
    }
    public void OnClickButtonSphere2()
    {
        Addobject="Sphere2";
    }
    
}
