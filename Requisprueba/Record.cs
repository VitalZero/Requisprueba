using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requisprueba
{
    public class Record
    {
        private int numRequi;
        private string fechaElaboracion;
        private string fechaSolicitud;
        private string fechaAutorizacion;
        private double monto;
        private string notas;

        public Record(int requi, string fechaElab, string fechaSol, string fechaAut, double mont, string not)
        {
            numRequi = requi;
            fechaElaboracion = fechaElab;
            fechaSolicitud = fechaSol;
            fechaAutorizacion = fechaAut;
            monto = mont;
            notas = not;
        }

        public int NumRequi { get; set; }
        public string FechaElaboracion { get; set; }
        public string FechaSolcitud { get; set; }
        public string FechaAutorizacion { get; set; }
        public double Monto { get; set; }
        public string Notas { get; set; }
    }
}
