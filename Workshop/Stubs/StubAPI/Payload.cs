using System;

namespace StubAPI
{
    public class Payload
    {
        public string MessageContent { get; set; }
        public string MessageName { get; set; }         
    }

    public class ApiMessage
    {
        public string TargetName { get; set; }
        public DateTime Timestamp { get; set; }
        public Payload Payload { get; set; }
    }
}