Friend Class TopicState
    Inherits ChildView

    Public Shared Property Topic As Topic
    Private ReadOnly titleLabel As Label
    Private ReadOnly contentLabel As Label

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        titleLabel = New Label With
            {
                .Width = [Dim].Fill,
                .Text = "???? (Esc to continue game)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        contentLabel = New Label With
            {
                .Width = [Dim].Fill,
                .Y = Pos.Bottom(titleLabel) + 1,
                .Text = "????",
                .TextAlignment = TextAlignment.Left
            }
        Add(contentLabel)
    End Sub

    Friend Overrides Sub UpdateView()
        titleLabel.Text = $"{Topic.ToDescriptor.Title} (Esc to continue game)"
        contentLabel.Text = Topic.ToDescriptor.Content
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            ShowState(GameState.Neutral)
        End If
    End Sub
End Class
