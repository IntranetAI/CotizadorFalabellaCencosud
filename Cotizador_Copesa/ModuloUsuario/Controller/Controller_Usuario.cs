using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Copesa.ModuloUsuario.Model;
using System.Data.SqlClient;

namespace Cotizador_Copesa.ModuloUsuario.Controller
{
    public class Controller_Usuario
    {
        public Usuario IniciarSession(string Correo, string Contraseña)
        {
            Usuario user = new Usuario();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Usuario_AccesoLogin";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    cmd.Parameters.AddWithValue("@Password", Contraseña);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.IDUsuario = Convert.ToInt32(reader["ID_Usuario"].ToString());
                        user.NombreCompleto = reader["Nombre_Completo"].ToString();
                        user.Perfil = reader["Perfil"].ToString();
                        user.Empresa = reader["Empresa"].ToString();
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return user;
        }

        public Usuario BuscarUsuario_ID(int id)
        {
            Usuario user = new Usuario();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Usuario_BuscarUsuario_ID";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.NombreCompleto = reader["Nombre_Completo"].ToString();
                        user.Telefono = reader["Telefono"].ToString();
                        user.Celular = reader["Celular"].ToString();
                        user.Correo = reader["Correo"].ToString();
                        user.Empresa = reader["Empresa"].ToString();
                        user.Fax = reader["Fax"].ToString();
                        user.Perfil = reader["Perfil"].ToString();
                    }
                }
                catch
                {

                }
            }
            con.CerrarConexion();
            return user;
        }

        public Usuario BuscarPersonalComercial_Empresa(string Empresa)
        {
            Usuario user = new Usuario();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "BuscarPersonalComercial_Empresa";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.NombreCompleto = reader["Nombre_Completo"].ToString();
                        user.Telefono = reader["Telefono"].ToString();
                        user.Celular = reader["Celular"].ToString();
                        user.Correo = reader["Correo"].ToString();
                        user.Empresa = Empresa;
                        user.Fax = reader["Fax"].ToString();
                    }
                }
                catch
                {

                }
            }
            con.CerrarConexion();
            return user;
        }
    }
}