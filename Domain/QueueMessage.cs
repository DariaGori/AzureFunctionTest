using System;

namespace Domain
{
    [Serializable]
    public class QueueMessage
    {
        public int OrderNumber { get; set; }
        public string OrderDescription { get; set; }
    }
}
