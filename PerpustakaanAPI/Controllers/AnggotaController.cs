using Microsoft.AspNetCore.Mvc;
using PerpustakaanAPI.Context;
using PerpustakaanAPI.Models;

namespace PerpustakaanAPI.Controllers
{
    [ApiController]
    [Route("api/anggota")]
    public class AnggotaController : ControllerBase
    {
        private string _constr;

        public AnggotaController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi")!;
        }

        // GET api/anggota
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var ctx = new AnggotaContext(_constr);
                var list = ctx.GetAll();
                return Ok(new ApiResponse<List<Anggota>>
                {
                    status = "success",
                    data = list,
                    meta = new Meta { total = list.Count }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengambil data anggota: " + ex.Message
                });
            }
        }

        // GET api/anggota/{id}
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var ctx = new AnggotaContext(_constr);
                var anggota = ctx.GetById(id);
                if (anggota == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Anggota dengan id {id} tidak ditemukan"
                    });

                return Ok(new ApiResponse<Anggota>
                {
                    status = "success",
                    data = anggota
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengambil data anggota: " + ex.Message
                });
            }
        }

        // POST api/anggota
        [HttpPost]
        public ActionResult Insert([FromBody] Anggota anggota)
        {
            try
            {
                if (string.IsNullOrEmpty(anggota.nama) || string.IsNullOrEmpty(anggota.email))
                    return BadRequest(new ApiError
                    {
                        status = "error",
                        message = "Nama dan email tidak boleh kosong"
                    });

                var ctx = new AnggotaContext(_constr);
                bool hasil = ctx.Insert(anggota);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal menambahkan anggota"
                    });

                return StatusCode(201, new ApiResponse<string>
                {
                    status = "success",
                    data = "Anggota berhasil ditambahkan"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal menambahkan anggota: " + ex.Message
                });
            }
        }

        // PUT api/anggota/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Anggota anggota)
        {
            try
            {
                var ctx = new AnggotaContext(_constr);
                var exist = ctx.GetById(id);
                if (exist == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Anggota dengan id {id} tidak ditemukan"
                    });

                bool hasil = ctx.Update(id, anggota);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal mengupdate anggota"
                    });

                return Ok(new ApiResponse<string>
                {
                    status = "success",
                    data = "Anggota berhasil diupdate"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengupdate anggota: " + ex.Message
                });
            }
        }

        // DELETE api/anggota/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ctx = new AnggotaContext(_constr);
                var exist = ctx.GetById(id);
                if (exist == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Anggota dengan id {id} tidak ditemukan"
                    });

                bool hasil = ctx.Delete(id);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal menghapus anggota"
                    });

                return Ok(new ApiResponse<string>
                {
                    status = "success",
                    data = "Anggota berhasil dihapus"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal menghapus anggota: " + ex.Message
                });
            }
        }
    }
}