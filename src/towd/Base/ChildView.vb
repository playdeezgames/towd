Imports towd.business
Imports towd.data

Friend MustInherit Class ChildView
    Inherits View
    Private ReadOnly mainView As MainView
    Private Shared worldData As New WorldData
    Protected Shared Function LoadGame(saveSlot As SaveSlot) As Boolean
        Dim loadAttempt = saveSlot.ToDescriptor.LoadGame()
        If loadAttempt IsNot Nothing Then
            worldData = loadAttempt
            MessageBox.Query("Game Loaded!", $"{saveSlot.ToDescriptor.DisplayName} is loaded!", "Ok")
            Return True
        Else
            MessageBox.ErrorQuery("Load Failed!", $"Could not load {saveSlot.ToDescriptor.DisplayName}!", "Ok")
            Return False
        End If
    End Function
    Protected Shared Sub SaveGame(saveSlot As SaveSlot)
        saveSlot.ToDescriptor.SaveGame(ChildView.worldData)
    End Sub
    Protected Shared ReadOnly Property World As IWorld
        Get
            Return New World(worldData)
        End Get
    End Property
    Sub New(mainView As MainView)
        Me.mainView = mainView
        Width = [Dim].Fill()
        Height = [Dim].Fill()
        AddHandler Me.KeyPress, AddressOf OnKeyPress
    End Sub

    Protected Overridable Sub OnKeyPress(args As KeyEventEventArgs)
        'nada
    End Sub
    Friend MustOverride Sub UpdateView()
    Protected Sub ShowState(gameState As GameState)
        mainView.ShowState(gameState)
    End Sub
End Class
