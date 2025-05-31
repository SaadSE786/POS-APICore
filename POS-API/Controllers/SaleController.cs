using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.BusinessObjects;
using POS_API.Model;
using POS_API.Models;
using POS_API.Services;

namespace POS_API.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly POSEntities db;
        private readonly SQLService _sqlService;

        public SaleController(DbContextOptions<POSEntities> options, SQLService sqlService)
        {
            db = new POSEntities(options);
            _sqlService = sqlService;
        }

        #region Sale

        [HttpPost, Route("posvrnos")]
        public async Task<IActionResult> GetVrNo([FromBody] DateRequest request)
        {
            try
            {
                int[] vrnos = new int[2];
                DateTime date = request.date;
                vrnos[0] = await _sqlService.GetMaxId("tblStockMains", "intVrno", "Cash_Sale", date);
                vrnos[1] = await _sqlService.GetMaxId("tblStockMains", "intVrnoa", "Cash_Sale", date);
                return Ok(new { status = 200, message = "Sale added successfully", data = vrnos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpPost, Route("addSale")]
        public async Task<IActionResult> AddSale([FromBody] StockMain sale)
        {
            if (sale == null)
                return BadRequest(new { status = 400, message = "Invalid sale data" });

            try
            {
                tblStockMain stockMain = new tblStockMain
                {
                    intCompanyId = sale.intCompanyId,
                    intPartyId = sale.intPartyId,
                    intVrno = sale.intVrno,
                    intVrnoA = sale.intVrnoA,
                    dtVrDate = sale.dtVrDate,
                    varRemarks = sale.varRemarks,
                    intTransporterId = sale.intTransporterId,
                    varVrType = sale.varVrType,
                    dcDiscount = sale.dcDiscount,
                    dcDiscountAmount = sale.dcDiscountAmount,
                    dcExpense = sale.dcExpense,
                    dcAdditionalCharges = sale.dcAdditionalCharges,
                    dcNetAmount = sale.dcNetAmount,
                    dcTotalAmount = sale.dcTotalAmount,
                    dtCreationDate = DateTime.Now,
                    intCreatedBy = sale.intCreatedBy
                };

                db.tblStockMains.Add(stockMain);
                await db.SaveChangesAsync();

                if (sale.stockDetails != null)
                {
                    foreach (var detail in sale.stockDetails)
                    {
                        tblStockDetail stockDetail = new tblStockDetail
                        {
                            intStid = stockMain.intStid,
                            intItemId = detail.intItemId,
                            intWarehouseId = detail.intWarehouseId,
                            intQuantity = detail.intQuantity,
                            dcRate = detail.dcRate,
                            dcAmount = detail.dcAmount,
                            dcDisc = detail.dcDisc,
                            dcDiscAmount = detail.dcDiscAmount,
                            dcExclTaxAmount = detail.dcExclTaxAmount,
                            dcTax = detail.dcTax,
                            dcTaxAmount = detail.dcTaxAmount,
                            dcInclTaxAmount = detail.dcInclTaxAmount,
                            varType = detail.varType,
                            dcPurRate = detail.dcPurRate,
                            dtCreationDate = DateTime.Now,
                            intCreatedBy = detail.intCreatedBy
                        };

                        db.tblStockDetails.Add(stockDetail);
                    }
                    await db.SaveChangesAsync();
                }

                return Ok(new { status = 200, message = "Sale added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpPut, Route("updateSale")]
        public async Task<IActionResult> UpdateSale([FromBody] StockMain sale)
        {
            try
            {
                var stockMain = await db.tblStockMains.FindAsync(sale.intStid);
                if (stockMain == null)
                    return NotFound(new { status = 404, message = "Sale not found" });

                stockMain.intCompanyId = sale.intCompanyId;
                stockMain.intPartyId = sale.intPartyId;
                stockMain.intVrno = sale.intVrno;
                stockMain.intVrnoA = sale.intVrnoA;
                stockMain.dtVrDate = sale.dtVrDate;
                stockMain.varRemarks = sale.varRemarks;
                stockMain.intTransporterId = sale.intTransporterId;
                stockMain.varVrType = sale.varVrType;
                stockMain.dcDiscount = sale.dcDiscount;
                stockMain.dcDiscountAmount = sale.dcDiscountAmount;
                stockMain.dcExpense = sale.dcExpense;
                stockMain.dcAdditionalCharges = sale.dcAdditionalCharges;
                stockMain.dcNetAmount = sale.dcNetAmount;
                stockMain.dtUpdationDate = DateTime.Now;
                stockMain.intUpdatedBy = sale.intUpdatedBy;
                stockMain.dcTotalAmount = sale.dcTotalAmount;

                db.Entry(stockMain).State = EntityState.Modified;

                var existingDetails = db.tblStockDetails.Where(d => d.intStid == sale.intStid).ToList();
                db.tblStockDetails.RemoveRange(existingDetails);

                if (sale.stockDetails != null)
                {
                    foreach (var detail in sale.stockDetails)
                    {
                        tblStockDetail stockDetail = new tblStockDetail
                        {
                            intStid = stockMain.intStid,
                            intItemId = detail.intItemId,
                            intWarehouseId = detail.intWarehouseId,
                            intQuantity = detail.intQuantity,
                            dcRate = detail.dcRate,
                            dcAmount = detail.dcAmount,
                            dcDisc = detail.dcDisc,
                            dcDiscAmount = detail.dcDiscAmount,
                            dcExclTaxAmount = detail.dcExclTaxAmount,
                            dcTax = detail.dcTax,
                            dcTaxAmount = detail.dcTaxAmount,
                            dcInclTaxAmount = detail.dcInclTaxAmount,
                            varType = detail.varType,
                            dcPurRate = detail.dcPurRate,
                            dtCreationDate = DateTime.Now,
                            intCreatedBy = detail.intCreatedBy
                        };

                        db.tblStockDetails.Add(stockDetail);
                    }
                }

                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Sale updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpDelete, Route("deleteSale/{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                var stockMain = await db.tblStockMains.FindAsync(id);
                if (stockMain == null)
                    return NotFound(new { status = 404, message = "Sale not found" });

                var stockDetails = db.tblStockDetails.Where(d => d.intStid == id).ToList();
                db.tblStockDetails.RemoveRange(stockDetails);
                db.tblStockMains.Remove(stockMain);

                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Sale deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet, Route("getSales")]
        public IActionResult GetSales()
        {
            try
            {
                var sales = (from s in db.tblStockMains
                             select new StockMain
                             {
                                 intStid = s.intStid,
                                 intCompanyId = s.intCompanyId,
                                 intPartyId = s.intPartyId,
                                 intVrno = s.intVrno,
                                 intVrnoA = s.intVrnoA,
                                 dtVrDate = s.dtVrDate,
                                 varRemarks = s.varRemarks,
                                 intTransporterId = s.intTransporterId,
                                 varVrType = s.varVrType,
                                 dcDiscount = s.dcDiscount,
                                 dcDiscountAmount = s.dcDiscountAmount,
                                 dcExpense = s.dcExpense,
                                 dcAdditionalCharges = s.dcAdditionalCharges,
                                 dcNetAmount = s.dcNetAmount,
                                 dtCreationDate = s.dtCreationDate,
                                 dtUpdationDate = s.dtUpdationDate,
                                 intCreatedBy = s.intCreatedBy,
                                 intUpdatedBy = s.intUpdatedBy,
                                 dcTotalAmount = s.dcTotalAmount,
                                 stockDetails = (from d in db.tblStockDetails
                                                 join i in db.tblItems on d.intItemId equals i.intItemId into items
                                                 from item in items.DefaultIfEmpty()
                                                 where d.intStid == s.intStid
                                                 select new StockDetail
                                                 {
                                                     intStockDetailId = d.intStockDetailId,
                                                     intStid = d.intStid,
                                                     intItemId = d.intItemId,
                                                     varItemName = item.varItemName,
                                                     intWarehouseId = d.intWarehouseId,
                                                     intQuantity = d.intQuantity,
                                                     dcRate = d.dcRate,
                                                     dcAmount = d.dcAmount,
                                                     dcDisc = d.dcDisc,
                                                     dcDiscAmount = d.dcDiscAmount,
                                                     dcExclTaxAmount = d.dcExclTaxAmount,
                                                     dcTax = d.dcTax,
                                                     dcTaxAmount = d.dcTaxAmount,
                                                     dcInclTaxAmount = d.dcInclTaxAmount,
                                                     varType = d.varType,
                                                     dcPurRate = d.dcPurRate,
                                                     dtCreationDate = d.dtCreationDate,
                                                     dtUpdationDate = d.dtUpdationDate,
                                                     intCreatedBy = d.intCreatedBy,
                                                     intUpdatedBy = d.intUpdatedBy
                                                 }).ToList()
                             }).ToList();
                
                return Ok(new { status = 200, message = "Success", sales });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet, Route("getSaleById/{id}")]
        public IActionResult GetSaleById(int id)
        {
            try
            {
                var sale = db.tblStockMains.Where(s => s.intStid == id).Select(s => new StockMain
                {
                    intStid = s.intStid,
                    intCompanyId = s.intCompanyId,
                    intPartyId = s.intPartyId,
                    intVrno = s.intVrno,
                    intVrnoA = s.intVrnoA,
                    dtVrDate = s.dtVrDate,
                    varRemarks = s.varRemarks,
                    intTransporterId = s.intTransporterId,
                    varVrType = s.varVrType,
                    dcDiscount = s.dcDiscount,
                    dcDiscountAmount = s.dcDiscountAmount,
                    dcExpense = s.dcExpense,
                    dcAdditionalCharges = s.dcAdditionalCharges,
                    dcNetAmount = s.dcNetAmount,
                    dtCreationDate = s.dtCreationDate,
                    dtUpdationDate = s.dtUpdationDate,
                    intCreatedBy = s.intCreatedBy,
                    intUpdatedBy = s.intUpdatedBy,
                    dcTotalAmount = s.dcTotalAmount,
                    stockDetails = s.tblStockDetails.Select(d => new StockDetail
                    {
                        intStockDetailId = d.intStockDetailId,
                        intStid = d.intStid,
                        intItemId = d.intItemId,
                        intWarehouseId = d.intWarehouseId,
                        intQuantity = d.intQuantity,
                        dcRate = d.dcRate,
                        dcAmount = d.dcAmount,
                        dcDisc = d.dcDisc,
                        dcDiscAmount = d.dcDiscAmount,
                        dcExclTaxAmount = d.dcExclTaxAmount,
                        dcTax = d.dcTax,
                        dcTaxAmount = d.dcTaxAmount,
                        dcInclTaxAmount = d.dcInclTaxAmount,
                        varType = d.varType,
                        dcPurRate = d.dcPurRate,
                        dtCreationDate = d.dtCreationDate,
                        dtUpdationDate = d.dtUpdationDate,
                        intCreatedBy = d.intCreatedBy,
                        intUpdatedBy = d.intUpdatedBy
                    }).ToList()
                }).FirstOrDefault();

                if (sale == null)
                    return NotFound(new { status = 404, message = "Sale not found" });

                return Ok(new { status = 200, message = "Success", sale });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        #endregion
    }

    public class DateRequest
    {
        public DateTime date { get; set; }
    }
}
