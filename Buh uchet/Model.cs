using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buh_uchet
{
    internal class Model
    {
        public string Name { get; set; }
        public string NoteType { get; set; }
        public int Moneys { get; set; }
        public bool PlusMoney { get; set; }
        public DateTime NoteDate;
        public Model(string Name, string NoteType, int Moneys, DateTime NoteDate )
        {
            this.Name = Name;
            this.NoteType = NoteType;
            if(Moneys < 0)
            {
                this.Moneys = Moneys*(-1);
                this.PlusMoney = false;
            }
            else
            {
                this.Moneys = Moneys;
                this.PlusMoney = true;
            }
            this.NoteDate = NoteDate;

        }
    }
}
