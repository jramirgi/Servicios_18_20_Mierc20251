using Servicios_18_20.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_18_20.Clases
{
    public class clsEmpleado
    {
        private DBSuperEntities dbSuper = new DBSuperEntities(); //Objeto para gestionar los datos del empleado con la base de datos
        public EMPLeado empleado { get; set; } //Objeto tipo Empleado para gestionar el CRUD en la base de datos
        public string Insertar()
        {
            try
            {
                dbSuper.EMPLeadoes.Add(empleado); //Agrega un empleado a la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                dbSuper.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado insertado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al insertar el empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public string Actualizar()
        {
            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el empleado
                EMPLeado empl = Consultar(empleado.Documento);
                if (empl == null)
                {
                    return "Empleado no existe"; //Retorna un mensaje de error
                }
                dbSuper.EMPLeadoes.AddOrUpdate(empleado); //Actualiza el empleado en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                dbSuper.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch(Exception ex)
            {
                return "Error al actualizar el empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        private bool Validar(string Documento)
        {
            if (Consultar(Documento) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public EMPLeado Consultar(string Documento) 
        {
            //Expresiones lambda se convierte en objetos del tipo que se esta consultando
            //El método FirstOrDefault retorna el primer objeto que cumpla con la condición que se escribe en la consulta
            EMPLeado empl = dbSuper.EMPLeadoes.FirstOrDefault(e => e.Documento == Documento);
            return empl;
        }
        public string Eliminar()
        {
            //Primero se debe consultar
            try
            {
                EMPLeado empl = Consultar(empleado.Documento);
                if (empl == null)
                {
                    return "Empleado no existe"; //Retorna un mensaje de error
                }
                //Si el empleado existe se elimina
                dbSuper.EMPLeadoes.Remove(empl); //Elimina el empleado de la lista
                dbSuper.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado eliminado correctamente"; //Retorna un mensaje de confirmación

            }
            catch (Exception ex)
            {
                return "Error al eliminar el empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public string EliminarXDocumento(string Documento)
        {
            //Primero se debe consultar
            try
            {
                EMPLeado empl = Consultar(Documento);
                if (empl == null)
                {
                    return "Empleado no existe"; //Retorna un mensaje de error
                }
                //Si el empleado existe se elimina
                dbSuper.EMPLeadoes.Remove(empl); //Elimina el empleado de la lista
                dbSuper.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado eliminado correctamente"; //Retorna un mensaje de confirmación

            }
            catch (Exception ex)
            {
                return "Error al eliminar el empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public List<EMPLeado> ConsultarTodos()
        {
            return dbSuper.EMPLeadoes
                .OrderBy(e => e.PrimerApellido) //Ordena los empleados por apellido
                //.Where(e => e.Direccion == "") //Filtra los empleados que no tienen dirección
                .ToList(); //Retorna una lista de empleados
        }
    }
}