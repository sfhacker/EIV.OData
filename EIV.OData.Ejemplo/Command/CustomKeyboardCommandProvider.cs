
namespace EIV.OData.Ejemplo.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Telerik.Windows.Controls;
    using Telerik.Windows.Controls.GridView;
    public class CustomKeyboardCommandProvider : DefaultKeyboardCommandProvider
    {
        private GridViewDataControl parentGrid = null;

        public CustomKeyboardCommandProvider(GridViewDataControl grid) : base(grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid", "Invalid value: Null");
            }
            this.parentGrid = grid;
        }

        public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        {
            List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();

            // I want to disable next row in edit mode after updating the previous one
            if (key == Key.Tab)
            {
                //commandsToExecute.Remove(RadGridViewCommands.SelectCurrentItem);
                if (parentGrid.CurrentCell.IsInEditMode)
                {
                    commandsToExecute.Clear();

                    commandsToExecute.Remove(RadGridViewCommands.MoveDown);

                }
            }
            else if (key == Key.Enter)
            {
                if (parentGrid.CurrentCell.IsInEditMode)
                {
                    commandsToExecute.Clear();

                    commandsToExecute.Remove(RadGridViewCommands.MoveDown);

                }
            }

            return commandsToExecute;

        }
    }
}