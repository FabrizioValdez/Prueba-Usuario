using System;
using System.Web.UI;

namespace PruebaUsuarios
{
    public partial class Inicio : Page
    {
        private static string[] nombres = {
            "Carlos", "Maria", "Juan", "Ana", "Luis", "Sofia", "Pedro", "Laura",
            "Diego", "Carmen", "Miguel", "Isabella", "Fernando", "Valentina", " Andres",
            "Camila", "Ricardo", "Patricia", "Javier", "Lorena", "Roberto", "Fernanda",
            "Alberto", "Gabriela", "Eduardo", "Daniela", "Francisco", "Renata", "Manuel",
            "Adriana", "Sergio", "Victoria", "Gustavo", "Elena", "Rafael", "Julia",
            "Antonio", "Paula", "Oscar", "Silvia", "Ramon", "Beatriz", "Pablo", "Martha",
            "Hugo", "Alicia", "Ivan", "Teresa", "Jorge", "Leticia"
        };

        private static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indice = random.Next(nombres.Length);
                lblNombreUsuario.Text = nombres[indice];
            }
        }
    }
}
