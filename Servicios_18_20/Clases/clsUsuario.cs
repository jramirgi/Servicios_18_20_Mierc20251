using Servicios_18_20.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_18_20.Clases
{
    public class clsUsuario
    {
        private DBSuperEntities DBSuper = new DBSuperEntities();
        public Usuario usuario { get; set; }
        public string CrearUsuario(int idPerfil)
        {
            try
            {
                clsCypher cypher = new clsCypher();
                cypher.Password = usuario.Clave;
                if (cypher.CifrarClave())
                {
                    //Graba el usuario
                    usuario.Clave = cypher.PasswordCifrado;
                    usuario.Salt = cypher.Salt;
                    DBSuper.Usuarios.Add(usuario);
                    DBSuper.SaveChanges();
                    //Grabar el perfil del usuario
                    Usuario_Perfil usuarioPerfil = new Usuario_Perfil();
                    usuarioPerfil.idPerfil = idPerfil;
                    usuarioPerfil.Activo = true;
                    usuarioPerfil.idUsuario = usuario.id;
                    DBSuper.Usuario_Perfil.Add(usuarioPerfil);
                    DBSuper.SaveChanges();
                    return "Se creó el usuario exitosamente";
                }
                else
                {
                    return "No se pudo cifrar la clave";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
