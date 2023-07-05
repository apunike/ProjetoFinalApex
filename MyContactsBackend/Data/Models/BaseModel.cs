using System;

namespace Data.Models
{
     public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAdt { get; set; }
        public DateTime UpdatedAdt { get; set; }
    }
}
