using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityRepository _cityRepository;

        public CityController(ILogger<CityController> logger, ICityRepository cityRepository)
        {
            _logger = logger;
            _cityRepository = cityRepository;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _cityRepository.GetCities();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            var city = await _cityRepository.GetById(id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (city == null)
                return BadRequest("City object is null");

            await _cityRepository.Add(city);
            await _cityRepository.SaveChanges();
            return CreatedAtAction("GetCityById", new { id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity([FromBody] City city)
        {
            var existingCity = await _cityRepository.GetById(city.Id);
            if (existingCity == null)
                return NotFound();

            await _cityRepository.Update(city);
            await _cityRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var existingCity = await _cityRepository.GetById(id);

            if (existingCity == null)
                return NotFound();

            await _cityRepository.Delete(id);
            await _cityRepository.SaveChanges();

            return Ok();
        }
    }
}