using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGA.AI.Common;

namespace DBGA.AI.Weapon
{
    public class TEST : MonoBehaviour
    {
        public WeaponBase currentW;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                currentW.Shoot();
            }
        
        }
    }
}
