Imports towd.business

Friend Class DialogState
    Inherits ChildView
    Private ReadOnly promptLabel As Label
    Private ReadOnly linesTextView As TextView
    Private ReadOnly choicesListView As ListView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        promptLabel = New Label With
            {
                .Width = [Dim].Fill,
                .Y = 1,
                .Text = "(dialog prompt)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(promptLabel)
        linesTextView = New TextView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Percent(70),
                .Y = Pos.Bottom(promptLabel),
                .Enabled = False,
                .WordWrap = True,
                .Text = "(dialog lines go here)"
            }
        Add(linesTextView)
        choicesListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill,
                .Y = Pos.Bottom(linesTextView)
            }
        AddHandler choicesListView.OpenSelectedItem, AddressOf OnChoicesListViewOpenSelectedItem
        Add(choicesListView)
    End Sub

    Private Sub OnChoicesListViewOpenSelectedItem(args As ListViewItemEventArgs)
        CurrentDialog = CurrentDialog.Choose(args.Value.ToString)
        UpdateView()
    End Sub

    Friend Overrides Sub UpdateView()
        If CurrentDialog IsNot Nothing Then
            promptLabel.Text = CurrentDialog.Prompt
            linesTextView.Text = String.Join(vbCrLf, CurrentDialog.Lines)
            choicesListView.SetSource(CurrentDialog.Choices.ToList)
            MyBase.UpdateView()
        Else
            ShowState(GameState.Neutral)
        End If
    End Sub

    Public Shared Property CurrentDialog As IDialog
End Class
