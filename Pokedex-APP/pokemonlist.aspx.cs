using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Pokedex_APP
{
    public partial class pokemonlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pokemonNegocio negocio = new pokemonNegocio();
            DGVpokemons.DataSource = negocio.ListarConsp();
            DGVpokemons.DataBind();
        }

        protected void DGVpokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id= DGVpokemons.SelectedDataKey.Value.ToString();
            Response.Redirect("formularioPokemon.aspx?id="+id);
        }

        protected void DGVpokemons_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
         
        }

        protected void DGVpokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVpokemons.PageIndex = e.NewPageIndex;
            DGVpokemons.DataBind();
        }
    }
}