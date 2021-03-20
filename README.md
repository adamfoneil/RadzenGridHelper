This is a little WinForms app to help with creating [Radzen DataGrid](https://blazor.radzen.com/datagrid) markup. The Radzen syntax is a little verbose IMO, so this helps remove the repeated content. I'm not complaining about Radzen's components, because their stuff is good and free. I just need a little more productivity when it comes to creating data grid functionality in Blazor. Having done it manually dozens of times now, I'm ready for a little tooling help. The need for this came up as I've been working on my show-and-tell Blazor project [BlazorAO](https://github.com/adamfoneil/BlazorAO).

![img](https://adamosoftware.blob.core.windows.net/images/SV7VJC3876.png)

This is .NET5 WinForms app, which I'm very new to. I really like it, but my [WinForms.Library](https://github.com/adamfoneil/WinForms.Library) is not compatible, so I had to build some new data binding helpers. That's what this new [WinForms.Binding](https://github.com/adamfoneil/RadzenGridHelper/tree/master/WinForms.Binding) project is. This has the [Save](https://github.com/adamfoneil/RadzenGridHelper/blob/master/WinForms.Binding/DocumentManager.cs#L86), [New](https://github.com/adamfoneil/RadzenGridHelper/blob/master/WinForms.Binding/DocumentManager.cs#L49), and [Open](https://github.com/adamfoneil/RadzenGridHelper/blob/master/WinForms.Binding/DocumentManager.cs#L61) functionality. I also started a new [ControlBinder](https://github.com/adamfoneil/RadzenGridHelper/blob/master/WinForms.Binding/ControlBinder.cs) class adapted from my old library. This new binder class is in its infancy, and I've only implemented a couple control types [ToolStripTextBoxes](https://github.com/adamfoneil/RadzenGridHelper/blob/master/WinForms.Binding/ControlBinder_ToolStrip.cs) and a new [DataGridView](https://github.com/adamfoneil/RadzenGridHelper/blob/master/WinForms.Binding/ControlBinder_DataGridView.cs) handler.

You can see where I'm using my new binder in the main form [here](https://github.com/adamfoneil/RadzenGridHelper/blob/master/RadzenGridHelper/frmMain.cs#L32).

As for the markup generation, this is done with a few ingredients:

- A special [XmlElement](https://github.com/adamfoneil/RadzenGridHelper/blob/master/RadzenGridHelper/Classes/XmlElement.cs) class. Yes, C# has good XML support natively, but neither `XElement` nor `XmlDocument` were really simple enough for what I wanted.

- A model class [Grid](https://github.com/adamfoneil/RadzenGridHelper/blob/master/RadzenGridHelper/Models/Grid.cs) is what my UI binds to and represents the markup output.

- The grid markup itself is made by [RadzenMarkup.BuildGrid](https://github.com/adamfoneil/RadzenGridHelper/blob/master/RadzenGridHelper/Services/RadzenMarkup.cs). This builds XML in a particular way from a `Grid` object.

- You can see sample output in the [unit test](https://github.com/adamfoneil/RadzenGridHelper/blob/master/Testing/MarkupTest.cs) for it.

I considered adding Reflection capability to make it easier to discover property names, but there's a big jump in complexity to make that all work nicely, so I'm stopping where I am now on this.
