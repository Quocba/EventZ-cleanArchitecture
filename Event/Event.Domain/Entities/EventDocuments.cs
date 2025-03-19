using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    [Table("event_documents")]
    public class EventDocuments : BaseEntity<Guid>
    {
        [Column("title")]
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }


        [Column("content")]
        [Required]
        [MaxLength]
        public string Content { get; set; }

        [Column("link_document")]
        [Required]
        [MaxLength]
        public string LinkDocument {  get; set; }

        [Column("documents_type")]
        [Required]
        public EventDocumentsTypeEnum DocumentsType { get; set; }

        [Column("event_id")]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        public Events Events { get; set; }
        
    }
}
