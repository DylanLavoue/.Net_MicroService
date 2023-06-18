using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        // public readonly ItemsRepository repository;

        // public readonly IMapper mapper;

        // public ItemsController(ItemsRepository repository, IMapper mapper)
        // {
        //     this.repository = repository;
        //     this.mapper = mapper;
        // }

        private static readonly List<ItemDto?> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze Sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public IEnumerable<ItemDto?> Get()
        {
            return items;
        }


        [HttpGet("{id}")]
        public ItemDto? GetById(Guid id)
        {
            var item = items.SingleOrDefault(item => item?.Id == id);
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.SingleOrDefault(item => item?.Id == id);
            var updatedItem = existingItem! with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };
            var index = items.FindIndex(existingItem => existingItem?.Id == id);
            items[index] = updatedItem;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem?.Id == id);
            items.RemoveAt(index);
            return NoContent();
        }
   

        // GET /items
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        // {
        //     var items = (await repository.GetAllAsync())
        //         .Select(item => mapper.Map<ItemDto>(item));
        //     return Ok(items);
        // }


        // GET /items/{id}
        // [HttpGet("{id}")]
        // public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        // {
        //     var item = await repository.GetAsync(id);
        //     if (item == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(mapper.Map<ItemDto>(item));
        // }



        // POST /items
        // [HttpPost]
        // public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        // {
        //     var item = mapper.Map<Item>(createItemDto);
        //     await repository.CreateAsync(item);
        //     return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        // }

        // PUT /items/{id}
        // [HttpPut("{id}")]
        // public async Task<ActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        // {
        //     var existingItem = await repository.GetAsync(id);
        //     if (existingItem == null)
        //     {
        //         return NotFound();
        //     }
        //     var updatedItem = existingItem with
        //     {
        //         Name = updateItemDto.Name,
        //         Description = updateItemDto.Description,
        //         Price = updateItemDto.Price
        //     };
        //     await repository.UpdateAsync(updatedItem);
        //     return NoContent();
        // }

        // DELETE /items/{id}
        // [HttpDelete("{id}")]
        // public async Task<ActionResult> DeleteAsync(Guid id)
        // {
        //     var existingItem = await repository.GetAsync(id);
        //     if (existingItem == null)
        //     {
        //         return NotFound();
        //     }
        //     await repository.RemoveAsync(id);
        //     return NoContent();
        // }
    }
}