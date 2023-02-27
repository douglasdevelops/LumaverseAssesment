using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeLocatorFrontEnd.Views.ViewModels
{
    public class EngineerViewModel
    {
        public DropdownModel Locations = new DropdownModel();
        public DropdownModel EmployeeStatus = new DropdownModel();
        public Engineer Engineer = new Engineer();
    }

    public class DropdownModel
    {
        public List<SelectListItem> Options { get; set; } = new List<SelectListItem>();
        public string SelectedValue { get; set; } = String.Empty;
    }
}
