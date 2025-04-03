using Servicios_18_20.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_18_20.Clases
{
    public class clsProducto
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public PRODucto producto { get; set; }
        public List<PRODucto> ConsultarTodos()
        {
            return dbSuper.PRODuctoes
                .OrderBy(p=> p.Nombre)
                .ToList();
        }
        public PRODucto Consultar(int Codigo)
        {
            return dbSuper.PRODuctoes.FirstOrDefault(p=> p.Codigo == Codigo);
        }
        public List<PRODucto> ConsultarXTipo(int codigoTipoProducto)
        {
            return dbSuper.PRODuctoes
                    .Where(p => p.CodigoTipoProducto == codigoTipoProducto)
                    .OrderBy(p => p.Nombre)
                    .ToList();
        }
        public string Insertar()
        {
            try
            {
                dbSuper.PRODuctoes.Add(producto);
                dbSuper.SaveChanges();
                return "Se grabó el producto: " + producto.Nombre + " en la base de datos.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                PRODucto prod = Consultar(producto.Codigo);
                if (prod == null)
                {
                    return "El producto no está definido en la base de datos";
                }
                dbSuper.PRODuctoes.AddOrUpdate(producto);
                dbSuper.SaveChanges();
                return "Se actualizó el producto: " + producto.Nombre + " en la base de datos.";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public string Eliminar(int Codigo)
        {
            try
            {
                PRODucto prod = Consultar(Codigo);
                if (prod == null)
                {
                    return "El producto no está definido en la base de datos";
                }
                dbSuper.PRODuctoes.Remove(prod); //Se remueve el objeto que se consultó, no el que se pasa en la propieadad de la clase
                dbSuper.SaveChanges();
                return "Se eliminó el producto: " + prod.Nombre + " en la base de datos.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ModificarActivo(int Codigo, bool Activo)
        {
            try
            {
                PRODucto prod = Consultar(Codigo);
                if (prod == null)
                {
                    return "El producto no está definido en la base de datos";
                }
                prod.Activo = Activo;
                dbSuper.SaveChanges();
                if (Activo)
                {
                    return "Se activó el producto: " + prod.Nombre;
                }
                else
                {
                    return "Se inactivó el producto: " + prod.Nombre;
                }
            }
            catch(Exception ex)
            { return ex.Message; }
        }
        public string GrabarImagen(string idProducto, List<string> Archivos)
        {
            foreach (string archivo in Archivos)
            {
                ImagenesProducto imagen = new ImagenesProducto();
                imagen.idProducto = Convert.ToInt32(idProducto);
                imagen.NombreImagen = archivo;
                dbSuper.ImagenesProductoes.Add(imagen);
                dbSuper.SaveChanges();
            }
            return "Archivos procesados correctamente";
        }
        public string GrabarImagenProducto(int idProducto, List<string> Imagenes)
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    ImagenesProducto imagenProducto = new ImagenesProducto();
                    imagenProducto.idProducto = idProducto;
                    imagenProducto.NombreImagen = imagen;
                    dbSuper.ImagenesProductoes.Add(imagenProducto);
                    dbSuper.SaveChanges();
                }
                return "Se grabó la información en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public IQueryable ListarImagenesXProducto(int idProducto)
        {
            return from P in dbSuper.Set<PRODucto>()
                   join TP in dbSuper.Set<TIpoPRoducto>()
                   on P.CodigoTipoProducto equals TP.Codigo
                   join I in dbSuper.Set<ImagenesProducto>()
                   on P.Codigo equals I.idProducto
                   where I.idProducto == idProducto
                   orderby I.NombreImagen
                   select new
                   {
                       idTipoProducto = TP.Codigo,
                       TipoProducto = TP.Nombre,
                       idProducto = P.Codigo,
                       Producto = P.Nombre,
                       Imagen = I.NombreImagen
                   };
        }

    }
}