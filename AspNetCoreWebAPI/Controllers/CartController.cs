using AspNetCoreWebAPI.Entities;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/cart")]
    public class CartController : Controller
    {

        IGenericEFRepository _rep;

        public CartController(IGenericEFRepository rep)
        {
            _rep = rep;
        }

        // GET api/cart
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var item = _rep.Get<Cart>();
            var DTOs = Mapper.Map<IEnumerable<CartDTO>>(item);
            return Ok(DTOs);
        }

        // GET api/cart/:cartID:
        [AllowAnonymous]
        [HttpGet("{cartId}")]
        public IActionResult Get(string cartId)
        {

            var carts = _rep.Get<Cart>().Where(c =>
                c.CartID.Equals(cartId));

            var DTOs = Mapper.Map<IEnumerable<CartDTO>>(carts);
            return Ok(DTOs);
        }

        // DELETE api/cart/:id:
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_rep.Exists<Cart>(id)) return NotFound();

            var entityToDelete = _rep.Get<Cart>(id);

            _rep.Delete(entityToDelete);

            if (!_rep.Save()) return StatusCode(500,
                "A problem occurred while handling your request.");

            return NoContent();
        }

        // PATCH api/cart/:id: 
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<CartUpdateDTO> DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _rep.Get<Cart>(id);
            if (entity == null) return NotFound();

            var entityToPatch = Mapper.Map<CartUpdateDTO>(entity);
            DTO.ApplyTo(entityToPatch, ModelState);
            TryValidateModel(entityToPatch);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Mapper.Map(entityToPatch, entity);

            if (!_rep.Save()) return StatusCode(500,
                "A problem happened while handling your request.");

            return NoContent();
        }
    }
}
