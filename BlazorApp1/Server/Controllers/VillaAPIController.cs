using BlazorApp1.Server.Data;
using BlazorApp1.Shared;
using BlazorApp1.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //database
        private readonly ApplicationDbContext _db;

        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
		{
			//add custom validation
			if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("CustomError", "Villa already Exists!");
				return BadRequest(ModelState);
			}

			if (villaDTO == null)
			{
				return BadRequest(villaDTO);
			}

			if (villaDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

			Villa model = new()
			{
				Amenity = villaDTO.Amenity,
				Details = villaDTO.Details,
				ImageUrl = villaDTO.ImageUrl,
				Name = villaDTO.Name,
				Occupancy = villaDTO.Occupancy,
				Rate = villaDTO.Rate,
				Sqft = villaDTO.Sqft
			};

			_db.Villas.Add(model);
			_db.SaveChanges();

			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
		{

			if (villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}

			var existingVilla = _db.Villas.Find(id);
			if (existingVilla == null)
			{
				return NotFound();
			}

			existingVilla.Amenity = villaDTO.Amenity;
			existingVilla.Details = villaDTO.Details;
			existingVilla.ImageUrl = villaDTO.ImageUrl;
			existingVilla.Name = villaDTO.Name;
			existingVilla.Occupancy = villaDTO.Occupancy;
			existingVilla.Rate = villaDTO.Rate;
			existingVilla.Sqft = villaDTO.Sqft;

			_db.Villas.Update(existingVilla);
			_db.SaveChanges();

			return NoContent();

		}

	}
}
