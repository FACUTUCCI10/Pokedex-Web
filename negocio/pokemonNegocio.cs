using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace negocio
{
          public class pokemonNegocio
    {
          public  List<pokemon> Listar(string id)
        {
            List<pokemon> lista = new List<pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS;database=POKEDEX_DB;integrated security=true";
                comando.CommandType=System.Data.CommandType.Text;
                comando.CommandText = "select Numero,Nombre,P.descripcion,urlImagen,E.descripcion tipo,d.Descripcion debilidad,p.IdDebilidad,p.IdTipo,p.id from POKEMONS P,ELEMENTOS E,ELEMENTOS d where E.Id=P.IdTipo and D.Id=P.IdDebilidad and P.activo=1";
                if (id != "")
                    comando.CommandText += " and p.id = " + id;
                comando.Connection = conexion;
                conexion.Open();
               
                lector = comando.ExecuteReader();
               
                while (lector.Read())

                {
                    pokemon aux = new pokemon();
                    aux.id = (int)lector["id"];
                    aux.numero = lector.GetInt32(0);
                    aux.nombre = (string)lector["nombre"];
                    aux.descripcion=(string)lector["Descripcion"];

                    if (!(lector["urlImagen"] is DBNull))
                    aux.urlImagen = (string)lector["urlImagen"];

                    aux.tipo = new elementos();
                    aux.tipo.id = (int)lector["idTipo"];
                    aux.tipo.Descripcion = (string)lector["tipo"];
                   
                    aux.debilidad = new elementos();
                    aux.debilidad.id = (int)lector["idDebilidad"];
                    aux.debilidad.Descripcion = (string)lector["debilidad"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;

                

            }
            catch (Exception ex)
            {

                throw ex;

            }
           
        }
        public List<pokemon> ListarConsp() 
        {
            List<pokemon> lista = new List<pokemon>();
            AccesoDatos dateanos = new AccesoDatos();
            try
            {
                //string consulta = "select Numero,Nombre,P.descripcion,urlImagen,E.descripcion tipo,d.Descripcion debilidad,p.IdDebilidad,p.IdTipo,p.id from POKEMONS P,ELEMENTOS E,ELEMENTOS d where E.Id=P.IdTipo and D.Id=P.IdDebilidad and P.activo=1";
                //dateanos.SetearConsulta(consulta);
                dateanos.setearProcedimiento("storedlistar");
                dateanos.EjecutarLectura();
                while (dateanos.lector.Read())

                {
                    pokemon aux = new pokemon();
                   
                    aux.id = (int)dateanos.lector["id"];
                    aux.numero = dateanos.lector.GetInt32(0);
                    aux.nombre = (string)dateanos.lector["nombre"];
                    aux.descripcion = (string)dateanos.lector["Descripcion"];

                    if (!(dateanos.lector["urlImagen"] is DBNull))
                        aux.urlImagen = (string)dateanos.lector["urlImagen"];

                    aux.tipo = new elementos();
                    aux.tipo.id = (int)dateanos.lector["idTipo"];
                    aux.tipo.Descripcion = (string)dateanos.lector["tipo"];

                    aux.debilidad = new elementos();
                    aux.debilidad.id = (int)dateanos.lector["idDebilidad"];
                    aux.debilidad.Descripcion = (string)dateanos.lector["debilidad"];

                    aux.Activo = bool.Parse(dateanos.lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void agregar (pokemon nuevo)
        {
            AccesoDatos accedatos = new AccesoDatos();
            try
            {
              
                accedatos.SetearConsulta("insert into POKEMONS(Numero,Nombre,Descripcion,Activo,idtipo,idDebilidad,urlimagen) values("+nuevo.numero+",'"+nuevo.nombre+"','"+nuevo.descripcion+"',1,@idTipo,@idDebilidad,@urlimagen)");
                accedatos.setearParametro("@idTipo",nuevo.tipo.id);
                accedatos.setearParametro("@idDebilidad", nuevo.debilidad.id);
                accedatos.setearParametro("@urlimagen",nuevo.urlImagen);
                accedatos.ejecutarAccion();
            }
            catch (Exception ex) 
            {

                throw ex;
            }
            finally
            {
                accedatos.CerrarConexion();
            }
        }
        public void agregarConSP(pokemon nuevo)
        {
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearProcedimiento("storedAltaPokemon");
        //        datos.setearParametro("@numero", nuevo.Numero);
        //        datos.setearParametro("@nombre", nuevo.Nombre);
        //        datos.setearParametro("@desc", nuevo.Descripcion);
        //        datos.setearParametro("@img", nuevo.UrlImagen);
        //        datos.setearParametro("@idTipo", nuevo.Tipo.Id);
        //        datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
        //        //datos.setearParametro("@idEvolucion", null);
        //        datos.ejecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}
        AccesoDatos accesodatos = new AccesoDatos();
            try
            {
                accesodatos.setearProcedimiento("storedPokemons");
                accesodatos.setearParametro("@numero", nuevo.numero);
                accesodatos.setearParametro("@nombre", nuevo.nombre);
                accesodatos.setearParametro("@desc", nuevo.descripcion);
                accesodatos.setearParametro("@img", nuevo.urlImagen);
                accesodatos.setearParametro("@idTipo", nuevo.tipo.id);
                accesodatos.setearParametro("@idDebilidad", nuevo.debilidad.id);
                //accedatos.setearParametro("@idTipo", null);
                accesodatos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesodatos.CerrarConexion();
            }
        }
        public void modificar (pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update POKEMONS set Numero=1,Nombre=@nombre,Descripcion=@descr,UrlImagen=@img,IdTipo=@idtipo,Iddebilidad=@idDeb where Id=@id");
                datos.setearParametro("@nombre", poke.nombre);
                datos.setearParametro("@descr", poke.descripcion);
                datos.setearParametro("@img",poke.urlImagen);
                datos.setearParametro("@idtipo",poke.tipo.id);
                datos.setearParametro("@idDeb",poke.debilidad.id);
                datos.setearParametro("@id",poke.id);

                datos.ejecutarAccion();
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
        public void modificarConSp(pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarPokemon");
                datos.setearParametro("@nombre", poke.nombre);
                datos.setearParametro("@descr", poke.descripcion);
                datos.setearParametro("@img", poke.urlImagen);
                datos.setearParametro("@idtipo", poke.tipo.id);
                datos.setearParametro("@idDeb", poke.debilidad.id);
                datos.setearParametro("@id", poke.id);

                datos.ejecutarAccion();
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
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from pokemons where id =@id");
                datos.setearParametro("@id",id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EliminarLogico(int id)
        {
            
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearConsulta("update pokemons set activo = 0 where id = @id");
                datos.setearParametro("@id", id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<pokemon> filtrar(string campo, string criterio, string filtro)
        {
            List<pokemon> lista = new List<pokemon>();
            AccesoDatos dateanos = new AccesoDatos();
            try
            {
                string consulta = "select Numero,Nombre,P.descripcion,urlImagen,E.descripcion tipo,d.Descripcion debilidad,p.IdDebilidad,p.IdTipo,p.id from POKEMONS P,ELEMENTOS E,ELEMENTOS d where E.Id=P.IdTipo and D.Id=P.IdDebilidad and P.activo=1 and ";
                if (campo == "Numero")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "P.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "P.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "P.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                dateanos.SetearConsulta(consulta);
                dateanos.EjecutarLectura();
                while (dateanos.lector.Read())

                {
                    pokemon aux = new pokemon();
                    aux.id = (int)dateanos.lector["id"];
                    aux.numero = dateanos.lector.GetInt32(0);
                    aux.nombre = (string)dateanos.lector["nombre"];
                    aux.descripcion = (string)dateanos.lector["Descripcion"];

                    if (!(dateanos.lector["urlImagen"] is DBNull))
                        aux.urlImagen = (string)dateanos.lector["urlImagen"];

                    aux.tipo = new elementos();
                    aux.tipo.id = (int)dateanos.lector["idTipo"];
                    aux.tipo.Descripcion = (string)dateanos.lector["tipo"];

                    aux.debilidad = new elementos();
                    aux.debilidad.id = (int)dateanos.lector["idDebilidad"];
                    aux.debilidad.Descripcion = (string)dateanos.lector["debilidad"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
