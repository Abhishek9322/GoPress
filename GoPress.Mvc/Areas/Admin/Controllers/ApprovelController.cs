using GoPress.Mvc.Areas.Admin.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApprovelController : Controller
    {
        private readonly ApiService _apiService;    
        public ApprovelController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingShopOwners()
        {
            var response = await _apiService.GetAsync<Response<List<PendingShopOwnerViewModel>>>
                (
                   "api/AdminApprovel/pending-shopowners"
                );

            if(response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load pending shop owners.";
                return View(new List<PendingShopOwnerViewModel>());
            }
            return View(response.Data);
        } 

        [HttpGet]
        public async Task<IActionResult> GetAllPendingDeliveryBoy()
        {
            var response = await _apiService.GetAsync<Response<List<PendingDeliveryBoyViewModel>>>
                (
                   "api/AdminApprovel/pending-deliveryboys"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load pending Delivery Boy.";
                return View(new List<PendingDeliveryBoyViewModel>());
            }
                return View(response.Data);  
        }


        

        //Approve or reject here and make cancel approval of the user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveUser(int id)
        {
            try
            {
                var response = await _apiService.PutAsync<Response<string>>(
                    $"api/AdminApprovel/{id}/approve"
                    );

                if (response == null || !response.Succeeded)
                {
                    TempData["Error"] = "Uable Get All Approved users. ";
                    return RedirectToAction(nameof(GetAllApprovedUsers));

                }

                TempData["Success"] = response.Message ?? "All Approved Users";
                return RedirectToAction(nameof(GetAllApprovedUsers));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(GetAllPendingDeliveryBoy));
            }
        }


        [HttpGet]
        public async Task<IActionResult>GetAllApprovedUsers()
        {
            var response = await _apiService.GetAsync<Response<List<AllApprovedUsersViewModel>>>
               (
               "api/AdminApprovel/All-Approvedusers"
               );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load All Approved Users.";
                return View(new List<AllApprovedUsersViewModel>());
            }

            return View(response.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllApprovedShopOwner()
        {
            var response = await _apiService.GetAsync<Response<List<GetAllApprovedShopOwnerViewModel>>>
                (
                "api/AdminApprovel/Approed-shopowner"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load Approved shop owners.";
                return View(new List<GetAllApprovedShopOwnerViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApprovedDeliveryBoy()
        {
            var response = await _apiService.GetAsync<Response<List<GetAllApprovedDeliveryBoyViewModel>>>
                (
                "api/AdminApprovel/Approved-DeliveryBoy"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load Approved Delivery Boy.";
                return View(new List<GetAllApprovedDeliveryBoyViewModel>());
            }

            return View(response.Data);
        }
    }
}
