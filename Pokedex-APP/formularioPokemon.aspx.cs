using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using dominio;
using negocio;
namespace Pokedex_APP
{
    public partial class formularioPokemon : System.Web.UI.Page
    {
      public bool confirmaliminacion {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            confirmaliminacion = false;
            if (!IsPostBack)
            {
                ElementoNegocio negocio = new ElementoNegocio();
                List<elementos> lista = negocio.listar();

                ddlTipo.DataSource = lista;
                ddlTipo.DataValueField = "id";
                ddlDebilidad.DataValueField = "descripcion";
                ddlTipo.DataBind();

                ddlDebilidad.DataSource = lista;
                ddlDebilidad.DataValueField = "id";
                ddlDebilidad.DataValueField = "descripcion";
                ddlDebilidad.DataBind();
            }
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            if (id !="" && !IsPostBack)
            {
                pokemonNegocio negocio = new pokemonNegocio();
                pokemon seleccionado = (negocio.Listar(id))[0];

                txtId.Text = id;
                txtNombre.Text = seleccionado.nombre;
                txtNumero.Text = seleccionado.numero.ToString();
                txtDescripcion.Text = seleccionado.descripcion;
                txtImagenUrl.Text = seleccionado.urlImagen;

                ddlTipo.SelectedValue = seleccionado.tipo.id.ToString();
                ddlDebilidad.SelectedValue = seleccionado.debilidad.id.ToString();
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                pokemon nuevo = new pokemon();
                pokemonNegocio negocio = new pokemonNegocio();


                nuevo.numero = int.Parse(txtNumero.Text);
                nuevo.nombre = txtNombre.Text;
                nuevo.descripcion = txtDescripcion.Text;
                nuevo.urlImagen = txtImagenUrl.Text;

                nuevo.tipo = new elementos();
                nuevo.tipo.id = int.Parse(ddlTipo.SelectedValue);
                nuevo.debilidad = new elementos();
                nuevo.debilidad.id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.id=int.Parse(txtId.Text);
                    negocio.modificarConSp(nuevo);

                }

                else
                     negocio.agregarConSP(nuevo);

                Response.Redirect("pokemonlist.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            confirmaliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    pokemonNegocio negocio = new pokemonNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("pokemonlist.aspx");
                }
              
            }
            catch (Exception ex)
            {

                Session.Add("error",ex);
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                pokemonNegocio negocio = new pokemonNegocio();
                negocio.EliminarLogico(int.Parse(txtId.Text));
                Response.Redirect("pokemonlist.aspx");
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }
    }
}