using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class AssemblyInstructions
    {
        public short IdAssemblyInstructions { get; set; }
        public short? Idreference { get; set; }
        public byte[] Instructions { get; set; }
        public string FileName { get; set; }
    }
}
