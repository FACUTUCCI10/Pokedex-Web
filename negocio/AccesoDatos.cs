using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.AccessControl;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader Lector;
        
        public SqlDataReader lector
        {
            get { return Lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS;database=POKEDEX_DB;integrated security=true");
            comando = new SqlCommand();

        }
        public void SetearConsulta(string consul)
        {
            comando.CommandType =System.Data.CommandType.Text;
            comando.CommandText = consul;
        }
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                Lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }
        public void ejecutarAccion()
        {   comando.Connection= conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void setearParametro(string nombre,object valor)
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }
        public void setearProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }
        public void CerrarConexion()
        {
            if (lector!=null)
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
