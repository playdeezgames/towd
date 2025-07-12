Imports towd.business
Imports towd.data

Friend MustInherit Class ChildView
    Inherits View
    Private ReadOnly mainView As MainView
    Private Shared worldData As New WorldData
    Protected Shared Function LoadGame(saveSlot As String) As Boolean
        Dim loadAttempt = saveSlot.ToSaveSlotDescriptor.LoadGame()
        If loadAttempt IsNot Nothing Then
            worldData = loadAttempt
            MessageBox.Query("Game Loaded!", $"{saveSlot.ToSaveSlotDescriptor.DisplayName} is loaded!", "Ok")
            Return True
        Else
            MessageBox.ErrorQuery("Load Failed!", $"Could not load {saveSlot.ToSaveSlotDescriptor.DisplayName}!", "Ok")
            Return False
        End If
    End Function
    Protected Shared Sub SaveGame(saveSlot As String, notify As Boolean)
        saveSlot.ToSaveSlotDescriptor.SaveGame(ChildView.worldData)
        If notify Then
            MessageBox.Query("Game Saved!", $"{saveSlot.ToSaveSlotDescriptor.DisplayName} is saved!", "Ok")
        End If
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
    Friend Overridable Sub UpdateView()
        Dim character = World?.Avatar
        While character?.HasMessages
            MessageBox.Query("", String.Join(vbCrLf, character.CurrentMessage), "Ok")
            character.DismissMessage()
        End While
    End Sub
    Protected Sub ShowState(gameState As GameState)
        mainView.ShowState(gameState)
    End Sub
End Class
