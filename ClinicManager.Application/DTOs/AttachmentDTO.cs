using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.DTOs
{
    public class AttachmentDTO
    {
        public AttachmentDTO(byte[] bytes, string name)
        {
            Bytes = bytes;
            Name = name;
        }

        public byte[] Bytes { get; set; }
        public string Name { get; set; }
    }
}
