using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requisprueba
{
    public class Record
    {
        //private int numRequi;
        //private string fechaElaboracion;
        //private string fechaSolicitud;
        //private string fechaAutorizacion;
        //private double monto;
        //private string notas;

        public Record(int requi, string fechaElab, string fechaSol, string fechaAut, double mont, string not)
        {
            NumRequi = requi;
            FechaElaboracion = fechaElab;
            FechaSolicitud = fechaSol;
            FechaAutorizacion = fechaAut;
            Monto = mont;
            Notas = not;
        }

        public int NumRequi { get; set; }
        public string FechaElaboracion { get; set; }
        public string FechaSolicitud { get; set; }
        public string FechaAutorizacion { get; set; }
        public double Monto { get; set; }
        public string Notas { get; set; }
    }
}
