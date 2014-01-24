using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Worldbuilder
{
    [Serializable]
    class pion
    {
        public Vector2 worldpos;
        public string type;

        public pion pions(string t)
        {
            type = t;

            return this;
        }
    }
}
