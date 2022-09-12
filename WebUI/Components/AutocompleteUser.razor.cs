using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Components.Abstract;
using WebUI.Data;
using WebUI.Data.Interfaces;

namespace WebUI.Components
{
    /// <summary>
    /// Autocomplete component that loads a full set of string values for one entity type
    /// </summary>
    /// <typeparam name="TItem">the type of entity to load</typeparam>
    public partial class AutocompleteUser : CustomSelectorBase<ApplicationUser, ISelectOption<string>, string>
    {
        #region Convenience Properties
        private ISelectOption<string> _selectedOption => _options?.Where(o => o.OptionId == Value).FirstOrDefault();
        #endregion

        /// <summary>
        /// callback when the user picks a new option
        /// </summary>
        protected void SelectedOptionChanged(ISelectOption<string> newOption)
        {
            string newId = newOption?.OptionId;

            if (Value == newId) return;

            Value = newId;

            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(Value);
                if (DoChangeNotifications)
                {
                    EditContext?.NotifyFieldChanged(FieldIdentifier);
                }
            }
        }

        protected override async Task<IEnumerable<ISelectOption<string>>> LoadOptionsAsync()
        {
            var service = await _factory.CreateServiceBaseAsync(_authenticationStateProvider);
            return await service.GetSelectOptions<ApplicationUser, string>(Filter);
        }

        /// <summary>
        /// Get the display name of the selected option, or null if nothing is selected
        /// </summary>
        public string GetSelectedOptionName()
        {
            return _selectedOption?.DisplayName;
        }
    }
}
