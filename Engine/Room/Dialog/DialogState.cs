using System;
using System.Collections.Generic;

namespace Engine
{
    public class DialogState
    {
        public string CurrentState { get; set; }
        public Dictionary<string, DialogNode> Nodes { get; set; }

        internal DialogNode GetNode(string state)
        {
            if (Nodes == null)
            {
                Nodes = new Dictionary<string, DialogNode>();
            }
            if (!Nodes.ContainsKey(state))
            {
                Nodes[state] = new DialogNode();
            }
            return Nodes[state];
        }
    }
}