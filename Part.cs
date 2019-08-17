using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Parse
{
    class Part
    {
        public string PartNumber { get; private set; }
        public string Description { get; private set; }
        public double EAN { get; private set; }
        public int FreeStock { get; private set; }
        public Part(string PartNumber, string Description, double EAN, int FreeStock)
        {
            this.PartNumber = PartNumber;
            this.Description = Description;
            this.EAN = EAN;
            this.FreeStock = FreeStock;
        }

        public override string ToString()
        {
            return $"Part Number: {PartNumber}, Description: {Description}, EAN: {EAN}, FreeStock: {FreeStock}";
        }
    }
}
