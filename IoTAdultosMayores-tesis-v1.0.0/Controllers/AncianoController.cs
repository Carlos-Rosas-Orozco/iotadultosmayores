using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IoTAdultosMayores.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;

/*
Controlador que realiza todos los servicios del backend para las vistas y el modelo de la clase Anciano
 */

namespace IoTAdultosMayores.Controllers
{
    public class AncianoController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        //Conexion con MySQL, el objeto se llamara connection
        //Tienes que agregar las direcciones para la conexion con tu servidor, lo voy a dejar en blanco
        SqlConnection connection = new SqlConnection("");

        public AncianoController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View("AddAnciano");//Regresa la vista princial de la clase de Anciano, en este caso seria la vista AddAnciano
        }
        /*
            La funcion de Save solo guarda la informacion en la base de datos, es un insert si esta vacio, si ya hay 
            informacion en la base de datos realiza un update para  insertar los campos, solo como medida de prueba para
            para el prototipo.
         */
        public IActionResult Save()
        {
            int Id = 0;
            SqlCommand com = new SqlCommand("", connection);

            if (Request.Form["opc"] == "1")
                com.CommandText = "Insert into Anciano " +
                    "(An_primerNombre) values ('" + Request.Form["primerNombre"].ToString().ToUpper() +
                    "(An_segundoNombre) values ('" + Request.Form["segundoNombre"].ToString().ToUpper() +
                    "(An_primerApellido) values ('" + Request.Form["primerApellido"].ToString().ToUpper() +
                    "(An_segundoApellido) values ('" + Request.Form["segundoApellido"].ToString().ToUpper() +
                    "(An_relacion) values ('" + Request.Form["relacion"].ToString().ToUpper() +
                    "(An_fechaNacimiento) values ('" + Request.Form["fechaNacimiento"].ToString().ToUpper() +
                    "(An_valMinNormal) values ('" + Request.Form["valMinNormal"].ToString().ToUpper() +
                    "(An_valMaxNormal) values ('" + Request.Form["valMaxNormal"].ToString().ToUpper() +
                    "(An_valMinIrregular) values ('" + Request.Form["valMinIrregular"].ToString().ToUpper() +
                    "(An_valMinIrregular) values ('" + Request.Form["valMinIrregular"].ToString().ToUpper() + "')";
            else
            {//no esta terminado esto
                com.CommandText = "Update Anciano Set An_primerNombre='" + Request.Form["primerNombre"].ToString().ToUpper() + "' where An_Id=" + Request.Form["Id"];
                Id = Convert.ToInt32(Request.Form["Id"]);
            }

            connection.Open();
            com.ExecuteNonQuery();
            if (Request.Form["opc"] == "1")
            {
                com.CommandText = "Select top 1 * from Anciano order by an_id desc";
                SqlDataReader DR = com.ExecuteReader();
                DR.Read();
                Id = Convert.ToInt32(DR["Ma_Id"]);
            }
            connection.Close();
            return Redirect("MainPage");
        }
        /*
            Edit es para realizar una update, aunque la update se lleva a cabo en la funciones Save
            , Edit solo regresa la informacion de la base de datos a cada uno de los campos correspondenties para la vista
            que es EditAnciano
         */
        public IActionResult Edit(string Id)
        {
            SqlCommand com = new SqlCommand("Select * from Marcas where Ma_Id=" + Id, connection);//Se busca el id de acuerdo al registro en la base de datos
            SqlDataReader DR;
            Anciano row = new Anciano { An_Id = 0, An_primerNombre = "", An_segundoNombre = "", An_primerApellido = "", An_segundoApellido = "", An_relacion = "",
                                        /*An_fechaNacimiento = 0, Estoy investigando como guardar un tipo date*/ An_valMinNormal = 0, An_valMaxNormal = 0, An_valMinIrregular = 0, An_valMaxIrregular = 0};
            connection.Open();
            DR = com.ExecuteReader();
            if (DR.Read())
            {
                //relacion entre el campo y el registro para regresar los datos
                row = new Anciano { An_Id = Convert.ToInt32(DR["An_Id"]), An_primerNombre = DR["An_primerNombre"].ToString(),
                                    An_segundoNombre = DR["An_segundoNombre"].ToString(), An_primerApellido = DR["An_primerApellido"].ToString(),
                                    An_segundoApellido = DR["An_segundoApellido"].ToString(), An_fechaNacimiento = Convert.ToDateTime(DR["An_fechaNacimiento"]),
                                    An_relacion = DR["An_relacion"].ToString(),
                                    An_valMinNormal = Convert.ToInt32(DR["An_valMinNormal"]), An_valMaxNormal = Convert.ToInt32(DR["An_valMaxNormal"]),
                                    An_valMinIrregular = Convert.ToInt32(DR["An_valMinIrregular"]), An_valMaxIrregular = Convert.ToInt32(DR["An_valMaxIrregular"])};

            }
            connection.Close();
            return View(row);
        }
        /*
        Delete borra el registro en la db de acuerdo al numero del id del Anciano
         */
        public IActionResult Delete(string Id)
        {
            SqlCommand com = new SqlCommand("Delete Anciano where An_Id=" + Id, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();
            return Redirect("/Anciano");
        }
    }
}