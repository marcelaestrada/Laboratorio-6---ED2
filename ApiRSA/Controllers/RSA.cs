using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CifradoRSA;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;

namespace ApiRSA.Controllers
{
    [Route("api/rsa")]
    [ApiController]
    public class RSA : ControllerBase
    {
        [HttpGet("keys/{p}/{q}")]
        public async Task<IActionResult> generar(int p, int q)
        {
            string rutaPublic = $"./keys/public.key";
            string rutaPrivate = $"./keys/private.key";
            string rutaZip = $"./keys/keys.zip";
            FileStream publicKey = new FileStream(rutaPublic, FileMode.OpenOrCreate, FileAccess.Write);
            FileStream privateKey = new FileStream(rutaPrivate, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter pblc = new StreamWriter(publicKey);
            StreamWriter prvt = new StreamWriter(privateKey);

            try
            {
                Cifrado llaves = new Cifrado();
                List<string> keys = llaves.generarClaves(p, q);
                pblc.Write(keys[0]);
                pblc.Close();
                prvt.Write(keys[1]);
                prvt.Close();

                using(var file = ZipFile.Open(rutaZip, ZipArchiveMode.Create))
                {
                    file.CreateEntryFromFile(rutaPublic, Path.GetFileName(rutaPublic));
                    file.CreateEntryFromFile(rutaPrivate, Path.GetFileName(rutaPrivate));
                }

                System.IO.File.Delete(rutaPublic);
                System.IO.File.Delete(rutaPrivate);

                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{nombre}")]
        [RequestSizeLimit(40971520)]
        public async Task<IActionResult> cifrarDescifrar([FromForm] IFormFile file, [FromForm] IFormFile key, string nombre)
        {
            Cifrado cifDes = new Cifrado();
            string path = @".\temporales\";
            string[] separado = (key.FileName).Split('.');
            string terminacion = separado[0];

            try
            {
                using (var filestream = new FileStream((path + file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Close();
                }

                using (var filestream2 = new FileStream((path + key.FileName), FileMode.Create))
                {
                    await key.CopyToAsync(filestream2);
                    filestream2.Close();
                }

                string pathFile = path + file.FileName;
                FileStream fileS = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
                string linea = System.IO.File.ReadAllText(path + key.FileName, System.Text.Encoding.Default);
                
                if (terminacion == "public")
                {
                    FileStream nuevo = new FileStream(@".\" + nombre + ".txt", FileMode.Create, FileAccess.ReadWrite);
                    string[] data = linea.Split(',');
                    int n = Convert.ToInt32(data[0]);
                    int e = Convert.ToInt32(data[1]);
                    List<byte> bytes = cifDes.cifrar(fileS, n, e);
                    nuevo.Write(bytes.ToArray());
                    nuevo.Close();
                    System.IO.File.Delete(path + file.FileName);
                    System.IO.File.Delete(path + key.FileName);
                }
                else if(terminacion=="private")
                {
                    FileStream nuevo = new FileStream(@".\" + nombre + ".txt", FileMode.Create, FileAccess.ReadWrite);
                    string[] data = linea.Split(',');
                    int n = Convert.ToInt32(data[0]);
                    int d = Convert.ToInt32(data[1]);
                    List<byte> bytes = cifDes.descifrar(fileS, n, d);
                    nuevo.Write(bytes.ToArray());
                    nuevo.Close();
                    System.IO.File.Delete(path + file.FileName);
                    System.IO.File.Delete(path + key.FileName);
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
