using Microsoft.AspNetCore.Mvc;
using PerpustakaanAPI.Context;
using PerpustakaanAPI.Models;

namespace PerpustakaanAPI.Controllers
{
    [ApiController]
    [Route("api/buku")]
    public class BukuController : ControllerBase
    {
        private string _constr;

        public BukuController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi")!;
        }

        // GET api/buku
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var ctx = new BukuContext(_constr);
                var list = ctx.GetAll();
                return Ok(new ApiResponse<List<Buku>>
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
                    message = "Gagal mengambil data buku: " + ex.Message
                });
            }
        }

        // GET api/buku/{id}
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var ctx = new BukuContext(_constr);
                var buku = ctx.GetById(id);
                if (buku == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Buku dengan id {id} tidak ditemukan"
                    });

                return Ok(new ApiResponse<Buku>
                {
                    status = "success",
                    data = buku
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengambil data buku: " + ex.Message
                });
            }
        }

        // POST api/buku
        [HttpPost]
        public ActionResult Insert([FromBody] Buku buku)
        {
            try
            {
                if (string.IsNullOrEmpty(buku.judul) || string.IsNullOrEmpty(buku.pengarang))
                    return BadRequest(new ApiError
                    {
                        status = "error",
                        message = "Judul dan pengarang tidak boleh kosong"
                    });

                var ctx = new BukuContext(_constr);
                bool hasil = ctx.Insert(buku);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal menambahkan buku"
                    });

                return StatusCode(201, new ApiResponse<string>
                {
                    status = "success",
                    data = "Buku berhasil ditambahkan"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal menambahkan buku: " + ex.Message
                });
            }
        }

        // PUT api/buku/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Buku buku)
        {
            try
            {
                var ctx = new BukuContext(_constr);
                var exist = ctx.GetById(id);
                if (exist == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Buku dengan id {id} tidak ditemukan"
                    });

                bool hasil = ctx.Update(id, buku);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal mengupdate buku"
                    });

                return Ok(new ApiResponse<string>
                {
                    status = "success",
                    data = "Buku berhasil diupdate"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal mengupdate buku: " + ex.Message
                });
            }
        }

        // DELETE api/buku/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ctx = new BukuContext(_constr);
                var exist = ctx.GetById(id);
                if (exist == null)
                    return NotFound(new ApiError
                    {
                        status = "error",
                        message = $"Buku dengan id {id} tidak ditemukan"
                    });

                bool hasil = ctx.Delete(id);
                if (!hasil)
                    return StatusCode(500, new ApiError
                    {
                        status = "error",
                        message = "Gagal menghapus buku"
                    });

                return Ok(new ApiResponse<string>
                {
                    status = "success",
                    data = "Buku berhasil dihapus"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError
                {
                    status = "error",
                    message = "Gagal menghapus buku: " + ex.Message
                });
            }
        }
    }
}