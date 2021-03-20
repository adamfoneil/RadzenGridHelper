using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadzenGridHelper.Models;
using RadzenGridHelper.Services;

namespace Testing
{
    [TestClass]
    public class MarkupTest
    {
        [TestMethod]
        public void RadzenGrid()
        {
            var grid = new Grid()
            {
                ItemType = "Whatever",
                ContextVariable = "w",
                Columns = new Grid.Column[]
                {
                    new Grid.Column()
                    {
                        PropertyName = "FirstName",
                        Title = "First Name",
                        EditorControl = "RadzenTextBox"
                    },
                    new Grid.Column()
                    {
                        PropertyName = "LastName",
                        Title = "Last Name",
                        EditorControl = "RadzenTextBox"
                    },
                    new Grid.Column()
                    {
                        PropertyName = "BirthDate",
                        Title = "Birth Date",
                        EditorControl = "RadzenDatePicker"
                    },
                    new Grid.Column()
                    {
                        PropertyName = "IsActive",
                        Title = "Active",
                        EditorControl = "RadzenSwitch"
                    }
                }
            };

            var xml = new RadzenMarkup().BuildGrid(grid);

            Assert.IsTrue(xml.Equals(
                @"<RadzenGrid TItem=""Whatever"">
  <Columns>
    <RadzenGridColumn TItem=""Whatever"" Title=""First Name"" Property=""FirstName"">
      <EditTemplate Context=""w"">
        <RadzenTextBox @bind-Value=""FirstName""/>
      </EditTemplate>
    </RadzenGridColumn>
    <RadzenGridColumn TItem=""Whatever"" Title=""Last Name"" Property=""LastName"">
      <EditTemplate Context=""w"">
        <RadzenTextBox @bind-Value=""LastName""/>
      </EditTemplate>
    </RadzenGridColumn>
    <RadzenGridColumn TItem=""Whatever"" Title=""Birth Date"" Property=""BirthDate"">
      <EditTemplate Context=""w"">
        <RadzenDatePicker @bind-Value=""BirthDate""/>
      </EditTemplate>
    </RadzenGridColumn>
    <RadzenGridColumn TItem=""Whatever"" Title=""Active"" Property=""IsActive"">
      <EditTemplate Context=""w"">
        <RadzenSwitch @bind-Value=""IsActive""/>
      </EditTemplate>
    </RadzenGridColumn>
  </Columns>
</RadzenGrid>
"));
        }
    }
}
