using System;

namespace Mog.Api.Core.Models  
{  
    public class Feed
    {  
        public Guid Id { get; set; }
        public string CharacterName { get; set; }
        public int Update { get; set; }
        public int Addition { get; set; }
        public int Deletion { get; set; }
        public int StatUpdate { get; set; }
        public int StatAddition { get; set; }
        public int StateDeletion { get; set; }
        public int Like { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public DateTime TimeStamp { get; set; }
    }  
}