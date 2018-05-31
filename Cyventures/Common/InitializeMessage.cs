﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class InitializeMessage<TPayload>:MessageBase
    {
        public static readonly string Id = Guid.NewGuid().ToString();
        public TPayload Payload { get; private set; }
        public InitializeMessage(TPayload payload):base(Id)
        {
            Payload = payload;
        }
    }
}
