Friend Class TopicState
    Inherits ChildView

    Public Shared Property Topic As Topic
    Private ReadOnly titleLabel As Label
    Private ReadOnly contentLabel As TextView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        titleLabel = New Label With
            {
                .Width = [Dim].Fill,
                .Text = "???? (Esc to continue game)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)

        contentLabel = New TextView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 3,
                .Y = Pos.Bottom(titleLabel) + 1,
                .Text = "????",
                .TextAlignment = TextAlignment.Left,
                .WordWrap = True,
                .[ReadOnly] = True
            }
        Add(contentLabel)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center(),
                .Y = Pos.Bottom(contentLabel) + 1
            }
        AddHandler closeButton.Clicked, AddressOf OnCloseButtonClicked
        Add(closeButton)
    End Sub

    Private Sub OnCloseButtonClicked()
        ShowState(GameState.Neutral)
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
