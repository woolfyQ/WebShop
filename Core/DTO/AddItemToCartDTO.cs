﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class AddItemToCartDTO
    {
        public Guid CartId { get; set; }       
        public Guid ProductId { get; set; }    
        public int Quantity { get; set; }      
    }
}