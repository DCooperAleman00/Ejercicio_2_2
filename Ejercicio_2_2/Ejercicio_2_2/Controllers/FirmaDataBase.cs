using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Ejercicio_2_2.Models;
using System.IO;

namespace Ejercicio_2_2.Controllers
{
    //CRUD
    public class FirmaDataBase
    {
        readonly SQLiteAsyncConnection BaseDatos;

        public FirmaDataBase(string pathdb)
        {
            BaseDatos = new SQLiteAsyncConnection(pathdb);
            BaseDatos.CreateTableAsync<FirmaModelo>().Wait();
        }

        public Task<List<FirmaModelo>> GetFirmasList()
        {
            return BaseDatos.Table<FirmaModelo>().ToListAsync();
        }

        public Task<int> CreateFirma(FirmaModelo firma)
        {
            if (firma.Id != 0)
            {
                return BaseDatos.UpdateAsync(firma);
            }
            else
            {
                return BaseDatos.InsertAsync(firma);
            }
        }


        //listar las firmas 
        public Task<FirmaModelo> GetFirmaID(int pcodigo)
        {
            return BaseDatos.Table<FirmaModelo>()
                .Where(i => i.Id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
