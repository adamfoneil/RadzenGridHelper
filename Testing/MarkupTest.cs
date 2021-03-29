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

            var xml = RadzenMarkup.BuildGrid(grid, ' ', 2);

            Assert.IsTrue(xml.Equals(
                @"<RadzenGrid TItem=""Whatever"">
  <Columns>
    <RadzenGridColumn TItem=""Whatever"" Title=""First Name"" Property=""FirstName"">
      <Template Context=""w"">
        <span>@w.FirstName</span>
      </Template>
      <EditTemplate Context=""w"">
        <RadzenTextBox @bind-Value=""w.FirstName""/>
      </EditTemplate>
    </RadzenGridColumn>
    <RadzenGridColumn TItem=""Whatever"" Title=""Last Name"" Property=""LastName"">
      <Template Context=""w"">
        <span>@w.LastName</span>
      </Template>
      <EditTemplate Context=""w"">
        <RadzenTextBox @bind-Value=""w.LastName""/>
      </EditTemplate>
    </RadzenGridColumn>
    <RadzenGridColumn TItem=""Whatever"" Title=""Birth Date"" Property=""BirthDate"">
      <Template Context=""w"">
        <span>@w.BirthDate</span>
      </Template>
      <EditTemplate Context=""w"">
        <RadzenDatePicker @bind-Value=""w.BirthDate""/>
      </EditTemplate>
    </RadzenGridColumn>
    <RadzenGridColumn TItem=""Whatever"" Title=""Active"" Property=""IsActive"">
      <Template Context=""w"">
        <span>@w.IsActive</span>
      </Template>
      <EditTemplate Context=""w"">
        <RadzenSwitch @bind-Value=""w.IsActive""/>
      </EditTemplate>
    </RadzenGridColumn>
  </Columns>
</RadzenGrid>
"));
        }
    }
}
