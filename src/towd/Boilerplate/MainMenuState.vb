Imports System.Data
Imports System.Runtime.CompilerServices
Imports System.Runtime.Loader

Friend Class MainMenuState
    Inherits ChildView
    Private scumLoadButton As Button
    Private loadButton As Button

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Main Menu",
                .X = Pos.Center
            }

        Dim embarkButton As New Button With
            {
                .Text = "Embark!",
                .Y = Pos.Bottom(titleLabel) + 1,
                .X = Pos.Center,
                .IsDefault = True
            }
        AddHandler embarkButton.Clicked, AddressOf OnEmbarkButtonClicked

        scumLoadButton = New Button With
            {
                .Text = "Scum Load",
                .Y = Pos.Bottom(embarkButton) + 1,
                .X = Pos.Center,
                .IsDefault = True
            }
        AddHandler scumLoadButton.Clicked, AddressOf OnScumLoadButtonClicked

        loadButton = New Button With
            {
                .Text = "Load...",
                .Y = Pos.Bottom(scumLoadButton) + 1,
                .X = Pos.Center,
                .IsDefault = True
            }
        AddHandler loadButton.Clicked, AddressOf OnLoadButtonClicked

        Dim quitButton As New Button With
            {
                .Text = "Quit",
                .Y = Pos.Bottom(loadButton) + 1,
                .X = Pos.Center
            }
        AddHandler quitButton.Clicked, AddressOf OnQuitButtonClicked

        Add(titleLabel, embarkButton, scumLoadButton, loadButton, quitButton)
    End Sub

    Private Sub OnLoadButtonClicked()
        ShowState(GameState.LoadMenu)
    End Sub

    Private Sub OnScumLoadButtonClicked()
        If LoadGame(SaveSlot.ScumSlot) Then
            ShowState(GameState.Neutral)
        End If
    End Sub

    Private Sub OnEmbarkButtonClicked()
        World.Initialize()
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnQuitButtonClicked()
        If MessageBox.Query("Confirm Quit", "Are you sure you want to quit?", "No", "Yes") = 1 Then
            Application.RequestStop()
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        scumLoadButton.Enabled = SaveSlot.ScumSlot.ToSaveSlotDescriptor.SaveExists
        loadButton.Enabled = SaveSlots.Descriptors.Any(Function(x) x.Value.SaveExists)
        MyBase.UpdateView()
    End Sub
End Class
