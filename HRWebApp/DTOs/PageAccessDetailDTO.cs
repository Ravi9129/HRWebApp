﻿namespace HRWebApp.DTOs
{
    public class PageAccessDetailDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string PageName { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
