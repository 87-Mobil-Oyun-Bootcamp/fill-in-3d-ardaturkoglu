using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillController : MonoBehaviour
{    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FillBlock"))
        {
            this.GetComponent<Renderer>().material.color = Color.yellow;
            Destroy(other.gameObject);
            this.GetComponent<Collider>().enabled =false;
            var blockController = other.GetComponent<BlockController>();

            if (blockController)
            {
                blockController.BlockState = BlockState.Collected;
            }
        }
    }
}
