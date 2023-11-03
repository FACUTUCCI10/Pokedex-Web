using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
			 public class ElementoNegocio
    {
			 public List<elementos> listar()
        {
			List<elementos>lista=new List<elementos> ();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.SetearConsulta("select id,descripcion from elementos");
				datos.EjecutarLectura();

				while(datos.lector.Read()) {
					elementos aux = new elementos();
					aux.id = (int)datos.lector["id"];
					aux.Descripcion = (string)datos.lector["descripcion"];

					lista.Add(aux);
				}
				return lista;
		}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.CerrarConexion();
			}
        }
    }
}
