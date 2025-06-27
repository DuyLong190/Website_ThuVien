using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanPhucLongQuang_DoAnWeb.Models;
using System.Threading.Tasks;
using System.Linq;
using QuanPhucLongQuang_DoAnWeb.Areas.Admin.Models;
using QuanPhucLongQuang_DoAnWeb.Areas.Admin.ViewModels;

namespace QuanPhucLongQuang_DoAnWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                TempData["UserError"] = "Không tìm thấy người dùng. Id không hợp lệ.";
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["UserError"] = "Không tìm thấy người dùng. Có thể tài khoản đã bị xóa.";
                return RedirectToAction("Index");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var firstUser = _userManager.Users.OrderBy(u => u.Id).FirstOrDefault();
            bool isFirstAdmin = firstUser != null && firstUser.Id == user.Id;
            if (isFirstAdmin && user.Id != _userManager.GetUserId(User))
            {
                TempData["UserError"] = "Chỉ chủ tài khoản admin đầu tiên mới có quyền sửa thông tin của mình.";
                return RedirectToAction("Index");
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                Age = user.Age,
                Role = userRoles.FirstOrDefault()
            };
            ViewBag.RoleList = new SelectList(new[] { SD.Role_Admin, SD.Role_Employee, SD.Role_Company, SD.Role_Customer }, model.Role);
            ViewBag.DisableRole = isFirstAdmin;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (id != model.Id)
            {
                TempData["UserError"] = "Id không hợp lệ.";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    TempData["UserError"] = "Không tìm thấy người dùng. Có thể tài khoản đã bị xóa.";
                    return RedirectToAction("Index");
                }
                var firstUser = _userManager.Users.OrderBy(u => u.Id).FirstOrDefault();
                bool isFirstAdmin = firstUser != null && firstUser.Id == user.Id;
                if (isFirstAdmin && user.Id != _userManager.GetUserId(User))
                {
                    TempData["UserError"] = "Chỉ chủ tài khoản admin đầu tiên mới có quyền sửa thông tin của mình.";
                    return RedirectToAction("Index");
                }
                user.FullName = model.FullName;
                user.Address = model.Address;
                user.Age = model.Age;
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (!isFirstAdmin)
                    {
                        var currentRoles = await _userManager.GetRolesAsync(user);
                        if (currentRoles.Count > 0)
                            await _userManager.RemoveFromRolesAsync(user, currentRoles);
                        if (!string.IsNullOrEmpty(model.Role))
                            await _userManager.AddToRoleAsync(user, model.Role);
                    }
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            ViewBag.RoleList = new SelectList(new[] { SD.Role_Admin, SD.Role_Employee, SD.Role_Company, SD.Role_Customer }, model.Role);
            var firstUserCheck = _userManager.Users.OrderBy(u => u.Id).FirstOrDefault();
            ViewBag.DisableRole = firstUserCheck != null && firstUserCheck.Id == model.Id;
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                TempData["UserError"] = "Không tìm thấy người dùng. Id không hợp lệ.";
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["UserError"] = "Không tìm thấy người dùng. Có thể tài khoản đã bị xóa.";
                return RedirectToAction("Index");
            }
            var firstUser = _userManager.Users.OrderBy(u => u.Id).FirstOrDefault();
            if (firstUser != null && firstUser.Id == user.Id)
            {
                TempData["UserError"] = "Không thể xóa tài khoản admin đầu tiên của hệ thống.";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["UserError"] = "Không tìm thấy người dùng. Có thể tài khoản đã bị xóa.";
                return RedirectToAction("Index");
            }
            var firstUser = _userManager.Users.OrderBy(u => u.Id).FirstOrDefault();
            if (firstUser != null && firstUser.Id == user.Id)
            {
                TempData["UserError"] = "Không thể xóa tài khoản admin đầu tiên của hệ thống.";
                return RedirectToAction("Index");
            }
            var result = await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        // Có thể thêm các action như Edit, Delete, Details ở đây
    }
} 