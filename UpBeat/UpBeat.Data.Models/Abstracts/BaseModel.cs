using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UpBeat.Data.Models.Contracts;

namespace UpBeat.Data.Models.Abstracts
{
    public abstract class BaseModel : IAuditable, IDeletable
    {
        [Key]
        public int Id { get; set; }
        
        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
