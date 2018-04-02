using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                
                context.Menus.Add(newMenu);
                context.SaveChanges();
                
                return Redirect("/Menu");
            }
            return View(addMenuViewModel);
        }

        [HttpGet]
        [Route("Menu/ViewMenu/{id}")]
        public IActionResult ViewMenu(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };
            return View(viewMenuViewModel);
        }

        [HttpGet]
        [Route("Menu/AddItem/{id}")]
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            AddMenuItemViewModel amivm =
                new AddMenuItemViewModel(
                    menu,
                    context.Cheeses.ToList()
                );
            return View(amivm);
        }

        [HttpPost]
        [Route("Menu/AddItem/{id}")]
        public IActionResult AddItem(AddMenuItemViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CheeseMenu newCheeseMenu;
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == vm.cheeseID)
                    .Where(cm => cm.MenuID == vm.menuID).ToList();
                if (existingItems.Count == 0)
                {
                    newCheeseMenu = new CheeseMenu {
                            MenuID = vm.menuID,
                            CheeseID = vm.cheeseID
                        };
                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();
                }
                return Redirect("/Menu/ViewMenu/" + vm.menuID.ToString());
            }
            return View(vm);
        }
    }
}