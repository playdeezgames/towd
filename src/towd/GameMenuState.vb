Friend Class GameMenuState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Game Menu",
                .X = Pos.Center
            }
        Dim continueButton As New Button With
            {
                .Text = "Continue",
                .X = Pos.Center,
                .Y = Pos.Bottom(titleLabel) + 1,
                .IsDefault = True
            }
        AddHandler continueButton.Clicked, AddressOf OnContinueButtonClicked
        Dim scumSaveButton As New Button With
            {
                .Text = "Scum Save Game",
                .X = Pos.Center,
                .Y = Pos.Bottom(continueButton) + 1
            }
        AddHandler scumSaveButton.Clicked, AddressOf OnScumSaveButtonClicked
        Dim saveButton As New Button With
            {
                .Text = "Save Game",
                .X = Pos.Center,
                .Y = Pos.Bottom(scumSaveButton) + 1
            }
        AddHandler saveButton.Clicked, AddressOf OnSaveButtonClicked
        Dim abandonButton As New Button With
            {
                .Text = "Abandon Game",
                .X = Pos.Center,
                .Y = Pos.Bottom(saveButton) + 1
            }
        AddHandler abandonButton.Clicked, AddressOf OnAbandonButtonClicked
        Add(titleLabel, continueButton, scumSaveButton, saveButton, abandonButton)
    End Sub

    Private Sub OnScumSaveButtonClicked()
        SaveSlot.ScumSlot.ToDescriptor.SaveGame(World)
        MessageBox.Query("Game Saved!", "You saved the game to the scum slot!", "Ok")
    End Sub

    Private Sub OnSaveButtonClicked()
        ShowState(GameState.SaveMenu)
    End Sub

    Private Sub OnAbandonButtonClicked()
        If MessageBox.Query("Confirm Abandon?", "Are you sure you want to abandon the game?", "No", "Yes") = 1 Then
            World.Abandon()
            ShowState(GameState.MainMenu)
        End If
    End Sub

    Private Sub OnContinueButtonClicked()
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        'nada
    End Sub
End Class
