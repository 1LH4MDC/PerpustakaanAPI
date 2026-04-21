using Microsoft.AspNetCore.Mvc;
using PerpustakaanAPI.Context;
using PerpustakaanAPI.Models;

namespace PerpustakaanAPI.Controllers
{
    [ApiController]
    [Route("api/peminjaman")]
    public class PeminjamanController : ControllerBase
    {
        private string _constr;

        public PeminjamanController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi")!;
        }

        // GET api/peminjaman
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var ctx = new PeminjamanContext(_constr);
                var list = ctx.GetAll();
                return Ok(new ApiResponse<List<Peminjaman>>
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
                    message = "Gagal mengambil data peminjaman: " + ex.Message
                });
            }
        }

        // GET api/peminjaman/{id}
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var ctx = new PeminjamanContext(_constr);
                var item = ctx.GetById(id);
                if (item == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Peminjaman dengan id {id} tidak ditemukan"
                    });

                return Ok(new ApiResponse<Peminjaman>
                {
                    status = "success",
                    data = item
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengambil data peminjaman: " + ex.Message
                });
            }
        }

        // POST api/peminjaman
        [HttpPost]
        public ActionResult Insert([FromBody] Peminjaman pm)
        {
            try
            {
                if (pm.id_buku == 0 || pm.id_anggota == 0)
                    return BadRequest(new ApiError
                    {
                        status = "error",
                        message = "id_buku dan id_anggota tidak boleh kosong"
                    });

                var ctx = new PeminjamanContext(_constr);
                bool hasil = ctx.Insert(pm);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal menambahkan peminjaman"
                    });

                return StatusCode(201, new ApiResponse<string>
                {
                    status = "success",
                    data = "Peminjaman berhasil ditambahkan"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal menambahkan peminjaman: " + ex.Message
                });
            }
        }

        // PUT api/peminjaman/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Peminjaman pm)
        {
            try
            {
                var ctx = new PeminjamanContext(_constr);
                var exist = ctx.GetById(id);
                if (exist == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Peminjaman dengan id {id} tidak ditemukan"
                    });

                bool hasil = ctx.Update(id, pm);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal mengupdate peminjaman"
                    });

                return Ok(new ApiResponse<string>
                {
                    status = "success",
                    data = "Peminjaman berhasil diupdate"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengupdate peminjaman: " + ex.Message
                });
            }
        }

        // DELETE api/peminjaman/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ctx = new PeminjamanContext(_constr);
                var exist = ctx.GetById(id);
                if (exist == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Peminjaman dengan id {id} tidak ditemukan"
                    });

                bool hasil = ctx.Delete(id);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal menghapus peminjaman"
                    });

                return Ok(new ApiResponse<string>
                {
                    status = "success",
                    data = "Peminjaman berhasil dihapus"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal menghapus peminjaman: " + ex.Message
                });
            }
        }
    }
}