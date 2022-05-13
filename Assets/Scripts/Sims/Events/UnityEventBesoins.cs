using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PGSauce.Core.PGEvents;

namespace PGSauce.Games.DropDaBomb
{
    [Serializable]
    public class UnityEventBesoins : UnityEvent<Dictionary<string, Besoins.Besoin>> { }
}
