using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Delete : MonoBehaviour
{
    [PunRPC]
    void delete()
    {
        Destroy(this.gameObject);        
    }
}
