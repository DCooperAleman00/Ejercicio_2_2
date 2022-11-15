using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Ejercicio_2_2.Models
{
    public class FirmaModelo
    {
        //atributos
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public Byte[] Firma { get; set; }
    }
}
