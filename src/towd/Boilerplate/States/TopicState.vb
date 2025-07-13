Friend Class TopicState
    Inherits ChildView

    Public Shared Property Topic As String
    Private ReadOnly titleLabel As Label
    Private ReadOnly contentTextView As TextView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        titleLabel = New Label With
            {
                .Width = [Dim].Fill,
                .Text = "???? (Esc to continue game)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)

        contentTextView = New TextView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1,
                .Y = Pos.Bottom(titleLabel),
                .Text = "????",
                .TextAlignment = TextAlignment.Left,
                .WordWrap = True,
                .Enabled = False
            }
        Add(contentTextView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center(),
                .Y = Pos.Bottom(contentTextView)
            }
        AddHandler closeButton.Clicked, AddressOf OnCloseButtonClicked
        Add(closeButton)
    End Sub

    Private Sub OnCloseButtonClicked()
        ShowState(NeutralUIDialog.DetermineInPlayDialog(Context))
    End Sub

    Friend Overrides Sub UpdateView()
        titleLabel.Text = $"{Topic.ToTopicDescriptor.Title} (Esc to continue game)"
        contentTextView.Text = Topic.ToTopicDescriptor.Content
        contentTextView.ColorScheme = New ColorScheme With
            {
                .Normal = ColorScheme.Normal,
                .Disabled = ColorScheme.Normal,
                .Focus = ColorScheme.Focus,
                .HotFocus = ColorScheme.HotFocus,
                .HotNormal = ColorScheme.HotNormal
            }
        MyBase.UpdateView()
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            ShowState(NeutralUIDialog.DetermineInPlayDialog(Context))
        End If
    End Sub
End Class
