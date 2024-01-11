using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIBase.Application.Interfaces;
using WebAPIBase.Contracts.Requests.Countries;
using WebAPIBase.Contracts.Responses.Countries;
using WebAPIBase.Domain.Entities;

namespace WebAPIBase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class CountriesController : ControllerBase
{
    public readonly ICountriesHandler _countriesHandler;

    public CountriesController(ICountriesHandler countriesHandler)
    {
        _countriesHandler = countriesHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CountriesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var countries = await _countriesHandler.GetAllAsync();
        var response = new CountriesResponse(countries.Select(x => new CountryResponse(x.Id, x.Name, x.CountryCode)).ToList());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        Country? country = await _countriesHandler.GetByIdAsync(id);
        if (country is null)
        {
            return NotFound();
        }
        var countryResponse = new CountryResponse(country.Id, country.Name, country.CountryCode);
        return Ok(countryResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateCountryRequest createRequest)
    {
        var insertedCountry = await _countriesHandler.CreateAsync(createRequest.Name, createRequest.CountryCode);
        var countryResponse = new CountryResponse(insertedCountry.Id, insertedCountry.Name, insertedCountry.CountryCode);

        return CreatedAtAction(nameof(GetById), new { id = insertedCountry.Id }, countryResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, UpdateCountryRequest updateRequest)
    {
        var updatedCountry = await _countriesHandler.UpdateAsync(id, updateRequest.Name, updateRequest.CountryCode);
        var countryResponse = new CountryResponse(updatedCountry.Id, updatedCountry.Name, updatedCountry.CountryCode);

        return Ok(countryResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _countriesHandler.DeleteAsync(id);
        return NoContent();
    }
}