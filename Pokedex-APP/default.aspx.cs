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
    public partial class _default : System.Web.UI.Page
    {
        public List<pokemon> listapokemon { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            pokemonNegocio negocio = new pokemonNegocio();
            listapokemon = negocio.ListarConsp();
           
            Repetidor.DataSource = listapokemon;
            Repetidor.DataBind();
        }
    }
}