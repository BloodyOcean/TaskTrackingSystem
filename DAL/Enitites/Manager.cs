﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Enitites
{
    public class Manager : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public int ProjectId { get; set; }

        // Navigation properties.
        //public Project Project { get; set; }
    }
}