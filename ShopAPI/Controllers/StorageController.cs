//using Application.Services;
//using Core.DTO;
//using Microsoft.AspNetCore.Mvc;

//namespace ShopAPI.Controllers
//{
//    public class StorageController : ControllerBase
//    {
//        private readonly StorageService _storageService;

//        public StorageController(StorageService wareHouseService)
//        {
//            _storageService = wareHouseService;
//        }

//        [HttpPost("Create")]
//        public async Task<IActionResult> Create([FromBody] StorageDTO wareHouseDTO, CancellationToken cancellationToken)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var warehouse = await _storageService.Create(wareHouseDTO, cancellationToken);
//            return Ok(warehouse);
//        }

//        [HttpDelete("Delete/{id}")]
//        public async Task<IActionResult> Delete(StorageDTO storageDTO CancellationToken cancellationToken)
//        {
//            var warehouse = await _storageService.Delete(storageDTO.Id, cancellationToken);
//            return Ok(warehouse);
//        }



//    }
//}

