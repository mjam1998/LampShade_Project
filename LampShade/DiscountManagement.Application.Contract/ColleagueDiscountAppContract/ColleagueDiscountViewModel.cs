﻿



namespace DiscountManagement.Application.Contract.ColleagueDiscountAppContract
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int DiscountRate { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }


    }
}
