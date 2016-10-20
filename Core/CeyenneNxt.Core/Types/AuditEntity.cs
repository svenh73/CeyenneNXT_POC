using System;

namespace CeyenneNxt.Core.Types
{
    public class AuditEntity
    {
        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public int? LastModifiedBy { get; set; }
    }
}
