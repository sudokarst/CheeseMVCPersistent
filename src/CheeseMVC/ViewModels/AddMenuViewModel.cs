﻿using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuViewModel
    {
        [Required(ErrorMessage = "Menu name cannot be empty")]
        [Display(Name = "Menu")]
        public string Name { get; set; }
    }
}
