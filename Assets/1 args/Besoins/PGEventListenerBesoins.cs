using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PGSauce.Core.PGEvents;

namespace PGSauce.Games.DropDaBomb
{
	public class PGEventListenerBesoins : PGEventListener1Args<Dictionary<string, Besoins.Besoin>, PGEventBesoins, UnityEventBesoins>
	{
	}
}
