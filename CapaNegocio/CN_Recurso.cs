﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Recurso
    {
        public static string ConvertirHash(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            //USAR LA REFERENCIA DE "System.Security.Cryptography"
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
        public static string GenerarClave()
        {
            string generar_clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return generar_clave;
        }
        public static string GenerarCodigo(string carrera)
        {
            string guid = Guid.NewGuid().ToString("N").Substring(0, 8);
            string generar_codigo = $"UTP-{guid}-{carrera}";
            return generar_codigo;
        }
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("garciajhair22@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("garciajhair22@gmail.com", "mhsg nqgu dlrv nhyy"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }
            return resultado;
        }
    }
}
