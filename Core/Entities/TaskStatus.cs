using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public enum TaskStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pending,
        [EnumMember(Value = "En Progreso")]
        InProgress,
        [EnumMember(Value = "Completada")]
        Completed
    }
}
