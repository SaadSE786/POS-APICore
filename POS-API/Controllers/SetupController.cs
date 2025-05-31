using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_API.Model;
using Microsoft.EntityFrameworkCore;
using POS_API.Services;
using POS_API.BusinessObjects;
using POS_API.Models;
using System.Net;

namespace POS_API.Controllers
{
    [Route("api/setup")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly POSEntities db;
        private readonly SQLService _sqlService;

        public SetupController(DbContextOptions<POSEntities> options, SQLService sqlService)
        {
            db = new POSEntities(options);
            _sqlService = sqlService;
        }

        #region Level1
        [HttpPost, Route("addLevel1")]
        public async Task<IActionResult> AddLevel1([FromBody] Level1 lvl)
        {
            if (lvl == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblLevel1 level1 = new tblLevel1
                {
                    //intLevel1Id = await _sqlService.GetMaxId("tblLevel1", "intLevel1Id"),
                    varLevel1Name = lvl.varLevel1Name,
                    dtCreationDate = DateTime.Now,
                    intCreatedBy = lvl.intCreatedBy,
                    intCompanyId = lvl.intCompanyId
                };
                db.tblLevel1.Add(level1);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error" });
            }
        }

        [HttpGet, Route("getLevel1")]
        public IActionResult GetLevel1()
        {
            try
            {
                var level1s = db.tblLevel1.Select(l => new Level1
                {
                    intLevel1Id = l.intLevel1Id,
                    varLevel1Name = l.varLevel1Name,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).ToList();
                return Ok(new { status = 200, message = "Success", levels = level1s });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateLevel1")]
        public async Task<IActionResult> UpdateLevel1([FromBody] Level1 lvl)
        {
            try
            {
                var level1 = await db.tblLevel1.FindAsync(lvl.intLevel1Id);
                if (level1 == null)
                    return NotFound(new { status = 202, message = "Not found" });

                level1.varLevel1Name = lvl.varLevel1Name;
                level1.dtUpdationDate = DateTime.Now;
                level1.intUpdatedBy = lvl.intUpdatedBy;
                db.Entry(level1).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteLevel1/{id}")]
        public async Task<IActionResult> DeleteLevel1(int id)
        {
            try
            {
                var level = await db.tblLevel1.FindAsync(id);
                if (level == null)
                    return NotFound(new { status = 202, message = "Not found" });

                db.tblLevel1.Remove(level);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getLevel1ById/{id}")]
        public IActionResult GetLevel1ById(int id)
        {
            try
            {
                var level = db.tblLevel1.Where(i => i.intLevel1Id == id).Select(l => new Level1
                {
                    intLevel1Id = l.intLevel1Id,
                    varLevel1Name = l.varLevel1Name,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).FirstOrDefault();
                return Ok(level);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion

        #region Level2
        [HttpPost, Route("addLevel2")]
        public async Task<IActionResult> AddLevel2([FromBody] Level2 lvl)
        {
            if (lvl == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblLevel2 level2 = new tblLevel2
                {
                    // intLevel2Id = await _sqlService.GetMaxId("tblLevel2", "intLevel2Id"), // Uncomment if you need to set PK manually
                    varLevel2Name = lvl.varLevel2Name,
                    dtCreationDate = DateTime.Now,
                    intCreatedBy = lvl.intCreatedBy,
                    intLevel1Id = lvl.intLevel1Id
                };
                db.tblLevel2.Add(level2);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error" });
            }
        }

        [HttpGet, Route("getLevel2")]
        public async Task<IActionResult> GetLevel2()
        {
            try
            {
                var level2s = await _sqlService.GetLevel2();
                return Ok(new { status = 200, message = "Success", levels = level2s });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateLevel2")]
        public async Task<IActionResult> UpdateLevel2([FromBody] Level2 lvl)
        {
            try
            {
                var level2 = await db.tblLevel2.FindAsync(lvl.intLevel2Id);
                if (level2 == null)
                    return NotFound(new { status = 202, message = "Not found" });

                level2.varLevel2Name = lvl.varLevel2Name;
                level2.dtUpdationDate = DateTime.Now;
                level2.intUpdatedBy = lvl.intUpdatedBy;
                level2.intLevel1Id = lvl.intLevel1Id;
                db.Entry(level2).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteLevel2/{id}")]
        public async Task<IActionResult> DeleteLevel2(int id)
        {
            try
            {
                var level = await db.tblLevel2.FindAsync(id);
                if (level == null)
                    return NotFound(new { status = 202, message = "Not found" });

                db.tblLevel2.Remove(level);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getLevel2ById/{id}")]
        public async Task<IActionResult> GetLevel2ById(int id)
        {
            try
            {
                var level = await _sqlService.GetLevel2ById(id);
                return Ok(level);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        #endregion

        #region Level3
        [HttpPost, Route("addLevel3")]
        public async Task<IActionResult> AddLevel3([FromBody] Level3 lvl)
        {
            if (lvl == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblLevel3 level3 = new tblLevel3
                {
                    // intLevel3Id = await _sqlService.GetMaxId("tblLevel3", "intLevel3Id"), // Uncomment if you need to set PK manually
                    varLevel3Name = lvl.varLevel3Name,
                    dtCreationDate = DateTime.Now,
                    intCreatedBy = lvl.intCreatedBy,
                    intLevel2Id = lvl.intLevel2Id
                };
                db.tblLevel3.Add(level3);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error" });
            }
        }

        [HttpGet, Route("getLevel3")]
        public async Task<IActionResult> GetLevel3()
        {
            try
            {
                var level3s = await _sqlService.GetLevel3();
                return Ok(new { status = 200, message = "Success", levels = level3s });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateLevel3")]
        public async Task<IActionResult> UpdateLevel3([FromBody] Level3 lvl)
        {
            try
            {
                var level3 = await db.tblLevel3.FindAsync(lvl.intLevel3Id);
                if (level3 == null)
                    return NotFound(new { status = 202, message = "Not found" });

                level3.varLevel3Name = lvl.varLevel3Name;
                level3.dtUpdationDate = DateTime.Now;
                level3.intUpdatedBy = lvl.intUpdatedBy;
                level3.intLevel2Id = lvl.intLevel2Id;
                db.Entry(level3).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteLevel3/{id}")]
        public async Task<IActionResult> DeleteLevel3(int id)
        {
            try
            {
                var level = await db.tblLevel3.FindAsync(id);
                if (level == null)
                    return NotFound(new { status = 202, message = "Not found" });

                db.tblLevel3.Remove(level);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getLevel3ById/{id}")]
        public async Task<IActionResult> GetLevel3ById(int id)
        {
            try
            {
                var level = await _sqlService.GetLevel3ById(id);
                return Ok(level);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        #endregion

        #region Item

        [HttpPost, Route("addItem")]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            if (item == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblItem tblItem = new tblItem
                {
                    varItemName = item.varItemName,
                    dcOrderLevel = item.dcOrderLevel,
                    dcMinLevel = item.dcMinLevel,
                    dcMaxLevel = item.dcMaxLevel,
                    dcOpenStock = item.dcOpenStock,
                    dtOpenDate = item.dtOpenDate,
                    dcPurRate = item.dcPurRate,
                    dcSellRate = item.dcSellRate,
                    dcRetailSaleRate = item.dcRetailSaleRate,
                    dcDistributorSaleRate = item.dcDistributorSaleRate,
                    dcDiscount = item.dcDiscount,
                    isActive = item.isActive,
                    isTaxable = item.isTaxable,
                    isExpirable = item.isExpirable,
                    dtExpiryDate = item.dtExpiryDate,
                    varUom = item.varUom,
                    intCompanyId = item.intCompanyId,
                    intCreatedBy = item.intCreatedBy,
                    dtCreationDate = DateTime.Now
                };
                db.tblItems.Add(tblItem);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error" });
            }
        }

        [HttpGet, Route("getItems")]
        public IActionResult GetItems()
        {
            try
            {
                var itemList = db.tblItems.Select(i => new Item
                {
                    intItemId = i.intItemId,
                    varItemName = i.varItemName,
                    dcOrderLevel = i.dcOrderLevel,
                    dcMinLevel = i.dcMinLevel,
                    dcMaxLevel = i.dcMaxLevel,
                    dcOpenStock = i.dcOpenStock,
                    dtOpenDate = i.dtOpenDate,
                    dcPurRate = i.dcPurRate,
                    dcSellRate = i.dcSellRate,
                    dcRetailSaleRate = i.dcRetailSaleRate,
                    dcDistributorSaleRate = i.dcDistributorSaleRate,
                    dcDiscount = i.dcDiscount,
                    isActive = i.isActive,
                    isTaxable = i.isTaxable,
                    isExpirable = i.isExpirable,
                    dtExpiryDate = i.dtExpiryDate,
                    varUom = i.varUom,
                    intCompanyId = i.intCompanyId,
                    intCreatedBy = i.intCreatedBy,
                    dtCreationDate = i.dtCreationDate,
                    dtUpdationDate = i.dtUpdationDate
                }).ToList();
                return Ok(new { status = 200, message = "Success", items = itemList });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] Item item)
        {
            try
            {
                var tblItem = await db.tblItems.FindAsync(item.intItemId);
                if (tblItem == null)
                    return NotFound(new { status = 202, message = "Not found" });

                tblItem.varItemName = item.varItemName;
                tblItem.dcOrderLevel = item.dcOrderLevel;
                tblItem.dcMinLevel = item.dcMinLevel;
                tblItem.dcMaxLevel = item.dcMaxLevel;
                tblItem.dcOpenStock = item.dcOpenStock;
                tblItem.dtOpenDate = item.dtOpenDate;
                tblItem.dcPurRate = item.dcPurRate;
                tblItem.dcSellRate = item.dcSellRate;
                tblItem.dcRetailSaleRate = item.dcRetailSaleRate;
                tblItem.dcDistributorSaleRate = item.dcDistributorSaleRate;
                tblItem.dcDiscount = item.dcDiscount;
                tblItem.isActive = item.isActive;
                tblItem.isTaxable = item.isTaxable;
                tblItem.isExpirable = item.isExpirable;
                tblItem.dtExpiryDate = item.dtExpiryDate;
                tblItem.varUom = item.varUom;
                tblItem.intCompanyId = item.intCompanyId;
                tblItem.intUpdatedBy = item.intUpdatedBy;
                tblItem.dtUpdationDate = DateTime.Now;
                db.Entry(tblItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await db.tblItems.FindAsync(id);
                if (item == null)
                    return NotFound(new { status = 202, message = "Not found" });

                db.tblItems.Remove(item);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getItemById/{id}")]
        public IActionResult GetItem(int id)
        {
            try
            {
                var item = db.tblItems.Where(i => i.intItemId == id).Select(i => new Item
                {
                    intItemId = i.intItemId,
                    varItemName = i.varItemName,
                    dcOrderLevel = i.dcOrderLevel,
                    dcMinLevel = i.dcMinLevel,
                    dcMaxLevel = i.dcMaxLevel,
                    dcOpenStock = i.dcOpenStock,
                    dtOpenDate = i.dtOpenDate,
                    dcPurRate = i.dcPurRate,
                    dcSellRate = i.dcSellRate,
                    dcRetailSaleRate = i.dcRetailSaleRate,
                    dcDistributorSaleRate = i.dcDistributorSaleRate,
                    dcDiscount = i.dcDiscount,
                    isActive = i.isActive,
                    isTaxable = i.isTaxable,
                    isExpirable = i.isExpirable,
                    dtExpiryDate = i.dtExpiryDate,
                    varUom = i.varUom,
                    intCompanyId = i.intCompanyId,
                    intCreatedBy = i.intCreatedBy,
                    dtCreationDate = i.dtCreationDate,
                    dtUpdationDate = i.dtUpdationDate
                }).FirstOrDefault();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion

        #region Warehouse

        [HttpPost, Route("addWarehouse")]
        public async Task<IActionResult> AddWarehouse([FromBody] Warehouse wrhouse)
        {
            if (wrhouse == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblWarehouse warehouse = new tblWarehouse
                {
                    varWarehouseName = wrhouse.varWarehouseName,
                    dtCreationDate = DateTime.Now,
                    intCreatedBy = wrhouse.intCreatedBy,
                    intCompanyId = wrhouse.intCompanyId
                };
                db.tblWarehouses.Add(warehouse);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error" });
            }
        }

        [HttpGet, Route("getWarehouse")]
        public IActionResult GetWarehouse()
        {
            try
            {
                var wrhouse = db.tblWarehouses.Select(l => new Warehouse
                {
                    intWarehouseId = l.intWarehouseId,
                    varWarehouseName = l.varWarehouseName,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate,
                    intUpdatedBy = l.intUpdatedBy
                }).ToList();
                return Ok(new { status = 200, message = "Success", warehouses = wrhouse });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateWarehouse")]
        public async Task<IActionResult> UpdateWarehouse([FromBody] Warehouse wrhouse)
        {
            try
            {
                var warehouse = await db.tblWarehouses.FindAsync(wrhouse.intWarehouseId);
                if (warehouse == null)
                    return NotFound(new { status = 202, message = "Not found" });

                warehouse.varWarehouseName = wrhouse.varWarehouseName;
                warehouse.dtUpdationDate = DateTime.Now;
                warehouse.intUpdatedBy = wrhouse.intUpdatedBy;
                db.Entry(warehouse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteWarehouse/{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            try
            {
                var warehouse = await db.tblWarehouses.FindAsync(id);
                if (warehouse == null)
                    return NotFound(new { status = 202, message = "Not found" });

                db.tblWarehouses.Remove(warehouse);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getWarehouseById/{id}")]
        public IActionResult GetWarehouseById(int id)
        {
            try
            {
                var warehouse = db.tblWarehouses.Where(i => i.intWarehouseId == id).Select(l => new Warehouse
                {
                    intWarehouseId = l.intWarehouseId,
                    varWarehouseName = l.varWarehouseName,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).FirstOrDefault();
                return Ok(warehouse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion

        #region Transporter

        [HttpPost, Route("addTransporter")]
        public async Task<IActionResult> AddTransporter([FromBody] Transporter transporter)
        {
            if (transporter == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblTransporter tblTransporter = new tblTransporter
                {
                    varTransporterName = transporter.varTransporterName,
                    varContactNo = transporter.varContactNo,
                    varEmail = transporter.varEmail,
                    varAddress = transporter.varAddress,
                    dtCreationDate = DateTime.Now,
                    intCreatedBy = transporter.intCreatedBy,
                    intCompanyId = transporter.intCompanyId
                };
                db.tblTransporters.Add(tblTransporter);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error" });
            }
        }

        [HttpGet, Route("getTransporter")]
        public IActionResult GetTransporter()
        {
            try
            {
                var transporters = db.tblTransporters.Select(l => new Transporter
                {
                    intTransporterId = l.intTransporterId,
                    varTransporterName = l.varTransporterName,
                    varContactNo = l.varContactNo,
                    varEmail = l.varEmail,
                    varAddress = l.varAddress,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate,
                    intUpdatedBy = l.intUpdatedBy
                }).ToList();
                return Ok(new { status = 200, message = "Success", transporter = transporters });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateTransporter")]
        public async Task<IActionResult> UpdateTransporter([FromBody] Transporter transporter)
        {
            try
            {
                var tblTransporter = await db.tblTransporters.FindAsync(transporter.intTransporterId);
                if (tblTransporter == null)
                    return NotFound(new { status = 202, message = "Not found" });

                tblTransporter.varTransporterName = transporter.varTransporterName;
                tblTransporter.varContactNo = transporter.varContactNo;
                tblTransporter.varEmail = transporter.varEmail;
                tblTransporter.varAddress = transporter.varAddress;
                tblTransporter.dtUpdationDate = DateTime.Now;
                tblTransporter.intUpdatedBy = transporter.intUpdatedBy;
                db.Entry(tblTransporter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteTransporter/{id}")]
        public async Task<IActionResult> DeleteTransporter(int id)
        {
            try
            {
                var transporter = await db.tblTransporters.FindAsync(id);
                if (transporter == null)
                    return NotFound(new { status = 202, message = "Not found" });

                db.tblTransporters.Remove(transporter);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getTransporterById/{id}")]
        public IActionResult GetTransporterById(int id)
        {
            try
            {
                var transporter = db.tblTransporters.Where(i => i.intTransporterId == id).Select(l => new Transporter
                {
                    intTransporterId = l.intTransporterId,
                    varTransporterName = l.varTransporterName,
                    varContactNo = l.varContactNo,
                    varEmail = l.varEmail,
                    varAddress = l.varAddress,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).FirstOrDefault();
                return Ok(transporter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion

        #region User

        [HttpGet, Route("getUser")]
        public IActionResult GetUser()
        {
            try
            {
                var users = db.tblUsers.ToList()
                    .Select(u => new User
                    {
                        intUserId = u.intUserId,
                        varName = u.varName,
                        varEmail = u.varEmail,
                        varAddress = u.varAddress,
                        varPassword = u.varPassword,
                        varCnic = u.varCnic,
                        varContactNo = u.varContactNo,
                        intCompanyId = u.intCompanyId,
                        dtCreationDate = u.dtCreationDate,
                        dtUpdationDate = u.dtUpdationDate,
                        //intCreatedBy = u.intCreatedBy,
                        //intUpdatedBy = u.intUpdatedBy,
                        varPhoto = u.varPhoto != null ? Convert.ToBase64String(u.varPhoto) : null
                    }).ToList();
                return Ok(new { status = 200, message = "Success", users });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet, Route("getUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var userEntity = db.tblUsers.FirstOrDefault(u => u.intUserId == id);

                if (userEntity == null)
                    return NotFound(new { status = 202, message = "Not Found" });

                var user = new User
                {
                    intUserId = userEntity.intUserId,
                    varName = userEntity.varName,
                    varEmail = userEntity.varEmail,
                    varAddress = userEntity.varAddress,
                    varPassword = userEntity.varPassword,
                    varCnic = userEntity.varCnic,
                    varContactNo = userEntity.varContactNo,
                    intCompanyId = userEntity.intCompanyId,
                    dtCreationDate = userEntity.dtCreationDate,
                    dtUpdationDate = userEntity.dtUpdationDate,
                    //intCreatedBy = userEntity.intCreatedBy,
                    //intUpdatedBy = userEntity.intUpdatedBy,
                    varPhoto = userEntity.varPhoto != null ? Convert.ToBase64String(userEntity.varPhoto) : null
                };
                return Ok(new { status = 200, message = "Success", user });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost, Route("addUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { status = 400, message = "Bad Request" });

            try
            {
                tblUser tblUser = new tblUser
                {
                    varName = user.varName,
                    varEmail = user.varEmail,
                    varAddress = user.varAddress,
                    varPassword = user.varPassword,
                    varCnic = user.varCnic,
                    varContactNo = user.varContactNo,
                    intCompanyId = user.intCompanyId,
                    dtCreationDate = DateTime.Now,
                    //intCreatedBy = user.intCreatedBy,
                    varPhoto = !string.IsNullOrEmpty(user.varPhoto) ? Convert.FromBase64String(user.varPhoto) : null
                };

                db.tblUsers.Add(tblUser);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut, Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var tblUser = await db.tblUsers.FindAsync(user.intUserId);
                if (tblUser == null)
                    return NotFound(new { status = 202, message = "Not Found" });

                tblUser.varName = user.varName;
                tblUser.varEmail = user.varEmail;
                tblUser.varAddress = user.varAddress;
                tblUser.varPassword = user.varPassword;
                tblUser.varCnic = user.varCnic;
                tblUser.varContactNo = user.varContactNo;
                tblUser.intCompanyId = user.intCompanyId;
                tblUser.dtUpdationDate = DateTime.Now;
                //tblUser.intUpdatedBy = user.intUpdatedBy;

                if (!string.IsNullOrEmpty(user.varPhoto))
                {
                    tblUser.varPhoto = Convert.FromBase64String(user.varPhoto);
                }
                else
                {
                    tblUser.varPhoto = null;
                }

                    db.Entry(tblUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete, Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await db.tblUsers.FindAsync(id);
                if (user == null)
                    return NotFound(new { status = 202, message = "Not Found" });

                db.tblUsers.Remove(user);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion

    }
}
