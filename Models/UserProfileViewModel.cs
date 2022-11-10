using StlBackend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StlBackend.ViewModels
{
    public class UserProfileViewModel
    {

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string ProfileImage { get; set; }

    }
}
