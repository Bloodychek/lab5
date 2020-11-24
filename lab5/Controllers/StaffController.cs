﻿using lab5.Infrastructure;
using lab5.Models;
using lab5.Services;
using lab5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Controllers
{
    public class StaffController : Controller
    {
        Petrol_StationContext context;
        private readonly CacheProvider cache;
        private const string filterKey = "fullName";
        public StaffController(Petrol_StationContext context, CacheProvider cacheProvider)
        {
            this.context = context;
            cache = cacheProvider;

        }

        public IActionResult Index(SortState sortState = SortState.FullNameAsc, int page = 1)
        {
            StaffFilterViewModel filter = HttpContext.Session.Get<StaffFilterViewModel>(filterKey);
            if (filter == null)
            {
                filter = new StaffFilterViewModel { FullName = string.Empty, StaffAge = 0, StaffFunction = string.Empty, WorkingHoursForAweek = default };
                HttpContext.Session.Set(filterKey, filter);
            }

            string modelKey = $"{typeof(Staff).Name}-{page}-{sortState}-{filter.FullName}-{filter.StaffAge}-{filter.StaffFunction}-{filter.WorkingHoursForAweek}";
            if (!cache.TryGetValue(modelKey, out StaffViewModel model))
            {
                model = new StaffViewModel();

                IQueryable<Staff> staff = GetSortedEntities(sortState, filter.FullName, filter.StaffAge, filter.StaffFunction, filter.WorkingHoursForAweek);

                int count = staff.Count();
                int pageSize = 10;
                model.PageViewModel = new PageViewModel(count, page, pageSize);

                model.Staffs = count == 0 ? new List<Staff>() : staff.Skip((model.PageViewModel.PageNumber - 1) * pageSize).Take(pageSize).ToList();
                model.SortViewModel = new SortViewModel(sortState);
                model.StaffFilterViewModel = filter;

                cache.Set(modelKey, model);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(StaffFilterViewModel filterModel, int page)
        {
            StaffFilterViewModel filter = HttpContext.Session.Get<StaffFilterViewModel>(filterKey);
            if (filter != null)
            {
                filter.FullName = filterModel.FullName;
                filter.StaffAge = filterModel.StaffAge;
                filter.StaffFunction = filterModel.StaffFunction;
                filter.WorkingHoursForAweek = filterModel.WorkingHoursForAweek;

                HttpContext.Session.Remove(filterKey);
                HttpContext.Session.Set(filterKey, filter);
            }

            return RedirectToAction("Index", new { page });
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create(int page)
        {
            StaffViewModel StaffViewModel = new StaffViewModel();
            StaffViewModel.PageViewModel = new PageViewModel { PageNumber = page };

            return View(StaffViewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(StaffViewModel StaffViewModel)
        {
            if (ModelState.IsValid)
            {
                await context.Staff.AddAsync(StaffViewModel.Staff);
                await context.SaveChangesAsync();
                cache.Clean();
                return RedirectToAction("Index", "Staff");
            }

            return View(StaffViewModel);
        }

        public async Task<IActionResult> Edit(int id, int page)
        {
            Staff staff = await context.Staff.FindAsync(id);
            if (staff != null)
            {
                StaffViewModel StaffViewModel = new StaffViewModel();
                StaffViewModel.PageViewModel = new PageViewModel { PageNumber = page };
                StaffViewModel.Staff = staff;

                return View(StaffViewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StaffViewModel StaffViewModel)
        {
            if (ModelState.IsValid)
            {
                Staff staff = context.Staff.Find(StaffViewModel.Staff.StaffId);
                if (staff != null)
                {
                    staff.FullName = StaffViewModel.Staff.FullName;
                    staff.StaffAge = StaffViewModel.Staff.StaffAge;
                    staff.StaffFunction = StaffViewModel.Staff.StaffFunction;
                    staff.WorkingHoursForAweek = StaffViewModel.Staff.WorkingHoursForAweek;

                    context.Staff.Update(staff);
                    await context.SaveChangesAsync();
                    cache.Clean();
                    return RedirectToAction("Index", "Staff", new { page = StaffViewModel.PageViewModel.PageNumber });
                }
                else
                {
                    return NotFound();
                }
            }

            return View(StaffViewModel);
        }

        public async Task<IActionResult> Delete(int id, int page)
        {
            Staff staff = await context.Staff.FindAsync(id);
            if (staff == null)
                return NotFound();

            bool deleteFlag = false;
            string message = "Do you want to delete this entity";

            if (context.Containers.Any(s => s.TypeOfGsmid == staff.StaffId) && context.Costs.Any(s => s.TypeOfGsmid == staff.StaffId))
                message = "This entity has entities, which dependents from this. Do you want to delete this entity and other, which dependents from this?";

            StaffViewModel StaffViewModel = new StaffViewModel();
            StaffViewModel.Staff = staff;
            StaffViewModel.PageViewModel = new PageViewModel { PageNumber = page };
            StaffViewModel.DeleteViewModel = new DeleteViewModels { Message = message, IsDeleted = deleteFlag };

            return View(StaffViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StaffViewModel StaffViewModel)
        {
            bool deleteFlag = true;
            string message = "Do you want to delete this entity";
            Staff staff = await context.Staff.FindAsync(StaffViewModel.Staff.StaffId);
            StaffViewModel.DeleteViewModel = new DeleteViewModels { Message = message, IsDeleted = deleteFlag };
            if (staff == null)
                return NotFound();

            context.Staff.Remove(staff);
            await context.SaveChangesAsync();
            cache.Clean();
            return View(StaffViewModel);
        }

        private IQueryable<Staff> GetSortedEntities(SortState sortState, string fullName, int staffAge, string staffFunction, DateTime workingHoursForAweek)
        {
            IQueryable<Staff> staff = context.Staff.AsQueryable();

            switch (sortState)
            {
                case SortState.FullNameAsc:
                    staff = staff.OrderBy(g => g.FullName);
                    break;
                case SortState.FullNameDesc:
                    staff = staff.OrderByDescending(g => g.FullName);
                    break;
                case SortState.StaffAgeAsc:
                    staff = staff.OrderBy(g => g.StaffAge);
                    break;
                case SortState.StaffAgeDesc:
                    staff = staff.OrderByDescending(g => g.StaffAge);
                    break;
                case SortState.StaffFunctionAsc:
                    staff = staff.OrderBy(g => g.StaffFunction);
                    break;
                case SortState.StaffFunctionDesc:
                    staff = staff.OrderByDescending(g => g.StaffFunction);
                    break;
                case SortState.WorkingHoursForAweekAsc:
                    staff = staff.OrderBy(g => g.WorkingHoursForAweek);
                    break;
                case SortState.WorkingHoursForAweekDesc:
                    staff = staff.OrderByDescending(g => g.WorkingHoursForAweek);
                    break;
            }

            if (!string.IsNullOrEmpty(fullName))
                staff = staff.Where(g => g.FullName.Contains(fullName)).AsQueryable();
            if ( staffAge != 0)
                staff = staff.Where(g => g.StaffAge == staffAge).AsQueryable();
            if (!string.IsNullOrEmpty(staffFunction))
                staff = staff.Where(g => g.StaffFunction.Contains(staffFunction)).AsQueryable();
            if (workingHoursForAweek != default)
                staff = staff.Where(g => g.WorkingHoursForAweek.Date == workingHoursForAweek.Date).AsQueryable();

            return staff;
        }
    }
}
